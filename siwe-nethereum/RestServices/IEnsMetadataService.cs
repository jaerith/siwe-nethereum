using System.Threading.Tasks;

namespace siwe_nethereum.RestServices
{
    public interface IEnsMetadataService
    {
        public Task<string> GetEnsAvatarUrl(string ensName);
    }
}
