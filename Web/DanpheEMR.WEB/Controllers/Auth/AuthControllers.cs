using DanpheEMR.Application.Features.Auth.Commands.CreateUserAccount;
using DanpheEMR.Application.Features.Auth.Queries.AuthenticateUser;
using DanpheEMR.Application.Features.Auth.Queries.GetMyProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanpheEMR.WEB.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthControllers : ApiControllerBase
    {
        //register
        [HttpGet("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserAccountCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserQuery query)
        {
            var result = await Mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result);
            }
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
