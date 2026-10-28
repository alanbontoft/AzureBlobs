namespace AzureBlobs;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Azure Blob Storage Test Application");

        var blobService = new AzureBlobService();

        await blobService.ListContainersAsync();

        await blobService.CreateTestFileAsync("testfile1.txt");

        await blobService.CreateTestFileAsync("testfile2.txt");


    }
}
