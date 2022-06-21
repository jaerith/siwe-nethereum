using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using siwe.Oidc;

using siwe_rest_service.Logic;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OidcRegistrationController : ControllerBase
    {
        private readonly OidcSettings _oidcSettings;

        public OidcRegistrationController(IOptions<OidcSettings> oidcSettings)
        {
            _oidcSettings = oidcSettings.Value;
        }

        // GET: api/<OidcRegistrationController>
        [HttpGet]
        public OidcConfiguration Get()
        {
            OidcConfiguration? configuration = new OidcConfiguration();

            var discoveryClient = new HttpClient();

            var response = discoveryClient.GetAsync(_oidcSettings.Issuer + "/.well-known/openid-configuration").Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;

                configuration = 
                    JsonSerializer.Deserialize<OidcConfiguration>(responseString);
            }

            return configuration ?? new OidcConfiguration();
        }

        [HttpPost]
        public IActionResult Post([FromBody] string redirectUri)
        {
            OidcRegistration? oidcRegistration = new OidcRegistration();

            try
            {
                var oidcConfiguration = Get();

                var registrationClient = new HttpClient();

                registrationClient.BaseAddress = new Uri(_oidcSettings.Issuer);
                registrationClient.DefaultRequestHeaders.Accept.Clear();
                registrationClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var registrationInput = new OidcRegistration();

                registrationInput.redirect_uris = new List<string>() { redirectUri };

                var response =
                    registrationClient
                    .PostAsJsonAsync<OidcRegistration>("/register", registrationInput)
                    .Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    oidcRegistration = JsonSerializer.Deserialize<OidcRegistration>(responseContent);

                    if ((oidcRegistration != null) && (oidcConfiguration != null))
                    {
                        oidcRegistration.configuration = oidcConfiguration;
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("ERROR -> \n" + ex.ToString());
            }

            return CreatedAtAction(nameof(Post), new { id = oidcRegistration?.client_id }, oidcRegistration);
        }

    }
}
