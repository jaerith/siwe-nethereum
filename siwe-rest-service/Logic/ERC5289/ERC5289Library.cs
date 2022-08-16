using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace siwe_rest_service.Logic.ERC5289
{

    public partial class ERC5289LibraryDeployment : ERC5289LibraryDeploymentBase
    {
        public ERC5289LibraryDeployment() : base(BYTECODE) { }
        public ERC5289LibraryDeployment(string byteCode) : base(byteCode) { }
    }

    public class ERC5289LibraryDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040526000805461ffff1916905534801561001b57600080fd5b506107858061002b6000396000f3fe608060405234801561001057600080fd5b50600436106100625760003560e01c806301ffc9a7146100675780632a418642146100a05780635e93a153146100e5578063747f0038146100fa5780637564797e1461011a578063dba0f8d214610140575b600080fd5b61008b61007536600461039e565b6001600160e01b03191663db0ddffb60e01b1490565b60405190151581526020015b60405180910390f35b61008b6100ae3660046103fd565b61ffff1660009081526002602090815260408083206001600160a01b03949094168352929052205467ffffffffffffffff16151590565b6100f86100f33660046104d3565b61019c565b005b61010d610108366004610531565b610274565b604051610097919061054c565b61012d61012836600461059a565b61031b565b60405161ffff9091168152602001610097565b61018361014e3660046103fd565b61ffff1660009081526002602090815260408083206001600160a01b03949094168352929052205467ffffffffffffffff1690565b60405167ffffffffffffffff9091168152602001610097565b6001600160a01b03831633146101e75760405162461bcd60e51b815260206004820152600c60248201526b34b73b30b634b2103ab9b2b960a11b604482015260640160405180910390fd5b61ffff8216600081815260026020908152604080832033808552908352818420805467ffffffffffffffff19164267ffffffffffffffff16179055938352600382528083209383529290522061023d8282610660565b5060405161ffff83169033907f19e2d5cebbefe3a7ba8a439b6e9d2f1067313d68fb1205c84b1a01b0a738972490600090a3505050565b61ffff81166000908152600160205260409020805460609190610296906105d7565b80601f01602080910402602001604051908101604052809291908181526020018280546102c2906105d7565b801561030f5780601f106102e45761010080835404028352916020019161030f565b820191906000526020600020905b8154815290600101906020018083116102f257829003601f168201915b50505050509050919050565b6000805461ffff1681526001602052604081206103388382610660565b506000805460405161ffff909116917ff849c0d0ba0c7f778ec6614285c306ffa3262888bb863e8e54c41170585dc53f91a26000805461ffff16908061037d83610720565b91906101000a81548161ffff021916908361ffff1602179055509050919050565b6000602082840312156103b057600080fd5b81356001600160e01b0319811681146103c857600080fd5b9392505050565b80356001600160a01b03811681146103e657600080fd5b919050565b803561ffff811681146103e657600080fd5b6000806040838503121561041057600080fd5b610419836103cf565b9150610427602084016103eb565b90509250929050565b634e487b7160e01b600052604160045260246000fd5b600082601f83011261045757600080fd5b813567ffffffffffffffff8082111561047257610472610430565b604051601f8301601f19908116603f0116810190828211818310171561049a5761049a610430565b816040528381528660208588010111156104b357600080fd5b836020870160208301376000602085830101528094505050505092915050565b6000806000606084860312156104e857600080fd5b6104f1846103cf565b92506104ff602085016103eb565b9150604084013567ffffffffffffffff81111561051b57600080fd5b61052786828701610446565b9150509250925092565b60006020828403121561054357600080fd5b6103c8826103eb565b600060208083528351808285015260005b818110156105795785810183015185820160400152820161055d565b506000604082860101526040601f19601f8301168501019250505092915050565b6000602082840312156105ac57600080fd5b813567ffffffffffffffff8111156105c357600080fd5b6105cf84828501610446565b949350505050565b600181811c908216806105eb57607f821691505b60208210810361060b57634e487b7160e01b600052602260045260246000fd5b50919050565b601f82111561065b57600081815260208120601f850160051c810160208610156106385750805b601f850160051c820191505b8181101561065757828155600101610644565b5050505b505050565b815167ffffffffffffffff81111561067a5761067a610430565b61068e8161068884546105d7565b84610611565b602080601f8311600181146106c357600084156106ab5750858301515b600019600386901b1c1916600185901b178555610657565b600085815260208120601f198616915b828110156106f2578886015182559484019460019091019084016106d3565b50858210156107105787850151600019600388901b60f8161c191681555b5050505050600190811b01905550565b600061ffff80831681810361074557634e487b7160e01b600052601160045260246000fd5b600101939250505056fea26469706673582212207131906f04600f2cad4e8652963c13703d711149872ce0134050350960e06d8964736f6c63430008100033";
        public ERC5289LibraryDeploymentBase() : base(BYTECODE) { }
        public ERC5289LibraryDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class DocumentSignedFunction : DocumentSignedFunctionBase { }

    [Function("documentSigned", "bool")]
    public class DocumentSignedFunctionBase : FunctionMessage
    {
        [Parameter("address", "user", 1)]
        public virtual string User { get; set; }
        [Parameter("uint16", "documentId", 2)]
        public virtual ushort DocumentId { get; set; }
    }

    public partial class DocumentSignedAtFunction : DocumentSignedAtFunctionBase { }

    [Function("documentSignedAt", "uint64")]
    public class DocumentSignedAtFunctionBase : FunctionMessage
    {
        [Parameter("address", "user", 1)]
        public virtual string User { get; set; }
        [Parameter("uint16", "documentId", 2)]
        public virtual ushort DocumentId { get; set; }
    }

    public partial class LegalDocumentFunction : LegalDocumentFunctionBase { }

    [Function("legalDocument", "bytes")]
    public class LegalDocumentFunctionBase : FunctionMessage
    {
        [Parameter("uint16", "documentId", 1)]
        public virtual ushort DocumentId { get; set; }
    }

    public partial class RegisterDocumentFunction : RegisterDocumentFunctionBase { }

    [Function("registerDocument", "uint16")]
    public class RegisterDocumentFunctionBase : FunctionMessage
    {
        [Parameter("bytes", "multihash", 1)]
        public virtual byte[] Multihash { get; set; }
    }

    public partial class SignDocumentFunction : SignDocumentFunctionBase { }

    [Function("signDocument")]
    public class SignDocumentFunctionBase : FunctionMessage
    {
        [Parameter("address", "signer", 1)]
        public virtual string Signer { get; set; }
        [Parameter("uint16", "documentId", 2)]
        public virtual ushort DocumentId { get; set; }
        [Parameter("bytes", "signature", 3)]
        public virtual byte[] Signature { get; set; }
    }

    public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

    [Function("supportsInterface", "bool")]
    public class SupportsInterfaceFunctionBase : FunctionMessage
    {
        [Parameter("bytes4", "_interfaceId", 1)]
        public virtual byte[] InterfaceId { get; set; }
    }

    public partial class DocumentRegisteredEventDTO : DocumentRegisteredEventDTOBase { }

    [Event("DocumentRegistered")]
    public class DocumentRegisteredEventDTOBase : IEventDTO
    {
        [Parameter("uint16", "documentId", 1, true)]
        public virtual ushort DocumentId { get; set; }
    }

    public partial class DocumentSignedEventDTO : DocumentSignedEventDTOBase { }

    [Event("DocumentSigned")]
    public class DocumentSignedEventDTOBase : IEventDTO
    {
        [Parameter("address", "signer", 1, true)]
        public virtual string Signer { get; set; }
        [Parameter("uint16", "documentId", 2, true)]
        public virtual ushort DocumentId { get; set; }
    }

    public partial class DocumentSignedOutputDTO : DocumentSignedOutputDTOBase { }

    [FunctionOutput]
    public class DocumentSignedOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "isSigned", 1)]
        public virtual bool IsSigned { get; set; }
    }

    public partial class DocumentSignedAtOutputDTO : DocumentSignedAtOutputDTOBase { }

    [FunctionOutput]
    public class DocumentSignedAtOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint64", "timestamp", 1)]
        public virtual ulong Timestamp { get; set; }
    }

    public partial class LegalDocumentOutputDTO : LegalDocumentOutputDTOBase { }

    [FunctionOutput]
    public class LegalDocumentOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bytes", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class SupportsInterfaceOutputDTO : SupportsInterfaceOutputDTOBase { }

    [FunctionOutput]
    public class SupportsInterfaceOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}
