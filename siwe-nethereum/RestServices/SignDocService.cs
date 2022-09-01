using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Newtonsoft.Json;

using siwe.Messages;
using siwe_rest_service.Models;

namespace siwe_nethereum.RestServices
{
    public class SignDocService : ISignDocService
    {
        private readonly string          _baseUrl;
        private readonly HttpClient      _httpClient;
        private readonly SiweRestService _siweRestService;

        public SignDocService(string providedBaseUrl = null)
        {
            _baseUrl    = providedBaseUrl ?? "http://localhost:1234";
            _httpClient = new HttpClient();

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _siweRestService = new SiweRestService(providedBaseUrl);
        }

        public SignDocService(HttpClient client, string providedBaseUrl = null)
        {
            _baseUrl    = providedBaseUrl ?? "http://localhost:1234";
            _httpClient = client;

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _siweRestService = new SiweRestService(client, providedBaseUrl);
        }

        public async Task<DocSignData> GetSignatureDateTime(MySiweMessage message)
        {
            var SignatureData = new DocSignData();

            if (string.IsNullOrEmpty(message.Statement))
            {
                return SignatureData;
            }

            SignatureData = JsonConvert.DeserializeObject<DocSignData>(message.Statement);

            return 
                await _httpClient.GetFromJsonAsync<DocSignData>("api/signdoc?user=" + SignatureData.DocSigner + 
                                                               "&docId=" + SignatureData.DocId);
        }

        public async Task<DocSignData> PostDocumentRegistration(MySiweMessage message)
        {
            var SignatureData = new DocSignData();

            if (string.IsNullOrEmpty(message.Statement))
            {
                return SignatureData;
            }

            var MsgAndText = await _siweRestService.PostSignIn(message);
            if (String.IsNullOrEmpty(MsgAndText.Address))
            {
                return SignatureData;
            }

            var ResponseSignatureData = new DocSignData();
            SignatureData = JsonConvert.DeserializeObject<DocSignData>(message.Statement);

            var response = await _httpClient.PostAsJsonAsync<DocSignData>("api/signdoc", SignatureData);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                ResponseSignatureData = JsonConvert.DeserializeObject<DocSignData>(responseContent);
            }

            return ResponseSignatureData;
        }

        public async Task<DocSignData> PutDocumentSignature(MySiweMessage message)
        {
            var SignatureData = new DocSignData();

            if (string.IsNullOrEmpty(message.Statement))
            {
                return SignatureData;
            }

            var MsgAndText = await _siweRestService.PostSignIn(message);
            if (String.IsNullOrEmpty(MsgAndText.Address))
            {
                return SignatureData;
            }

            var ResponseSignatureData = new DocSignData();
            SignatureData = JsonConvert.DeserializeObject<DocSignData>(message.Statement);

            var response = await _httpClient.PutAsJsonAsync<DocSignData>("api/signdoc", SignatureData);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                ResponseSignatureData = JsonConvert.DeserializeObject<DocSignData>(responseContent);
            }

            return ResponseSignatureData;
        }
    }
}
