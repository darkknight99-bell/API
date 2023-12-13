using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;


//Getting or Working with Files

namespace API.Controllers
{
    [Route("api/files")] //specifies URL pattern for a controller/action
    [ApiController] //enables certain api=specific behaviours like routing, http responses et all
    public class FilesController : ControllerBase
    {

        private readonly FileExtensionContentTypeProvider _extensionContentTypeProvider;  //we inject the dependency here

        public FilesController(FileExtensionContentTypeProvider extensionContentTypeProvider)
        {
            _extensionContentTypeProvider = extensionContentTypeProvider
                ?? throw new ArgumentNullException(nameof(extensionContentTypeProvider));
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId) 
        {
            //look up the actual file
            var pathToFile = "ASP.NET Core Roadmap.pdf";

            //check whether the file exists
            if(!System.IO.File.Exists(pathToFile)) 
            {
                return NotFound();
            }

            if(!_extensionContentTypeProvider.TryGetContentType(
                pathToFile, out var contentType)) 
            {
                contentType = "application/octet-stream"; //used to indicate that a body contains arbitrary binary data
            }
            
            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));   
        }
    }
}
