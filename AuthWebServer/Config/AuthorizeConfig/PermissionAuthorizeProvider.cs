using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AuthWebServer.Config.AuthorizeConfig {

    /// <summary>
    /// 权限处理程序
    /// </summary>
    public class PermissionAuthorizeProvider : DefaultAuthorizationPolicyProvider,IAuthorizationPolicyProvider {
        public PermissionAuthorizeProvider(IOptions<AuthorizationOptions> options) : base(options) {
        }

        public async new Task<AuthorizationPolicy> GetDefaultPolicyAsync() {
            return await base.GetDefaultPolicyAsync();
        }

        public async new Task<AuthorizationPolicy> GetFallbackPolicyAsync() {
            return await base.GetFallbackPolicyAsync();
        }

        public async new Task<AuthorizationPolicy> GetPolicyAsync(string policyName) {
            if (policyName.EndsWith(PermissionAttribute._permission)) {
                var policy = new AuthorizationPolicyBuilder();
                var targetName = policyName.Substring(0, policyName.IndexOf(PermissionAttribute._permission));
                policy.AddRequirements(new PermissionRequirement(targetName));
                var build = policy.Build();

                return build;
            }
            return await base.GetPolicyAsync(policyName);
        }
    }
}
