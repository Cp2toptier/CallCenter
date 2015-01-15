using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.CORE
{
    /// <summary>
    /// Entidad de dominio de tipos de equipo
    /// </summary>
    public class EquipmentType
    {
        /// <summary>
        /// Identificador único del tipo de equipo
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del tipo de equipo
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// Descripción del tipo de equipo
        /// </summary>
        public String Description { get; set; }

    }
}
