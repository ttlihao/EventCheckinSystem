using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly string _connectionString;
        private readonly string _containerName = "guest-images";

        public AzureBlobService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("File cannot be null or empty.", nameof(imageFile));

            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            await containerClient.CreateIfNotExistsAsync();

            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = new MemoryStream())
            {
                await imageFile.CopyToAsync(stream);
                stream.Position = 0;
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = imageFile.ContentType });
            }

         
            var sasToken = GenerateSasToken(blobClient);
            return $"{sasToken}";
        }

        private string GenerateSasToken(BlobClient blobClient)
        {
           
            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = _containerName,
                BlobName = blobClient.Name,
                ExpiresOn = DateTimeOffset.UtcNow.AddYears(1)
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.Write);
            return blobClient.GenerateSasUri(sasBuilder).ToString();
        }
    }
}
