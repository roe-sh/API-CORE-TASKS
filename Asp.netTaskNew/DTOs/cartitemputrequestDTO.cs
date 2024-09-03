using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netTaskNew.DTOs
{
    [Route("api/[controller]")]
    [ApiController]
    public class cartitemputrequestDTO : ControllerBase
    {
        public int Quantity { get; set; }
    }
}
