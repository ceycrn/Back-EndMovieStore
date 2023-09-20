using Business.Abstract;

using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly ILogger<AuthManager> _logger;


        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ILogger<AuthManager> logger)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _logger = logger;

        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                Budget = userForRegisterDto.Budget,
            };

            if (!IsValidEmail(userForRegisterDto.Email))
            {
                return new ErrorDataResult<User>("Geçersiz e-posta adresi");
            }
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
              
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            // Kullanıcının girdiği tuzu alın
            var enteredSalt = userToCheck.PasswordSalt;

            // Kullanıcının girdiği şifrenin, veritabanında saklanan şifre hash'ine ve tuzuna uyup uymadığını doğrulayın
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, enteredSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

         
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public static string ConvertSaltToBase64(byte[] salt)
        {
            return Convert.ToBase64String(salt);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
              
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


    }
}