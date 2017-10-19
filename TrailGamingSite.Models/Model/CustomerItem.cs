using TrailGamingSite.Models.Model;
using System;
using System.Collections.Generic;

namespace TrailGamingSite.Models.Model
{
    public class CustomerItem
    {
        public int Id { get; set; } //Customer Id 
        
        public string Name { get; set; } //Customer Name
        
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RegisteredDate { get; set; }
        public bool Active { get; set; }
        
        public ICollection<Transaction> Transaction { get; set; }
    }
}
