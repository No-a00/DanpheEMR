using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace DanpheEMR.WEB.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RequirePermissionAttribute : TypeFilterAttribute
    {
        public RequirePermissionAttribute(string resource, string action)
            : base(typeof(RequirePermissionFilter))
        {
            Arguments = new object[] { resource, action };
        }
    }
    public class RequirePermissionFilter : IAuthorizationFilter
    {
        private readonly string _resource;
        private readonly string _action;

        public RequirePermissionFilter(string resource, string action)
        {
            _resource = resource;
            _action = action;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Kiểm tra xem đã đăng nhập chưa) 
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult(); //401
                return;
            }

            // Bóc tách danh sách quyền từ Token  
            var userPermissions = user.Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToList();

           
            bool hasPermission = false;


            if (userPermissions.Contains($"{_resource}:{_action}"))
            {
                hasPermission = true;
            }
           
            else if (userPermissions.Contains($"{_resource}:Full"))
            {
                hasPermission = true;
            }
           
            else if (userPermissions.Contains("Admin:Full"))
            {
                hasPermission = true;
            }

            if (!hasPermission)
            {
                context.Result = new JsonResult(new
                {
                    Message = $"Bạn không có quyền {_action} trên phân hệ {_resource}.",
                    ErrorCode = "Auth.Forbidden"
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
}