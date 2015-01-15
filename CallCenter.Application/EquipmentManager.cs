using CallCenter.CORE;
using CallCenter.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Application
{
    public class EquipmentManager : Manager<Equipment>
    {
         public EquipmentManager(DBContext context) 
            : base(context) 
        { 
        
        }
        /// <summary>
        /// Método que devuelve todos los equipos
        /// </summary>
        /// <returns>Coleccion de tipos de equipo</returns>
        public IQueryable<Equipment> GetAll()
        {
            return Context.Equipments.Include("EquipmentType");
        }

        /// <summary>
        /// Método que devuelve los equipos de un usuario
        /// </summary>
        /// <param name="id">Id del ususario</param>
        /// <returns></returns>
        public IQueryable<Equipment> GetByUserId(Guid id)
        {
            return Context.Equipments.Include("EquipmentType").Where(i => i.UserId == id);
        }

        public IQueryable<Equipment> GetWithEquipmentType(Guid id)
        {
            return Context.Equipments.Include("EquipmentType").Where(i => i.Id == id);
        }

        public Equipment GetOneWithEquipmentType(Guid id)
        {
            return Context.Equipments.Include("EquipmentType").Where(i => i.Id == id).SingleOrDefault();
        }
    }
}
