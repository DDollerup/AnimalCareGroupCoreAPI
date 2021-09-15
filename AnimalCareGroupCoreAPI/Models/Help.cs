using System;
using System.Collections.Generic;

#nullable disable

namespace AnimalCareGroupCoreAPI.Models
{
    public partial class Help
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}
