using Application.Roles.Repository;
using Application.Roles.Request;
using Application.Roles.Response;
using Application.Roles.Service;
using Common.Enums;
using Common.Resultwrapper;
using Domain.Poco;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles.ServiceImpl
{
    public class RoleService : IRoleService
    {
        private RoleManager<IdentityRole> _roleManager;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RoleService(RoleManager<IdentityRole> roleManager, IRolePermissionRepository rolePermissionRepository)
        {
            _roleManager = roleManager;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<ResultWrapper<ResultCodeEnum>> CreateRole(RoleRequestModel model)
        {
            var role = await _roleManager.FindByNameAsync(model.Name);
            if (role != null)
                return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code400RoleAlreadyExist };
            var entity = new IdentityRole
            {
                Name = model.Name.ToLower()
            };
            await _roleManager.CreateAsync(entity);
            return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code200Success };
        }

        public async Task<ResultWrapper<List<RoleResponseModel>>> GetRoles(string[] roleNames)
        {
            var roles = new List<IdentityRole>();
            foreach (var item in roleNames)
            {
                var role = await _roleManager.FindByNameAsync(item.ToLower());
                if (role == null)
                {
                    continue;
                }
                roles.Add(role);
            }
            if(!roles.Any())
                return new ResultWrapper<List<RoleResponseModel>> { Status = ResultCodeEnum.Code404NotFound };
            var result = roles.Adapt<List<RoleResponseModel>>();
            return new ResultWrapper<List<RoleResponseModel>> { Status = ResultCodeEnum.Code200Success, Value = result };
        }

        public async Task<ResultWrapper<ResultCodeEnum>> SetPermissionToRole(SetRolePermissionRequestModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
                return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code404NotFound };
            foreach (var item in model.PermissionIds)
            {
                var rolePermission = new RolePermission
                {
                    RoleId = model.RoleId,
                    PermissionId = item
                };
                await _rolePermissionRepository.AddAsync(rolePermission);
            }
            return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code200Success };
        }
    }
}
