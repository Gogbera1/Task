using Application.Jwt.Request;
using Application.User.Request;
using Application.User.Response;
using Common.Enums;
using Common.Resultwrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.User.Service
{
    public interface IUserService
    {
        Task<ResultWrapper<TokenModel>> Login(UserLoginModel loginUser);
        Task<ResultWrapper<ResultCodeEnum>> DeleteUser(string id);
        Task<ResultWrapper<UserResponseModel>> GetUserById(string id);
        Task<ResultWrapper<List<UserResponseModel>>> GetUsers();
        Task<ResultWrapper<ResultCodeEnum>> CreateUser(UserRequestModel model);
        Task<ResultWrapper<ResultCodeEnum>> UpdateUser(string id, UpdateUserRequestModel model);
    }
}
