using Business_logic_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly BALCommon _balCommon;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CommonController(BALCommon balCommon, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _balCommon = balCommon;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("CountryList")]
        [Authorize]
        public async Task<IActionResult> CountryList()
        {
            try
            {
                var result = await _balCommon.GetCountryListAsync();
                return Ok(new { status = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Error", message = ex.Message });
            }
        }

        [HttpGet]
        [Route("CityList/{countryId}")]
        [Authorize]
        public async Task<IActionResult> CityList(int countryId)
        {
            try
            {
                var result = await _balCommon.GetCityListAsync(countryId);
                return Ok(new { status = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Error", message = ex.Message });
            }
        }

        [HttpGet]
        [Route("MissionCountryList")]
        public async Task<IActionResult> MissionCountryList()
        {
            try
            {
                var result = await _balCommon.GetMissionCountryListAsync();
                return Ok(new { status = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Error", message = ex.Message });
            }
        }

        [HttpGet]
        [Route("MissionCityList")]
        public async Task<IActionResult> MissionCityList()
        {
            try
            {
                var result = await _balCommon.GetMissionCityListAsync();
                return Ok(new { status = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Error", message = ex.Message });
            }
        }

        [HttpGet]
        [Route("MissionThemeList")]
        public async Task<IActionResult> MissionThemeList()
        {
            try
            {
                var result = await _balCommon.GetMissionThemeListAsync();
                return Ok(new { status = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Error", message = ex.Message });
            }
        }

        [HttpGet]
        [Route("MissionSkillList")]
        public async Task<IActionResult> MissionSkillList()
        {
            try
            {
                var result = await _balCommon.GetMissionSkillListAsync();
                return Ok(new { status = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Error", message = ex.Message });
            }
        }

        [HttpGet]
        [Route("MissionTitleList")]
        public async Task<IActionResult> MissionTitleList()
        {
            try
            {
                var result = await _balCommon.GetMissionTitleListAsync();
                return Ok(new { status = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Error", message = ex.Message });
            }
        }

        [HttpPost]
        [Route("UploadImage")]
        [Authorize]
        public async Task<IActionResult> UploadImage([FromForm] UploadFile upload)
        {
            string filePath = "";
            string fullPath = "";
            List<string> fileList = new List<string>();
            var files = Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    filePath = Path.Combine("UploadMissionImage", upload.ModuleName);
                    string fileRootPath = Path.Combine(_hostingEnvironment.WebRootPath, "UploadMissionImage", upload.ModuleName);

                    if (!Directory.Exists(fileRootPath))
                    {
                        Directory.CreateDirectory(fileRootPath);
                    }

                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string extension = Path.GetExtension(fileName);
                    string fullFileName = name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                    fullPath = Path.Combine(filePath, fullFileName);
                    string fullRootPath = Path.Combine(fileRootPath, fullFileName);
                    using (var stream = new FileStream(fullRootPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    fileList.Add(fullPath);
                }
            }
            return Ok(new { success = true, Data = fileList });
        }

        [HttpGet]
        [Route("GetUserSkill/{userId}")]
        [Authorize]
        public ResponseResult GetUserSkill(int userId)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                result.Data = _balCommon.GetUserSkill(userId);
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
