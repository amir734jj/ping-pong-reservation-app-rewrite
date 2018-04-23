using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Models.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        
        public DateTime StarTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
    }
}