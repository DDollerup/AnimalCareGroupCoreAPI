using System;
using System.Collections.Generic;

#nullable disable

namespace AnimalCareGroupCoreAPI.Models
{
    public partial class Animal
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public long? DaysInCare { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Details { get; set; }
        public string DateAdded { get; set; }
        public string DateChanged { get; set; }
    }
}
