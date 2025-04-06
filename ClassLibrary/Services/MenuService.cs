using AutoMapper;
using ClassLibrary.Base;
using ClassLibrary.Dto;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entity;

namespace ClassLibrary.Services {
    public class MenuService : BaseGenericRepository<Menu>, IMenuService, IBaseGenericRepository<Menu> {

        private readonly IMapper _mapper;
        public MenuService(MyDbContext myDbContext, IMapper mapper) : base(myDbContext) {
            _mapper = mapper;
        }

        public async Task<List<MenuDto>> GetMenuTreeALl() {
            var datas = await GetAllAsync();
            var top = datas.Where(x => x.ParentId == 0).ToList();
            List<MenuDto> ls = new List<MenuDto>();
            foreach (var item in top) {
                var dto = _mapper.Map<MenuDto>(item);
                MenuChildrenMapper(dto, datas.ToList());
                ls.Add(dto);
            }
            ls = ls.OrderBy(x => x.SortOrder).ToList();
            return ls;
        }

        /// <summary>
        /// 获取角色菜单-排除已禁用的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetMenuTreeByRole(int roleId) {
            var datas = await _myDbContext.Menu.Where(x => x.State != Model.Enums.MenuStateEnum.禁用).ToListAsync();
            var top = datas.Where(x => x.ParentId == 0).ToList();

            var menuIds = _myDbContext.RoleToMenu.Where(x => x.RoleId == roleId)
                .Select(x => x.PermissionId).ToList();

            List<MenuDto> ls = new List<MenuDto>();
            foreach (var item in top) {
                if (!menuIds.Contains(item.Id)) {
                    continue;
                }

                var dto = _mapper.Map<MenuDto>(item);
                MenuChildrenMapper(dto, datas.ToList(), menuIds);
                ls.Add(dto);
            }
            ls = ls.OrderBy(x => x.SortOrder).ToList();
            return ls;
        }

        private void MenuChildrenMapper(MenuDto menu, List<Menu> menus, List<int>? menuIds = null) {
            var top = menus.Where(x => x.ParentId == menu.Id).ToList();
            List<MenuDto> ls = new List<MenuDto>();
            foreach (var item in top) {
                if(menuIds != null && !menuIds.Contains(item.Id)) {
                    continue;
                }

                var dto = _mapper.Map<MenuDto>(item);
                MenuChildrenMapper(dto, menus, menuIds);
                ls.Add(dto);
            }
            ls = ls.OrderBy(x => x.SortOrder).ToList();
            menu.Children = ls;
        }
    }
}
