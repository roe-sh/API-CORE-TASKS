using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netTaskNew.DTOs
{
    
    public class userRequestDTO 
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
