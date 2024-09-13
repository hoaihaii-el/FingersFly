using FingersFly.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FingersFly.API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadReq()
        {
            return BadRequest("Bad request!");
        }

        [HttpGet("notfound")]
        public IActionResult GetNotfound()
        {
            return NotFound();
        }

        [HttpGet("internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("Test Exception");
        }

        [HttpGet("validationerror")]
        public IActionResult GetValidationError(Product product)
        {
            return Ok();
        }
    }
}
