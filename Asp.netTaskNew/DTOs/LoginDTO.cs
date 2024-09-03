using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netTaskNew.DTOs
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginDTO : ControllerBase
    {
        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
