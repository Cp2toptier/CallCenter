using CallCenter.CORE;
using CallCenter.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Application
{
    public class IncidenceManager : Manager<Incidence>
    {
        public IncidenceManager(DBContext context) 
            :base(context)
        {
            
        }
        /// <summary>
        /// Método que devuelve las incidencias 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Incidence> GetByUserId(Guid id)
        {
            return Context.Incidences.Include("Equipment").Where(i => i.UserId == id);
        }

        /// <summary>
        /// Método que devuelve las incidencias con el equipo sobre el que están hechas
        /// </summary>
        /// <param name="id">Id de incidencia</param>
        /// <returns> </returns>
        public Incidence GetWithEquipment(Guid id)
        {
            return Context.Incidences.Include("Equipment").Where(i => i.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// Método que devuelve todas las incidencias
        /// </summary>
        /// <returns></returns>
        public IQueryable<Incidence> GetAll()
        {
            return Context.Incidences.Include("Equipment");
        }

    }
}
