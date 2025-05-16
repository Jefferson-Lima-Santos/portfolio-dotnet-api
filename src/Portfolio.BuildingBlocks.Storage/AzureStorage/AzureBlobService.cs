using Portfolio.BuildingBlocks.Storage.Interface;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Portfolio.BuildingBlocks.Storage.AzureStorage
{
    public class AzureBlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<AzureBlobService> _logger;

        public AzureBlobService(BlobServiceClient blobServiceClient, ILogger<AzureBlobService> logger)
        {
            _blobServiceClient = blobServiceClient;
            _logger = logger;
        }

        public string GetBlobFileWithSasRead(string container, int minutesExpire = 2)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = container,
                Resource = "c",
                StartsOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddMinutes(minutesExpire),
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.List);
            Uri uri = blobContainerClient.GenerateSasUri(sasBuilder);

            return uri.ToString();
        }

        public string GetBlobFileWithSasRead(string pathBlobName, string container, int minutesExpire = 2)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlockBlobClient blockBlobClient = blobContainerClient.GetBlockBlobClient(pathBlobName);
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = container,
                BlobName = pathBlobName,
                Resource = "b",
                StartsOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddMinutes(minutesExpire),
                ContentType = "image/jpeg"
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.List);
            Uri uri = blockBlobClient.GenerateSasUri(sasBuilder);

            return uri.ToString();
        }

        public string GetBlobFileWithSasWrite(string pathBlobName, string container, int minutesExpire = 2)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlockBlobClient blockBlobClient = blobContainerClient.GetBlockBlobClient(pathBlobName);
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = container,
                BlobName = pathBlobName,
                Resource = "b",
                StartsOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddMinutes(minutesExpire),
                ContentType = "image/jpeg"
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.Write | BlobSasPermissions.List);
            Uri uri = blockBlobClient.GenerateSasUri(sasBuilder);

            return uri.ToString();
        }

        public string GetStorageContainerUri(string container)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            return blobContainerClient.Uri.ToString();
        }

        public async Task<string> UploadFileByteArrayToBlobAsync(byte[] fileByteArray, string containerName, string blobName, string contentType)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            using (MemoryStream stream = new MemoryStream(fileByteArray))
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType });
            }
            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteBlobAsync(string container, string blobName, CancellationToken cancellationToken)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobClient existingBlobClient = containerClient.GetBlobClient(blobName);
            return await existingBlobClient.DeleteIfExistsAsync();
        }
    }

    public static class AzureBlobServiceDependencyInjection
    {
        public static void AddAzureBlobServiceDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var teste = configuration.GetSection("AzureBlobStorage").GetRequiredSection("ConnectionString").Value;
            services.AddScoped(x => new BlobServiceClient(configuration.GetSection("AzureBlobStorage").GetRequiredSection("ConnectionString").Value));
            services.AddScoped<IBlobService, AzureBlobService>();
        }
    }
}
