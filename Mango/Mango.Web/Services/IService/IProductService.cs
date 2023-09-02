
using Mango.Web.Models;

namespace Mango.Web.Services.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetProductByIdAsync(int productId);
        Task<ResponseDto?> DeleteProductByIdAsync(int productId);
        Task<ResponseDto?> CreateProductAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
    }
}
