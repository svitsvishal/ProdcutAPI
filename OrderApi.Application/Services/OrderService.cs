using OrderApi.Application.DTOs;
using OrderApi.Application.DTOs.Conversions;
using OrderApi.Application.Interfaces;
using Polly;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Services
{
    public class OrderService(IOrder IoderInterface, HttpClient http,
       ResiliencePipelineProvider <string> resiliencePipeline)  : IOrderService
    {

        public async Task<ProductDTO>GetProduct(int  productid)
        {
            //Call product Api here
            //Redirect this call to Api gateway

            var getProduct = await http.GetAsync($"/api/products/{productid}");
            if (!getProduct.IsSuccessStatusCode)
                return null;

            var product =await getProduct.Content.ReadFromJsonAsync<ProductDTO>();
            return product!;

        }

        public async Task<AppUserDTO>GetUser(int userid)
        {

            var getUser = await http.GetAsync($"/api/products/{userid}");
            if (!getUser.IsSuccessStatusCode)
                return null;

            var product = await getUser.Content.ReadFromJsonAsync<AppUserDTO>();
            return product!;


        }       

    public  async Task<OrderDetailsDTO> GetOrderDetails(int orderId)
        {
           var order  = await IoderInterface.FindByIdAsync(orderId);
            if (order is null || order!.Id < 0)
                return null!;

            var retryPipeline = resiliencePipeline.GetPipeline("my-retry-pipelin");

            // prepare product
            var productDto = await retryPipeline.ExecuteAsync(async token => await GetProduct(order.PrdouctId));
            //prepare client

            var appUserDTO = await retryPipeline.ExecuteAsync(async token => await GetUser(order.ClientId));

            return new OrderDetailsDTO(
                order.Id,
                productDto.Id,
                appUserDTO.id,
                 appUserDTO.Name,
                  appUserDTO.Email,
                 appUserDTO.Addres,
                    appUserDTO!.TelephoneNumber,
                    productDto.Name,
                    order.PurchaseQuantity,
                    productDto.Price,
                    productDto.Quantity * order.PurchaseQuantity,
                    order.OrderDate);

        }

        public async Task<IEnumerable<OrderDTO>> GetOrderByClientId(int clientId)
        {
            var orders = await IoderInterface.GetOrdersAsync (o=>o.ClientId == clientId);
            if(!orders.Any() ) return null!;

            var (_, _orders) = OrderConversions.FromEntity(null, orders);
            return _orders!;

        }

      
    }
}
