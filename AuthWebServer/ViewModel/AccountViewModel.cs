using System.ComponentModel.DataAnnotations;

namespace AuthWebServer.ViewModel {
    public class AccountViewModel {

        [Required(ErrorMessage = "账号不可为空！")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不可为空！")]
        public string Password { get; set; }
    }
}
