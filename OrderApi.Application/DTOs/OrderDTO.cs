using System.ComponentModel.DataAnnotations;
namespace OrderApi.Application.DTOs
{
    public record OrderDTO
    (int id, [Required ,
     Range(1,int.MaxValue)] int PrdouctId,
     [Required, Range(1, int.MaxValue)] int ClientId,
     [Required, Range(1, int.MaxValue)] int PurchaseQuantity,
     DateTime OrderDate
     );
        
    

   // public record ProductDTO
   //(
   //    int Id,
   //    [Required] string name,
   //    [Required, Range(0, int.MaxValue)] int Quantity,
   //    [Required, DataType(DataType.Currency)] decimal Price
   //);
}
