using System;
using System.Text;

using Microsoft.Extensions.Options;

using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.Extensions;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.UI;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

using siwe_rest_service.Logic.ERC5289;

namespace siwe_rest_service.Logic
{
    public class SignDocumentLogic : ISignDocumentLogic
    {
        private readonly EthereumSettings? _ethereumSettings;

        private readonly String          _address;
        private readonly Web3            _web3;
        private readonly ContractHandler _signDocContractHandler;

        public SignDocumentLogic(IOptions<EthereumSettings> ethereumSettings)
        {
            _ethereumSettings = ethereumSettings.Value;

            var account = new Nethereum.Web3.Accounts.Account(_ethereumSettings.AccountPrivateKey);

            _address = account.Address;
            _web3    = new Web3(account, _ethereumSettings.Url);

            var eRC5289LibraryDeployment = new ERC5289LibraryDeployment();

            var transactionReceiptDeployment =
                _web3.Eth.GetContractDeploymentHandler<ERC5289LibraryDeployment>().SendRequestAndWaitForReceiptAsync(eRC5289LibraryDeployment).Result;

            var contractAddress = transactionReceiptDeployment.ContractAddress;

            _signDocContractHandler = _web3.Eth.GetContractHandler(contractAddress);
        }

        public SignDocumentLogic(IEthereumHostProvider hostProvider)
        {
            _ethereumSettings = null;
            _address          = String.Empty;

            _web3 = hostProvider.GetWeb3Async().Result;

            var eRC5289LibraryDeployment = new ERC5289LibraryDeployment();

            var transactionReceiptDeployment =
                _web3.Eth.GetContractDeploymentHandler<ERC5289LibraryDeployment>().SendRequestAndWaitForReceiptAsync(eRC5289LibraryDeployment).Result;

            var contractAddress = transactionReceiptDeployment.ContractAddress;

            _signDocContractHandler = _web3.Eth.GetContractHandler(contractAddress);
        }

        public ushort RegisterDocument(string multiHash)
        {
            var registerDocumentFunction =
                new RegisterDocumentFunction() { Multihash = Encoding.ASCII.GetBytes(multiHash) };

            var registerDocumentFunctionTxnReceipt =
                _signDocContractHandler.SendRequestAndWaitForReceiptAsync(registerDocumentFunction).Result;

            var registerEventOutput =
                registerDocumentFunctionTxnReceipt.DecodeAllEvents<DocumentRegisteredEventDTO>();

            return registerEventOutput[0].Event.DocumentId;
        }

        public string GetDocumentHash(ushort docId)
        {
            var legalDocumentFunction = new LegalDocumentFunction();

            legalDocumentFunction.DocumentId = docId;

            var docHashValueBytes =
                _signDocContractHandler.QueryAsync<LegalDocumentFunction, byte[]>(legalDocumentFunction).Result;

            return Encoding.UTF8.GetString(docHashValueBytes, 0, docHashValueBytes.Length); ;
        }

        public bool IsDocumentSigned(string user, ushort docId)
        {
            var documentSignedFunction = new DocumentSignedFunction();

            documentSignedFunction.User       = user;
            documentSignedFunction.DocumentId = docId;

            var isDocSignedValue =
                _signDocContractHandler.QueryAsync<DocumentSignedFunction, bool>(documentSignedFunction).Result;

            return isDocSignedValue;
        }

        public DateTime GetDocumentSigningDateTime(string user, ushort docId)
        {
            var documentSignedAtFunction = new DocumentSignedAtFunction();

            documentSignedAtFunction.User       = user;
            documentSignedAtFunction.DocumentId = docId;

            var docSignedAtEpochValue =
                _signDocContractHandler.QueryAsync<DocumentSignedAtFunction, ulong>(documentSignedAtFunction).Result;

            return DateTimeOffset.FromUnixTimeSeconds((long) docSignedAtEpochValue).DateTime;
        }

        public void SignDocument(string user, ushort docId, string signature)
        {
            var signDocumentFunction = new SignDocumentFunction();

            signDocumentFunction.Signer     = user;
            signDocumentFunction.DocumentId = docId;
            signDocumentFunction.Signature  = Encoding.ASCII.GetBytes(signature);

            var signDocumentFunctionTxnReceipt =
                _signDocContractHandler.SendRequestAndWaitForReceiptAsync(signDocumentFunction).Result;
        }

    }
}
