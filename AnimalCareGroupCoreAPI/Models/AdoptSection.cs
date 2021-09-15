using System;
using System.Collections.Generic;

#nullable disable

namespace AnimalCareGroupCoreAPI.Models
{
    public partial class AdoptSection
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}
