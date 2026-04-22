using DanpheEMR.Application.Features.Auth.Commands.CreateUserAccount;
using DanpheEMR.Application.Features.Auth.Queries.AuthenticateUser;
using DanpheEMR.Application.Features.Auth.Queries.GetMyProfile;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers.Auth
{
    [Route("api/auth")]
    public class AuthController : ApiControllerBase
    {
        [HttpPost("register")]
        //[AllowAnonymous]
        [RequirePermission("Admin", "Write")]
        public async Task<IActionResult> Register([FromBody] CreateUserAccountCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.IsSuccess)
            {
               
                return Ok(result);
            }
            return BadRequest(result.Error);
        }

     
        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] AuthenticateUserQuery query)
        {
            var result = await Mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result);
        }


        [HttpGet("me")]
        [Authorize] 
        public async Task<IActionResult> GetMyProfile()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new { Message = "Token không hợp lệ hoặc đã hết hạn." });
            }

            var query = new GetMyProfileQuery(userId);
            var result = await Mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}