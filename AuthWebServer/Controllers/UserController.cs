using AuthWebServer.Config;
using AuthWebServer.ViewModel;
using AutoMapper;
using ClassLibrary.Dto.Page;
using ClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace AuthWebServer.Controllers {

    [ApiController]
    [Route("[Controller]")]
    public class UserController(
        IUserService _userService,
        IMapper _mapper) : ControllerBase{

        [HttpGet("page")]
        public async Task<Result> Page([FromQuery]BaseQueryPage page) {
            if(page.q == null) {
                page.q = "";
            }
            var query = await _userService.GetUserPageData(page);

            return Result.Success(query);
        }

        [HttpGet("get/{id}")]
        public async Task<Result> GetRoleById(int id) {
            var data = await _userService.GetByIdAsync(id);

            return Result.Success(data);
        }

        [HttpPost("add")]
        public async Task<Result> Add(UserViewModel model) {

            var user = _mapper.Map<User>(model);
            var result = await _userService.AddAsync(user);
            if (result) {
                return Result.Success("添加成功！");
            }
            return Result.Error("添加失败");
        }

        [HttpPost("edit")]
        public async Task<Result> Edit(UserViewModel model) {

            var user = _mapper.Map<User>(model);
            var result = await _userService.UpdateAsync(user);
            if (result) {
                return Result.Success("修改成功");
            }
            return Result.Error("修改失败");
        }

        [HttpDelete("delete/{id}")]
        public async Task<Result> Delete(int id) {

            var result = await _userService.DeleteAsync(id);
            if (result) {
                return Result.Success("删除成功");
            }
            return Result.Error("删除失败");
        }
    }
}
