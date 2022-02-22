using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary
{
    public class MovieOrder
    {
        public int Id { get; set; }
        public string OrderName { get; set; } = string.Empty;
        public int MovieId { get; set; }
        public OrderState State { get; set; } = OrderState.UnSubmitted;
    }
}
