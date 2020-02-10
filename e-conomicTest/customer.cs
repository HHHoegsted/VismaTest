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
        public double Balance { get; set; }

        public override string ToString()
        {
            return "-----Kunde-----\nID:\t" + CustomerNumber + "\nNavn:\t" + Name + "\nEmail:\t" + Email + "\nBalance:\t" + Balance;
        }
    }
}
