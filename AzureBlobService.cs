using Azure.Storage;
using Azure.Storage.Blobs;

class AzureBlobService
{

    private readonly string _accessKey = "<access key>";

    private readonly string _accountName = "<storage account name>";

    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobService()
    {
        var credential = new StorageSharedKeyCredential(_accountName, _accessKey);

        var blobUri = new Uri($"https://{_accountName}.blob.core.windows.net");

        _blobServiceClient = new BlobServiceClient(blobUri, credential);

    }

    public async Task ListContainersAsync()
    {
        await foreach (var container in _blobServiceClient.GetBlobContainersAsync())
        {
            Console.WriteLine($"Container Name: {container.Name}");
        }
    }

    public async Task CreateTestFileAsync(string fileName)
    {
        try
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient("files");

            var blob = blobContainerClient.GetBlobClient($"data/{fileName}");

            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, leaveOpen: true))
            {
                await writer.WriteLineAsync("Azure Blob Test File");
                await writer.WriteLineAsync($"Created on {DateTime.Now}");
                await writer.FlushAsync();
            }

            stream.Position = 0;

            await blob.UploadAsync(stream, overwrite: true);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating test file: {ex.Message}");
        }


    }

}