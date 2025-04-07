using AuthWebServer.Config;
using AuthWebServer.ViewModel;
using AutoMapper;
using ClassLibrary.Dto;
using ClassLibrary.Dto.Page;
using ClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebServer.Controllers {
    [ApiController]
    [Route("[Controller]")]
    public class RoleController (
        IRoleService _roleService,
        IMenuService _menuService,
        IMapper _mapper): ControllerBase{

        [HttpGet("get")]
        public async Task<Result> GetAll() {
            var result = await _roleService.GetAllAsync();
            return Result.Success(result);
        }


        [HttpGet("page")]
        public async Task<Result> GetPage([FromQuery]BaseQueryPage page) {
            var query = await _roleService.GetPaginatedAsync(page, options => options.RoleName.Contains(page.q));

            return Result.Success(query);
        }

        [HttpGet("get/{id}")]
        public async Task<Result> GetRoleById(int id) {
            var data = await _roleService.GetRoleByInfo(id);

            return Result.Success(data);
        }

        [HttpGet("menu")]
        public async Task<Result> GetRoleMenu() {
            var data = await _menuService.GetMenuTreeByRole(1);
            return Result.Success(data);
        }

        [HttpPost("add")]
        public async Task<Result> Add([FromBody]RoleViewModel model) {
            var dto = _mapper.Map<RoleDto>(model);

            var result = await _roleService.AddRoleAndPermissions(dto);
            if (result) {
                return Result.Success("添加成功");
            }
            return Result.Success("添加失败");
        }

        [HttpPost("edit")]
        public async Task<Result> Edit([FromBody] RoleViewModel model) {
            var dto = _mapper.Map<RoleDto>(model);

            var result = await _roleService.UpdateRoleAndPemissions(dto);
            if (result) {
                return Result.Success("修改成功");
            }
            return Result.Success("修改失败");
        }

        [HttpDelete("delete/{id}")]
        public async Task<Result> Delete([FromRoute]int id) {
            var result = await _roleService.DeleteRoleById(id);
            if (result) {
                return Result.Success("删除成功");
            }
            return Result.Success("删除失败");
        }
    }
}
