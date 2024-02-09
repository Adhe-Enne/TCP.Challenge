using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Model.Enums
{
   public enum InvoiceStatus
    {
        NEW,
        PENDING,
        ON_PROCESS,
        ON_REVISION,
        APROVED,
        SENDED_TO_CLIENT,
        OUTSTANDING,
        PARCIAL_PAYMENT,
        FULL_PAYMENT,
        FINISHED,
        EXPIRED,
        REJECTED,
        CANCELED,
        DELETED
    }
}
