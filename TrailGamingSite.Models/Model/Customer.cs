using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrailGamingSite.Models.Model
{
    public class Customer
    {
        public Customer()
        {
            RegisteredDate = DateTime.Now;
            Active = true;
        }
        public int Id { get; set; } //Customer Id 

        [Required]
        public string Name { get; set; } //Customer Name

        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RegisteredDate { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ICollection<Transaction> Transaction { get; set; }

    }
}