using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoulaCodingChallenge.Contracts;
using MoulaCodingChallenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoulaCodingChallenge.Controllers.V1
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/account")]
    public class AccountControllerV1 : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountControllerV1(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Account API
        /// Return:
        /// 200 - OK with account details
        /// 401 - Unauthorised if request token is not correct
        /// 404 - Account Not found if username and account number conbination does not return a valid account.
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Default")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{AccountNumber}")]
        public async Task<IActionResult> Get([FromRoute] int AccountNumber)
        {
            var userName = User.Identity.Name;
            try
            {
                var result = await _accountService.GetAccountAsync(AccountNumber, userName);
                if (result != null)
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }

            return NotFound(null);
        }
    }
}
