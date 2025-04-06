using ClassLibrary.Base;
using ClassLibrary.Dto;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public interface IRoleService : IBaseGenericRepository<Role>
    {
        Task<RoleDto> GetRoleByInfo(int id);
        Task<bool> AddRoleAndPermissions(RoleDto dto);
        Task<bool> UpdateRoleAndPemissions(RoleDto dto);

        Task<bool> DeleteRoleById(int id);
    }
}
