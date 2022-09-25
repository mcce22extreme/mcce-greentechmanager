using GreenTechManager.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.Identity.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public AuthController()
        {
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("client_id", model.ClientId));
            nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));
            nvc.Add(new KeyValuePair<string, string>("username", model.UserName));
            nvc.Add(new KeyValuePair<string, string>("password", model.Password));
            //nvc.Add(new KeyValuePair<string, string>("scope", string.Join(' ', model.Scopes)));
            
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://{HttpContext.Request.Host}/connect/token")
            {
                Content = new FormUrlEncodedContent(nvc)
            };
            var response = await HttpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Ok(content);
            }
            else
            {
                return Unauthorized(content);
            }
        }
    }
}
