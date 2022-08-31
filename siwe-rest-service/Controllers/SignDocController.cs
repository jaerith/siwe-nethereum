using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using siwe.Messages;

using siwe_rest_service.Logic;
using siwe_rest_service.Models;

namespace siwe_rest_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignDocController : ControllerBase
    {
        private readonly SignDocumentLogic _signDocLogic;

        public SignDocController(SignDocumentLogic signDocLogic)
        {
            _signDocLogic = signDocLogic;
        }

        [HttpGet("{user}/{docId}")]
        public DocSignData Get(string user, ushort docId)
        {
            DocSignData data = new DocSignData() { DocSigner = user, DocId = docId };

            data.DocSignDateTime = _signDocLogic.GetDocumentSigningDateTime(user, docId);

            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] DocSignData registerData)
        {
            try
            {
                registerData.DocId =
                    _signDocLogic.RegisterDocument(registerData.DocumentHash);
            }
            catch (InvalidDataException ex)
            {
                return UnprocessableEntity(ex.ToString());
            }

            return CreatedAtAction(nameof(Post), new { id = registerData.DocumentHash }, registerData);
        }

        [HttpPut]
        public IActionResult Put([FromBody] DocSignData signDocData)
        {
            _signDocLogic.SignDocument(signDocData.DocSigner, (ushort) signDocData.DocId, signDocData.DocSignSignature);

            return NoContent();
        }
    }
}
