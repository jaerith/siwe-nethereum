namespace siwe_rest_service.Logic
{
    public interface ISignDocumentLogic
    {
        ushort RegisterDocument(string multiHash);

        string GetDocumentHash(ushort docId);

        bool IsDocumentSigned(string user, ushort docId);

        DateTime GetDocumentSigningDateTime(string user, ushort docId);

        void SignDocument(string user, ushort docId, string signature);
    }
}
