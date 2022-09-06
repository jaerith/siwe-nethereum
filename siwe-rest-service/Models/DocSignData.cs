namespace siwe_rest_service.Models
{
    public class DocSignData
    {
        public string? DocName { get; set; }

        public string? DocumentHash { get; set; }

        public ushort? DocId { get; set; }

        public string? DocSigner { get; set; }

        public string? DocSignSignature { get; set; }

        public DateTime? DocSignDateTime { get; set; }

        public DocSignData()
        { }
    }
}
