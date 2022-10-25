using System;
using System.Collections.Generic;

#nullable disable

namespace DeveloperAPI.Models
{
    public partial class Security
    {
        public int SecurityId { get; set; }
        public int? SecurityApp { get; set; }
        public string SecurityKey { get; set; }
    }
}
