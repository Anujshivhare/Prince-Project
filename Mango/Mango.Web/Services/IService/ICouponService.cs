using Mango.Web.Models;

namespace Mango.Web.Services.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponByIdAsync(int couponId);
        Task<ResponseDto?> GetCouponByCodeAsync(string code);
        Task<ResponseDto?> DeleteCouponByIdAsync(int couponId); 
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto); 

    }
}
