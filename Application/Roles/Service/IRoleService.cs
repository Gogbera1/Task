using Application.Roles.Request;
using Application.Roles.Response;
using Common.Enums;
using Common.Resultwrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles.Service
{
    public interface IRoleService
    {
        Task<ResultWrapper<ResultCodeEnum>> SetPermissionToRole(SetRolePermissionRequestModel model);
        Task<ResultWrapper<List<RoleResponseModel>>> GetRoles(string[] roleNames);
        Task<ResultWrapper<ResultCodeEnum>> CreateRole(RoleRequestModel model);
    }
}
