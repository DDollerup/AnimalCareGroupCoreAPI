using System;
using System.Collections.Generic;

#nullable disable

namespace AnimalCareGroupCoreAPI.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
