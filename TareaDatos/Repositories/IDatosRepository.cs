using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TareaDatos.Models;
namespace TareaDatos.Repositories
{
    public interface IDatosRepository
    {
        List<Bache> LeerBaches();        
        Bache LeerBachePorID(int id);
        void CrearBache(Bache nuevo);
        void ActualizarBache(Bache editar);
        void BorrarBache(int id);
        List<Periodico> LeerPeriodicos();
        Periodico LeerPeriodicoPorID(int id);
        void CrearPeriodico(Periodico nuevo);
        void ActualizarPeriodico(Periodico editar);
        void BorrarPeriodico(int id);

    }
}
