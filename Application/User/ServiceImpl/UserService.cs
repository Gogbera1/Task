using Application.Jwt.Request;
using Application.Jwt.Service;
using Application.User.Repositories;
using Application.User.Request;
using Application.User.Response;
using Application.User.Service;
using Common.Enums;
using Common.Resultwrapper;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.User.ServiceImpl
{
    public class UserService : IUserService
    {
        #region Injection
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructor
        public UserService(UserManager<IdentityUser> userManager, ITokenService tokenService, 
                           RoleManager<IdentityRole> roleManager, IUserRepository userRepository, 
                           SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }
        #endregion

        #region Methods

        public async Task<ResultWrapper<TokenModel>> Login(UserLoginModel loginUser)
        {
            var User = await _userManager.FindByEmailAsync(loginUser.Email);
            if (User == null)
                return new ResultWrapper<TokenModel>
                {
                    Status = ResultCodeEnum.Code404UserNotFound
                };
            var signIn =await _signInManager.PasswordSignInAsync(User, loginUser.Password, false, false);
            if (!signIn.Succeeded)
                return new ResultWrapper<TokenModel>
                {
                    Status = ResultCodeEnum.Code401Unauthorized
                };
            var token = await GenerateToken(User);
            return new ResultWrapper<TokenModel>
            {
                Status = ResultCodeEnum.Code200Success,
                Value = token
            };
        }

        public async Task<ResultWrapper<ResultCodeEnum>> CreateUser(UserRequestModel model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User != null)
                return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code400UserAlreadyExist };
            var userEntity = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            await _userManager.CreateAsync(userEntity, model.Password);
            return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code200Success };
        }

        public async Task<ResultWrapper<UserResponseModel>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new ResultWrapper<UserResponseModel> { Status = ResultCodeEnum.Code404UserNotFound };
            var result = user.Adapt<UserResponseModel>();
            return new ResultWrapper<UserResponseModel>
            {
                Status = ResultCodeEnum.Code200Success,
                Value = result
            };
        }

        public async Task<ResultWrapper<List<UserResponseModel>>> GetUsers()
        {
            var users = _userRepository.GetAll().ToList();
            if(!users.Any())
                return new ResultWrapper<List<UserResponseModel>> { Status = ResultCodeEnum.Code404UserNotFound };
            var result = users.Adapt<List<UserResponseModel>>();
            return new ResultWrapper<List<UserResponseModel>>
            {
                Status = ResultCodeEnum.Code200Success,
                Value = result
            };
        }

        public async Task<ResultWrapper<ResultCodeEnum>> UpdateUser(string id, UpdateUserRequestModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code404UserNotFound };
            user.Email = model.Email;
            user.UserName = model.UserName;
            await _userManager.UpdateAsync(user);
            return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code200Success };
        }
        public async Task<ResultWrapper<ResultCodeEnum>> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code404UserNotFound };
            await _userManager.DeleteAsync(user);
            return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code200Success };

            //ვერ მოვასწარი ჩემი იუზერი გამეკეთებინა და სოფტ დელეიტე დამესვა ტასკები სოფტით მაქვს გაკეთებული
        }

        #region Token

        private async Task<TokenModel> GenerateToken(IdentityUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var roleForId = await _roleManager.FindByNameAsync(role);
            //var roleId =await _roleManager.GetRoleIdAsync(new IdentityRole { Name = role,NormalizedName = role.ToUpper()});
            var permissions =await _userRepository.GetUserPermisions(roleForId.Id);
          
            var claims = new Dictionary<string, string>()
        {
            {"email", user.Email},
            {"role", role},
        };

            var accessToken = _tokenService.GenerateToken(user.Id, claims, permissions.Select(x => x.PermissionName).ToList());
            return (accessToken.Value);
        }
        #endregion
        #endregion
    }
}
