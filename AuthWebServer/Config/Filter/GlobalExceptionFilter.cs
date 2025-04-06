using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace AuthWebServer.Config.Filter {
    public class GlobalExceptionFilter(
        ILogger<GlobalExceptionFilter> _logger): IExceptionFilter {
        public void OnException(ExceptionContext context) {

            // 指定异常已经处理
            context.ExceptionHandled = true;

            _logger.LogError(context.Exception.ToString());
            context.Result = new JsonResult(Result.Error(context.Exception.Message));
        }
    }
}
