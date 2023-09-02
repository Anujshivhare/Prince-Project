using Mango.Web.Models;
using Mango.Web.Services.IService;
using Mango.Web.Utility;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                ApiUrl = SD.ProductAPIBase + "/api/product",
                Data = productDto
            });
        }

        public async Task<ResponseDto?> DeleteProductByIdAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.DELETE,
                ApiUrl = SD.ProductAPIBase + "/api/product/" + productId
            });
        }

        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                ApiUrl = SD.ProductAPIBase + "/api/product"
            });
        }

        public async Task<ResponseDto?> GetProductByIdAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                ApiUrl = SD.ProductAPIBase + "/api/product/" + productId
            });
        }

        public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                ApiUrl = SD.ProductAPIBase + "/api/product/",
                Data = productDto
            });
        }
    }
}
