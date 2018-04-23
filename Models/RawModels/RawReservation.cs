using System;
using System.Collections.Generic;
using System.ComponentModel;
using Models.Models;

namespace Models.RawModels
{
    public class RawReservation
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        
        public DateTime StarTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
    }
}