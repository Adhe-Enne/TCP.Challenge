﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Model.Dto
{
    public class InvoiceDetailCreateDto
    {
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
    }
}
