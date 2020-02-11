using System;
using System.Collections.Generic;
using System.Text;

namespace e_conomicTest
{
    class InvoiceResponse
    {
        public List<Invoice> Collection { get; set; }
        public Object Pagination { get; set; }
        public Object Self { get; set; }
    }
}
