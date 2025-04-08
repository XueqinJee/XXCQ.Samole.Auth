using Microsoft.AspNetCore.Authorization;

namespace AuthWebServer.Config.AuthorizeConfig {

    /// <summary>
    /// Handler 做实际权限处理
    /// </summary>
    public class PermissionAuthorizeHandler : AuthorizationHandler<PermissionRequirement> {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement) {

            context.Fail();

            return Task.CompletedTask;
        }
    }
}
