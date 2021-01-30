using System;
using System.Collections.Generic;

#nullable disable

namespace COMP2001API.Models
{
    public partial class Asimnpassword
    {
        public int PasswordId { get; set; }
        public int UserId { get; set; }
        public string password { get; set; }
        public DateTime? DateChanged { get; set; }
    }
}
