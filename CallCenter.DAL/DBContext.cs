using CallCenter.CORE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL
{
    /// <summary>
    /// Contexto de datos
    /// </summary>
    public class DBContext : DbContext
    {
        /// <summary>
        /// Constructor sin parámetros
        /// </summary>
        public DBContext()
        {

        }

        /// <summary>
        /// Contructyor del contexto de datos intanciando
        /// la clase de base de Entity Framework
        /// </summary>
        /// <param name="nameOrConectionString">Nombre o cadena de conexión</param>
        public DBContext(string nameOrConectionString)
            : base(nameOrConectionString)
        {

        }

        /// <summary>
        /// Conleccion de incidencias
        /// </summary>
        public DbSet<Incidence> Incidences { get; set; }
        /// <summary>
        /// Coleccion de equipos
        /// </summary>
        public DbSet<Equipment> Equipments { get; set; }
        /// <summary>
        /// Coleccion de tipo de equipos
        /// </summary>
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        /// <summary>
        /// Coleccion de mensajes
        /// </summary>
        public DbSet<Message> Messages { get; set; }
        
    }
}
