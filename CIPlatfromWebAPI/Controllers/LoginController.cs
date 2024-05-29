using Business_logic_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {       
        private readonly BALLogin _balLogin;
        ResponseResult result = new ResponseResult();
        public LoginController(BALLogin balLogin)
        {           
            _balLogin = balLogin;
        }

        [HttpPost]
        [Route("LoginUser")]
        public ResponseResult LoginUser(LoginRequest loginRequest)
        {
            try
            {                                
                result.Data = _balLogin.LoginUser(loginRequest);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
