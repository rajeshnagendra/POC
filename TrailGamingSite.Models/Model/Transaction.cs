using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrailGamingSite.Models.Model
{
    
    public class Transaction
    {
        public Transaction()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; } //Transaction Id
        public decimal Amount { get; set; } //Transaction Amount
        public string Type { get; set; } //Transaction Type
        public DateTime Date { get; set; } //Transaction Date

        public int CustomerId { get; set; }
    }
}

