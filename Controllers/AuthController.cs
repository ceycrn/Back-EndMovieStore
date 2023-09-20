using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IUserService _userService;
        DateTime logTimestamp = DateTime.Now;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            string logFolderPath = "Logs"; // "Logs" klasörünün adını kullanın
            string logFileName = "myapplog.json"; // Log dosyasının adı
            //object olacak 
            string logFilePath = Path.Combine(logFolderPath, logFileName);

            var logData = new
            {
                UserMail = userToLogin.Data.Email, // Kullanıcı adını ekleyebilirsiniz
                Success = true,
                Messages.UserRegistered, // const olarak kalacak
                LogTimestamp = logTimestamp
        }; 

            var jsonLog = JsonConvert.SerializeObject(logData);

            System.IO.File.AppendAllText(logFilePath, jsonLog + Environment.NewLine); // Environment.NewLine ile yeni satır ekleyin
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data+ result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            if (registerResult.Success)
            {
                var result = _authService.CreateAccessToken(registerResult.Data);
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                
            }
            return BadRequest(Messages.EmailValidation);    
        }

            

        [HttpPost("deleteuser")]
        public IActionResult Delete(int id)
        {
            var result = _userService.delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}