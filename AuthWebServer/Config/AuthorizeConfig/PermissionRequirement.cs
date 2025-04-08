using Microsoft.AspNetCore.Authorization;

namespace AuthWebServer.Config.AuthorizeConfig {
    public class PermissionRequirement : IAuthorizationRequirement{
        public string? per { get; set; }
        public PermissionRequirement(string per) { this.per = per; }
    }
}
