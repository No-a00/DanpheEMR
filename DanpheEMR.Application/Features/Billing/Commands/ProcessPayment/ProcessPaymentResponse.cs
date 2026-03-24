using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Billing.Commands.ProcessPayment
{
    public class ProcessPaymentResponse
    {
        public Guid PaymentId;
        public string NewInvoiceStatus;
    }
}
