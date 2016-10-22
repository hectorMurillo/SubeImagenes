using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TareaDatos.Models;

namespace TareaDatos.Repositories
{
    public class AzureDatosRepository : IDatosRepository
    {
        private string connection;
        public AzureDatosRepository( string connection)
        {
            this.connection = connection;
        }   
        public void BorrarBache(int id)
        {
            throw new NotImplementedException();
        }

        public void CrearBache(Bache nuevo)
        {
            var Datos = Referencia("Baches");
            int sigId;
            Datos.CreateIfNotExists();

            var todos = this.LeerBaches();
            nuevo.Id = (todos.Count + 1);
            if (nuevo.Id != 0)
            {                
                sigId = todos.Count();
            }
            else { sigId = 0; }

            BacheEntity entity = new BacheEntity((sigId + 1).ToString());
            entity.Id = nuevo.Id;
            entity.Imagen = nuevo.Imagen;
            
            

            TableOperation insertOperation = TableOperation.Insert(entity);

            Datos.Execute(insertOperation);
        }

        public List<Bache> LeerBaches()
        {
            var table = Referencia("Baches");
            table.CreateIfNotExists();
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<BacheEntity> query = new TableQuery<BacheEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Datos"));

            var datos = table.ExecuteQuery(query);

            if (datos != null)
            {
                return datos.Select(e => new Models.Bache()
                {
                    Id = e.Id,
                    Imagen = e.Imagen,
                    Descripcion=e.Descripcion,
                    Latitud=e.Latitud,
                    Longitud=e.Longitud
                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public Bache LeerBachePorID(int id)
        {
            var table = Referencia("Baches");
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            var query = TableOperation.Retrieve<BacheEntity >("Productos", id.ToString());

            var dato = table.Execute(query);

            if (dato?.Result != null)
            {
                var e = (BacheEntity)dato.Result;
                return new Models.Bache
                {
                    Id = e.Id,
                    Imagen = e.Imagen,
                    Descripcion = e.Descripcion,
                    Latitud = e.Latitud,
                    Longitud = e.Longitud
                };
            }
            else
            {
                return null;
            }
        }
        public void ActualizarBache(Bache editar)
        {
            var tabla = Referencia("Baches");

            var query = TableOperation.Retrieve<BacheEntity>("Baches", editar.Id.ToString());

            var producto = tabla.Execute(query);

            if (producto?.Result != null)
            {
                var p = (BacheEntity)producto.Result;

                // Create the Replace TableOperation.
                TableOperation updateOperation = TableOperation.Replace(p);                
                p.Imagen = editar.Imagen;
                p.Latitud = editar.Latitud;
                p.Longitud = editar.Longitud;

                // Execute the operation.
                tabla.Execute(updateOperation);
            }
        }
        public void BorrarPeriodico(int id)
        {
            throw new NotImplementedException();
        }

        public void CrearPeriodico(Periodico nuevo)
        {
            var Periodicos = Referencia("Periodicos");
            int sigId;
            Periodicos.CreateIfNotExists();

            var todos = this.LeerPeriodicos();
            nuevo.Id = (todos.Count + 1);
            if (nuevo.Id != 0)
            {
                sigId = todos.Count();
            }
            else { sigId = 0; }

            PeriodicoEntity entity = new PeriodicoEntity((sigId + 1).ToString());
            entity.Id = nuevo.Id;
            entity.Imagen = nuevo.Imagen;
            entity.Descripcion = nuevo.Descripcion;


            TableOperation insertOperation = TableOperation.Insert(entity);

            Periodicos.Execute(insertOperation);
        }

        public List<Periodico> LeerPeriodicos()
        {
            var table = Referencia("Periodicos");
            table.CreateIfNotExists();
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<PeriodicoEntity> query = new TableQuery<PeriodicoEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Periodicos"));

            var datos = table.ExecuteQuery(query);

            if (datos != null)
            {
                return datos.Select(e => new Models.Periodico()
                {
                    Id = e.Id,
                    Imagen = e.Imagen,
                    Descripcion = e.Descripcion,                  
                }).ToList();
            }
            else
            {
                return null;
            }
        }

        public void ActualizarPeriodico(Periodico editar)
        {
            var tabla = Referencia("Periodicos");

            var query = TableOperation.Retrieve<PeriodicoEntity>("Periodicos", editar.Id.ToString());

            var producto = tabla.Execute(query);

            if (producto?.Result != null)
            {
                var p = (PeriodicoEntity)producto.Result;

                // Create the Replace TableOperation.
                TableOperation updateOperation = TableOperation.Replace(p);                
                p.Imagen = editar.Imagen;
                p.Descripcion = editar.Descripcion;
                // Execute the operation.
                tabla.Execute(updateOperation);
            }
        }
        public Periodico LeerPeriodicoPorID(int id)
        {
            var table = Referencia("Periodicos");
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            var query = TableOperation.Retrieve<PeriodicoEntity>("Periodicos", id.ToString());

            var dato = table.Execute(query);

            if (dato?.Result != null)
            {
                var e = (PeriodicoEntity)dato.Result;
                return new Models.Periodico
                {
                    Id = e.Id,
                    Imagen = e.Imagen,
                    Descripcion = e.Descripcion,                    
                };
            }
            else
            {
                return null;
            }
        }

        private CloudTable Referencia(string tipo)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connection);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tipo);
            return table;
        }
    }
    public class BacheEntity : TableEntity
    {
        public BacheEntity(string id)
        {
            this.PartitionKey = "Baches";
            this.Id = int.Parse(id);
            this.RowKey = id;
        }
        public BacheEntity()
        {

        }
        public int Id { get; set; }        
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }
    public class PeriodicoEntity : TableEntity
    {
        public PeriodicoEntity(string id)
        {
            this.PartitionKey = "Periodicos";
            this.Id = int.Parse(id);
            this.RowKey = id;
        }
        public PeriodicoEntity()
        {

        }

        public int Id { get; set; }        
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
    }
}