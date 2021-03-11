using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoulaCodingChallenge.Controllers
{
	/// <summary>
	/// This is a dummy login api to generate JWT token, based on an assumption that account info is private and need AA
	/// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/dummyAuth")]
	[ExcludeFromCodeCoverage]
	public class DummyLoginController : ControllerBase
    {
		[HttpPost]
        [Route("{userName}")]
        public async Task<IActionResult> Login([FromRoute] string userName)
        {
			var handler = new JsonWebTokenHandler();
			var now = DateTime.UtcNow;

			var descriptor = new SecurityTokenDescriptor
			{
				Issuer = "moula",
				Audience = "account",
				IssuedAt = now,
				NotBefore = now,
				Expires = now.AddMinutes(30),
				Subject = new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, userName) }),
				SigningCredentials = new SigningCredentials(DummyKV.Key, SecurityAlgorithms.RsaSha256Signature)
			};
			string jwt = handler.CreateToken(descriptor);
			return Ok( new { accessToken = jwt});
		}
    }
}
