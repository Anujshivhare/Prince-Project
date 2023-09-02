using Mango.Web.Models;

namespace Mango.Web.Services.IService
{
    public interface IBaseService
    {
        public Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true); 
    }
}
