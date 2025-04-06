using AutoMapper;
using ClassLibrary.Base;
using ClassLibrary.Dto;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public class RoleService : BaseGenericRepository<Role>, IRoleService {

        private readonly IMapper _mapper;
        public RoleService(MyDbContext myDbContext, IMapper mapper) : base(myDbContext) {
            this._mapper = mapper;
        }

        public async Task<bool> AddRoleAndPermissions(RoleDto dto) {

            var role = _mapper.Map<Role>(dto);
            role.Id = 0;
            _myDbContext.Role.Add(role);
            _myDbContext.SaveChanges();
            // 增加权限
            foreach (var item in dto.MenuIds!) {
                var permission = new RoleToMenu();
                permission.RoleId = role.Id;
                permission.PermissionId = item;
                _myDbContext.RoleToMenu.Add(permission);
            }
            return await _myDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRoleById(int id) {
            var roleUserCount = _myDbContext.User.Where(x => x.RoleId == id).Count();
            if(roleUserCount != 0) {
                throw new Exception("当前角色已被用户关联，删除失败！");
            }
            var role = _myDbContext.Role.FirstOrDefault(x => x.Id == id);
            if(role == null) {
                throw new Exception("当前角色不存在！");
            }
            var permissions = await _myDbContext.RoleToMenu.Where(x => x.RoleId == id).ToListAsync();
            _myDbContext.RoleToMenu.RemoveRange(permissions);
            _myDbContext.Role.Remove(role);
            return await _myDbContext.SaveChangesAsync() > 0;
        }

        public async Task<RoleDto> GetRoleByInfo(int id) {
            var role = await _myDbContext.Role.FirstOrDefaultAsync(x => x.Id == id);
            if(role == null) {
                throw new Exception($"角色ID {nameof(id)} 不存在");
            }

            var dto = _mapper.Map<RoleDto>(role);
            var permissions = await _myDbContext.RoleToMenu.Where(x => x.RoleId == role.Id)
                .Select(x => x.PermissionId).ToArrayAsync();

            dto.MenuIds = permissions;
            return dto;
        }

        public async Task<bool> UpdateRoleAndPemissions(RoleDto dto) {
            var role = _myDbContext.Role.FirstOrDefault(x => x.Id == dto.Id);
            if (role == null) {
                throw new Exception("当前角色不存在！");
            }

            var permissions = await _myDbContext.RoleToMenu.Where(x => x.RoleId == dto.Id).ToListAsync();
            _myDbContext.RoleToMenu.RemoveRange(permissions);
            // 增加权限
            foreach (var item in dto.MenuIds!) {
                var permission = new RoleToMenu();
                permission.RoleId = role.Id;
                permission.PermissionId = item;
                _myDbContext.RoleToMenu.Add(permission);
            }
            role.RoleName = dto.RoleName;
            role.Description = dto.Description;
            role.UpdateTime = DateTime.Now;
            _myDbContext.Role.Update(role);
            return await _myDbContext.SaveChangesAsync() > 0;
        }
    }
}
