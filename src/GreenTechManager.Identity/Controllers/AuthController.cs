using GreenTechManager.Core;
using GreenTechManager.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.Identity.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("client_id", model.ClientId));
            nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));
            nvc.Add(new KeyValuePair<string, string>("username", model.UserName));
            nvc.Add(new KeyValuePair<string, string>("password", model.Password));

            var request = new HttpRequestMessage(HttpMethod.Post, $"{AppSettings.Current.BaseAddress}/connect/token")
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
