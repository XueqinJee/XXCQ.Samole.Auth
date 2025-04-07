using AutoMapper;
using ClassLibrary.Base;
using ClassLibrary.Dto;
using ClassLibrary.Dto.Page;
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
    public class UserService : BaseGenericRepository<User>, IUserService {

        private readonly IMapper _mapper;
        public UserService(MyDbContext myDbContext, IMapper mapper) : base(myDbContext) {
            _mapper = mapper;
        }

        public async Task<PageData<UserDto>> GetUserPageData(BaseQueryPage page) {
            var query = _myDbContext.User.AsQueryable();
            if (!string.IsNullOrEmpty(page.q)) {
                query = query.Where(x => x.Name!.Contains(page.q) || x.LoginName!.Contains(page.q));
            }

            query = query.Include(x => x.Role);
            var result = await GetPaginatedAsync(query, page);

            var data = result.Data?.Select(x => {
                var map = _mapper.Map<UserDto>(x);
                map.roleName = x.Role?.RoleName;
                return map;
            }).ToList();

            var mapResult = new PageData<UserDto>();
            mapResult.Data = data;
            mapResult.Total = result.Total;
            mapResult.TotalPage = result.TotalPage;
            mapResult.PageSize = result.PageSize;
            mapResult.CurrentPage = result.CurrentPage;
            return mapResult;
        }
    }
}
