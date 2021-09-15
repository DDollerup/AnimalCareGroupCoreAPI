using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCareGroupCoreAPI.Models
{
    public partial class DBChanges
    {
        public long Id { get; set; }
        public string Resource { get; set; }
        public string DateChanged { get; set; }
        public long ResourceId { get; set; }
        public string Action { get; set; }

    }
}
