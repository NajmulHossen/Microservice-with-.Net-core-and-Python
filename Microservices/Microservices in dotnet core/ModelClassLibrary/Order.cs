using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary
{
    public enum OrderState
    {
        UnSubmitted = 0,
        Submitted = 1,
        Accepted = 2,
        Unaccepted =3
    }
    public class Order
    {
        public int Id { get; set; }
        public string OrderName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public uint Quantity { get; set; } = 0;
        public OrderState State{ get; set; } = OrderState.UnSubmitted;
    }
    
}
