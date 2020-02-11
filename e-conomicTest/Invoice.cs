using System;
using System.Collections.Generic;
using System.Text;

namespace e_conomicTest
{
    class Invoice
    {
        public int BookedInvoiceNumber { get; set; }
        public Customer Customer { get; set; }
        public string DueDate { get; set; }
        public decimal RemainderInBaseCurrency { get; set; }
    }
}
