using CallCenter.CORE;
using CallCenter.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Application
{
    public class MessageManager : Manager<Message>
    {
         public MessageManager(DBContext context) 
            : base(context) 
        { 
        
        }
        /// <summary>
        /// Método que devuelve todos los equipos
        /// </summary>
        /// <returns>Coleccion de tipos de equipo</returns>
        public IQueryable<Message> GetAll()
        {
            return Context.Messages;
        }

        /// <summary>
        /// Método que devuelve los equipos de un usuario
        /// </summary>
        /// <param name="id">Id del ususario</param>
        /// <returns></returns>
        public IQueryable<Message> GetByIncidence(Guid id)
        {
            return Context.Messages.Include("Incidence").Where(i => i.Incidence.Id == id);
        }
    }
}
