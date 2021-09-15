using System;
using System.Collections.Generic;

#nullable disable

namespace AnimalCareGroupCoreAPI.Models
{
    public partial class Sysdiagram
    {
        public string Name { get; set; }
        public long PrincipalId { get; set; }
        public long DiagramId { get; set; }
        public long? Version { get; set; }
        public byte[] Definition { get; set; }
    }
}
