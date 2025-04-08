using Microsoft.AspNetCore.Authorization;

namespace AuthWebServer.Config.AuthorizeConfig {
    public class PermissionAttribute : AuthorizeAttribute{
        public static string _permission = "_auth_permission";
        public string Per { get; set; }
        public PermissionAttribute(string per) {
            Per = per + _permission;
        }
    }
}
