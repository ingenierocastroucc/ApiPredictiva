using SalesDatePrediction.Models.Models;

namespace SalesDatePrediction.Models
{
    public class CreateOrderRequest
    {
        public Orders Order { get; set; }
        public OrderDetails OrderDetails { get; set; }
    }
}
