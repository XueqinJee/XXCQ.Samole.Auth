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
    public interface IMenuService : IBaseGenericRepository<Menu>
    {
        Task<List<MenuDto>> GetMenuTreeALl();
        Task<List<MenuDto>> GetMenuTreeByRole(int roleId);
    }
}
