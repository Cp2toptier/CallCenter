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
    public class Equipment
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EquipmentType EquipmentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PurchaseDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Incidence> Issues { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Description { get; set; }
    }
}
