using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.DTOs.Conversions
{
    public static class OrderConversions
    {
        public static Order ToEntity(OrderDTO order) => new()
        {
            Id = order.id,
            ClientId = order.ClientId,
            PrdouctId  =order.PrdouctId,
            OrderDate = order.OrderDate,
            PurchaseQuantity = order.PurchaseQuantity,

        };

        public static (OrderDTO?, IEnumerable<OrderDTO>?) FromEntity(Order? order, IEnumerable<Order>?orders)
        {
            //
            if (order is not null || orders is null)
            {
                var singleOder = new OrderDTO (order!.Id,
                    order.ClientId ,
                    order.PrdouctId,
                    order.PurchaseQuantity ,
                    order.OrderDate);
                return (singleOder, null);
            }

            if (order is  null || orders is not null)
            {
                var _orders = orders!.Select(o =>
                new OrderDTO(o.Id, o.ClientId, o.PurchaseQuantity, o.PrdouctId, o.OrderDate));
                return (null, _orders);
            }
            return (null, null);


        }
    }
}
