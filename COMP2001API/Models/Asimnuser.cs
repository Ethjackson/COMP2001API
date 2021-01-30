using System;
using System.Collections.Generic;

#nullable disable

namespace COMP2001API.Models
{
    public partial class Asimnuser
    {
        public int UserId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}
