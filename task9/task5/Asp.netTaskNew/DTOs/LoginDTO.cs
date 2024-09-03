using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netTaskNew.DTOs
{

    public class LoginDTO 
    {
        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
