using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using PManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace PManagement.Storage
{
    public class AzureStorage : IStorageService
    {
        private readonly string storageAccount;
        private readonly string storageKey;
        private readonly string containerName;

        public AzureStorage(string storageAccount, string storageKey, string containerName)
        {
            this.storageAccount = storageAccount;
            this.storageKey = storageKey;
            this.containerName = containerName;
        }

        public string Updoad(string fileName, System.IO.Stream fileStream)
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            var blob = container.GetBlockBlobReference(fileName);
            blob.UploadFromStreamAsync(fileStream).Wait();

            return blob.Uri.AbsoluteUri;
        }
    
        public void List()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            List<string> blobs = new List<string>();
            BlobResultSegment resultSegment = container.ListBlobsSegmentedAsync(null).Result;
            foreach (IListBlobItem item in resultSegment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob blob = (CloudPageBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory dir = (CloudBlobDirectory)item;
                    blobs.Add(dir.Uri.ToString());
                }
            }
            //return View(blobs);
        }

        private CloudBlobContainer GetCloudBlobContainer()
        {
            //Account
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(
                new StorageCredentials(storageAccount, storageKey), true);

            //Client
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

            //Container
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
            blobContainer.CreateIfNotExistsAsync();

            return blobContainer;

            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureConnectionString);
            //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            //CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            //return container;
        }
    }
}
