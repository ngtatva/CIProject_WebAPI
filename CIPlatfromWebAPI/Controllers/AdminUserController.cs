using Business_logic_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : Controller
    {
        private readonly BALAdminUser _adminUser;
        public AdminUserController(BALAdminUser adminUser)
        {
            _adminUser = adminUser;
        }

        [HttpGet("UserDetailList")]
        public IActionResult GetUserDetailList()
        {
            try
            {
                var userDetailList = _adminUser.UserDetailList();
                return Ok(new ResponseResult { Data= userDetailList, Result=ResponseStatus.Success });
            }
            catch(Exception ex) 
            {
                return StatusCode(500, new ResponseResult { Result = ResponseStatus.Error, Message = ex.Message });
            }
        }

        [HttpDelete("DeleteUserAndUserDetail/{userId}")]
        public IActionResult DeleteUserAndUserDetail(int userId)
        {
            try
            {
                var result = _adminUser.DeleteUserAndUserDetail(userId);
                return Ok(new ResponseResult { Data = result, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult { Result = ResponseStatus.Error, Message = ex.Message });
            }
        }
    }
}
