using System.Threading.Tasks;

namespace siwe_nethereum.RestServices
{
    public interface IEnsMetadataService
    {
        public Task<string> GetEnsCacheAvatarUrl(string ensName);

        public string GetEnsCacheAvatar(string ensName);

        public Task<string> GetEnsEmail(string ensName);

        public Task<string> GetEnsName(string ensAddress);

        public Task<string> GetEnsUrl(string ensName);
    }
}
