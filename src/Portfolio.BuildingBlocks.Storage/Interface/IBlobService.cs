namespace Portfolio.BuildingBlocks.Storage.Interface
{
    public interface IBlobService
    {
        public string GetBlobFileWithSasRead(string container, int minutesExpire = 2);
        public string GetBlobFileWithSasRead(string pathBlobName, string container, int minutesExpire = 2);
        public string GetBlobFileWithSasWrite(string pathBlobName, string container, int minutesExpire = 2);
        public string GetStorageContainerUri(string container);
        public Task<bool> DeleteBlobAsync(string containerName, string blobName, CancellationToken cancellationToken);
    }
}
