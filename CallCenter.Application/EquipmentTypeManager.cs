using CallCenter.CORE;
using CallCenter.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Application
{
    public class EquipmentTypeManager : Manager<EquipmentType>
    {
        public EquipmentTypeManager(DBContext context) 
            : base(context) 
        { 
        
        }
        /// <summary>
        /// Método que devuelve todos los tipos de equipo
        /// </summary>
        /// <returns>Coleccion de tipos de equipo</returns>
        public IQueryable<EquipmentType> GetAll()
        {
            return Context.EquipmentTypes;
        }
    }
}
