using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary
{
    public class AcceptedOrder
    {
        public int Id { get; set; }
        public string OrderName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public uint Quantity { get; set; } = 0;
    }
}
