using Mango.Web.Models;
using Mango.Web.Services.IService;
using Mango.Web.Utility;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                ApiUrl = SD.CouponAPIBase + "/api/coupon",
                Data = couponDto
            });
        }

        public async Task<ResponseDto?> DeleteCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.DELETE,
                ApiUrl = SD.CouponAPIBase + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                ApiUrl = SD.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponByCodeAsync(string code)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                ApiUrl = SD.CouponAPIBase + "/api/coupon/GetByCode/" + code
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                ApiUrl = SD.CouponAPIBase + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                ApiUrl = SD.CouponAPIBase + "/api/coupon/",
                Data = couponDto
            });
        }
    }
}
