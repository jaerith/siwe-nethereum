using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using Newtonsoft.Json;

using Nethereum.ENS;
using Nethereum.Web3;

namespace siwe_nethereum.RestServices
{
    public class EnsMetadataService : IEnsMetadataService
    {
        private const string CONST_WEB3_URL = "https://mainnet.infura.io/v3/ddd5ed15e8d443e295b696c0d07c8b02";

        private readonly Web3       web3;
        private readonly ENSService ensService;

        private readonly string     baseUrl;
        private readonly string     web3Url;
        private readonly HttpClient httpClient;

        public EnsMetadataService(string providedBaseUrl = null, string providedWeb3Url = null)
        {
            baseUrl    = providedBaseUrl ?? "https://metadata.ens.domains/mainnet/";
            web3Url    = providedWeb3Url ?? CONST_WEB3_URL;
            httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            web3       = new Web3(web3Url);
            ensService = new ENSService(web3);
        }

        public EnsMetadataService(HttpClient client, string providedBaseUrl = null, string providedWeb3Url = null)
        {
            baseUrl    = providedBaseUrl ?? "https://metadata.ens.domains/mainnet/";
            web3Url    = providedWeb3Url ?? CONST_WEB3_URL;
            httpClient = client;

            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            web3       = new Web3(web3Url);
            ensService = new ENSService(web3);
        }

        public string GetEnsCacheAvatar(string ensName)
        {
            return (baseUrl + "avatar/" + ensName);
        }

        public async Task<string> GetEnsCacheAvatarUrl(string ensName)
        {
            return await GetEnsCacheValue(ensName, "image");
        }

        public async Task<string> GetEnsEmail(string ensName)
        {
            return await ensService.ResolveTextAsync(ensName, TextDataKey.email);
        }

        public async Task<string> GetEnsName(string lookupAddress)
        {
            return await ensService.ReverseResolveAsync(lookupAddress);
        }

        public async Task<string> GetEnsUrl(string ensName)
        {
            return await ensService.ResolveTextAsync(ensName, TextDataKey.url);
        }

        #region Support Methods

        protected async Task<string> GetEnsCacheValue(string ensName, string tagName)
        {
            string avatarUrl = String.Empty;

            try
            {
                string sEnsMetadataJsonPayload = 
                    await httpClient.GetStringAsync(baseUrl + "avatar/" + ensName + "/meta");

                if (!String.IsNullOrEmpty(sEnsMetadataJsonPayload))
                {
                    // To evade the exception of a root element
                    sEnsMetadataJsonPayload = "{\n \"root\": " + sEnsMetadataJsonPayload + " }";

                    XmlDocument EnsJsonDoc = 
                        (XmlDocument) JsonConvert.DeserializeXmlNode(sEnsMetadataJsonPayload);

                    using (var nodeReader = new XmlNodeReader(EnsJsonDoc))
                    {
                        nodeReader.MoveToContent();

                        var EnsMetaXDoc = XDocument.Load(nodeReader);

                        var ImageElement = EnsMetaXDoc.Root.Element(tagName);

                        if (ImageElement != null)
                        {
                            avatarUrl = ImageElement.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                avatarUrl = String.Empty;
            }

            return avatarUrl;
        }

        #endregion

    }
}
