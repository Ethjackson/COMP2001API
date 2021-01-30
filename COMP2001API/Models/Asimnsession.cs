using System;
using System.Collections.Generic;

#nullable disable

namespace COMP2001API.Models
{
    public partial class Asimnsession
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
