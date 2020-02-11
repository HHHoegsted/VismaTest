using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace e_conomicTest
{
    class Customer
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int CustomerNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public decimal DueAmount { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }

        public override string ToString()
        {
            return 
                "-----Kunde-----\nID:\t\t" + CustomerNumber + 
                "\nNavn:\t\t" + Name + 
                "\nEmail:\t\t" + Email + 
                "\nAdresse:\t" + Address + ", " + Zip + " " + City +
                "\nBalance:\t" + String.Format("{0:C}", Balance) +
                "\nForfaldent:\t" + String.Format("{0:C}", DueAmount);
        }
    }
}
