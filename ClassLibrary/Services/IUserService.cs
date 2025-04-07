using ClassLibrary.Base;
using ClassLibrary.Dto;
using ClassLibrary.Dto.Page;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public interface IUserService : IBaseGenericRepository<User>
    {

        Task<PageData<UserDto>> GetUserPageData(BaseQueryPage page);
    }
}
