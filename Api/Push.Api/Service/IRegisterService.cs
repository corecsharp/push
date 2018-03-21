using Push.Api;
using Push.Api.DTOs;
using Sherlock.Framework;

namespace Push.Api.Service
{
    public interface IRegisterService:IDependency
    {
        ErrCode Register(RegisterRequestDto dto, out string retMsg);

        ErrCode Unregister(UnregisterRequestDto dto, out string retMsg);
        
    }
}
