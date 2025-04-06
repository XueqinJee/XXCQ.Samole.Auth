using AuthWebServer.Config;
using AuthWebServer.ViewModel;
using AutoMapper;
using ClassLibrary.Dto;
using ClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace AuthWebServer.Controllers {

    [ApiController]
    [Route("[Controller]")]
    public class MenuController(
        IMenuService _menuService,
        IMapper _mapper) : ControllerBase {


        [HttpGet("get/{id}")]
        public async Task<Result> GetMenyById(int id) {
            var data = await _menuService.GetByIdAsync(id);
            return Result.Success(data);
        }

        [HttpGet("tree")]
        public async Task<Result> GetMenuTree() {
            var data = await _menuService.GetMenuTreeALl();
            return Result.Success(data);
        }

        [HttpPost("add")]
        public async Task<Result> Add([FromBody]MenuViewModel model) {

            var target = _mapper.Map<Menu>(model);
            var result = await _menuService.AddAsync(target);
            if (result) {
                return Result.Success("添加成功！");
            }

            return Result.Error("添加失败");
        }

        [HttpPost("edit/{id}")]
        public async Task<Result> Update([FromQuery] int id, [FromBody]MenuViewModel model) {

            var target = _mapper.Map<Menu>(model);
            var result = await _menuService.UpdateAsync(target);
            if (result) {
                return Result.Success("修改成功！");
            }
            return Result.Error("修改失败");
        }

        [HttpDelete("delete/{id}")]
        public async Task<Result> Delete([FromRoute]int id) {
            var result = await _menuService.DeleteAsync(id);

            if (result) {
                return Result.Success("删除成功");
            }
            return Result.Error("删除失败");
        }
    }
}
