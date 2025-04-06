using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Dto
{
    public class RoleDto : BaseEntity
    {
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public int[] MenuIds { get; set; } = new int[] { };
    }
}
