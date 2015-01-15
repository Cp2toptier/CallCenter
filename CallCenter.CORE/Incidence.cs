using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.CORE
{
    /// <summary>
    /// 
    /// </summary>
    public class Incidence
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IncidenceStatus Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Equipment Equipment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CloseDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String InternalNote { get; set; }

        public String InternalNotePrueba { get; set; }
    }
}
