using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Nethereum.Siwe.Core;

using siwe.Messages;
using siwe_rest_service.Models;

namespace siwe_nethereum.RestServices
{
    public class SiweRestService : ISiweRestService
    {
        private readonly string     baseUrl;
        private readonly HttpClient httpClient;

        public SiweRestService(string providedBaseUrl = null)
        {
            baseUrl    = providedBaseUrl ?? "http://localhost:1234";
            httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public SiweRestService(HttpClient client, string providedBaseUrl = null)
        {
            baseUrl    = providedBaseUrl ?? "http://localhost:1234";
            httpClient = client;

            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<SiweMeResult> GetMe()
        {
            return await httpClient.GetFromJsonAsync<SiweMeResult>("api/me");
        }

        public async Task<string> GetNonce()
        {
            return await httpClient.GetStringAsync(baseUrl + "api/nonce");
        }

        public async Task<SiweMessageAndText> GetNotepadText(string address)
        {
            return await httpClient.GetFromJsonAsync<SiweMessageAndText>("api/signin?id=" + address);
        }

        public async Task<SiweMessageAndText> PostSignIn(SiweMessage message)
        {
            var MsgWithText = new SiweMessageAndText();
            var response    = await httpClient.PostAsJsonAsync<SiweMessage>("api/signin", message);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                MsgWithText = JsonConvert.DeserializeObject<SiweMessageAndText>(responseContent);
            }

            return MsgWithText;
        }

        public async Task<SiweMeResult> PostSignOut(SiweMessage message)
        {
            var meResult = new SiweMeResult();
            var response = await httpClient.PostAsJsonAsync<SiweMessage>("api/signout", message);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                meResult = JsonConvert.DeserializeObject<SiweMeResult>(responseContent);
            }

            return meResult;
        }

        public async Task<SiweMessageAndText> PutSave(SiweMessageAndText message)
        {
            var MsgWithText = new SiweMessageAndText();

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", message.Token);

            var response = await httpClient.PutAsJsonAsync<SiweMessageAndText>("api/save", message);
            if (!response.IsSuccessStatusCode)
            {
                MsgWithText.Address  = message.Address;
                MsgWithText.ErrorMsg = response.ReasonPhrase;
            }

            return MsgWithText;
        }
    }
}
