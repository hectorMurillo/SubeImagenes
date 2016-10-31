using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TareaDatos.Repositories
{
    public class AzureImageStorage : IImageStorageContainer
    {
        private string connectionString;
        public AzureImageStorage(string connection)
        {
            connectionString = connection;
        }
        public string GuardarImagen(string contenedor, string nombre, Stream archivo)
        {
            //Cuenta
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            //Contenedor (Trim para los whitespace)
            CloudBlobContainer container = blobClient.GetContainerReference(contenedor.ToLower().Trim());
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            //Archivo o Blob

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombre.ToLower().Trim());

            blockBlob.UploadFromStream(archivo);
            return blockBlob.Uri.ToString();
        }

        public string LeerImagen(string contenedor, string nombre)
        {
            throw new NotImplementedException();
        }
        public Stream obtenerImagen(string contenedor, string nombre, Stream archivo)
        {
            Stream memoria = new MemoryStream();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(contenedor.ToLower().Trim());
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombre.ToLower().Trim());
            if (blockBlob != null)
                blockBlob.DownloadToStream(memoria);
            memoria.Position = 0;
            return memoria;
        }
    }
}