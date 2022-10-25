using Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MeamaTask.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult Error(ResultCodeEnum resultCode)
        {
            try
            {
                var message = resultCode.ToString();
                var code = Convert.ToInt32(resultCode.ToString().Substring(4, 3));

                return StatusCode(code, message);
            }
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ResultCodeEnum.Code500InternalServerError.ToString());
            }
        }
    }
}
