using Model.Entity;
using System.ComponentModel.DataAnnotations;

namespace AuthWebServer.ViewModel {
    public class UserViewModel : BaseEntity {

        [Required]
        public string Name { get; set; }
        [Required]
        public string LoginName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }
    }
}
