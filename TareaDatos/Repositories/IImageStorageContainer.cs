using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TareaDatos.Repositories
{
    public interface IImageStorageContainer
    {
        string GuardarImagen(string contenedor, string nombre, Stream archivo);
        string LeerImagen(string contenedor, string nombre);

    }
}