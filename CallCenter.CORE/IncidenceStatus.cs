using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.CORE
{
    /// <summary>
    /// Estado de una incidencia
    /// </summary>
    public enum IncidenceStatus : int
    {
        /// <summary>
        /// Abierta
        /// </summary>
        Abierta = 0,
        /// <summary>
        /// En proceso
        /// </summary>
        InProcess = 1,
        /// <summary>
        /// Cerrada
        /// </summary>
        Cerrada = 2
    }
}
