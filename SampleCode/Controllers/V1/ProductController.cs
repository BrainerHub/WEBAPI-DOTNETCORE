using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using SampleCodeService.SampleCodeRepository.Interface;
using SampleCodeModel.DbEntities;
using SampleCodeModel.Helper;
using SampleCodeModel.CustomModels;
using SampleCodeModel.RequestModel;

namespace SampleCode.Controllers.V1
{
    /// <summary>
    /// Products Controller
    /// </summary>
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Declaration
        private IProductRepository _productRepository;
        private static Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment { get; set; }

        /// <summary>
        /// Initializing Students Controller 
        /// </summary>
        /// <param name="productRepository">Product Repository</param>
        public ProductController(IProductRepository productRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion.

        #region List
        /// <summary>
        /// Get Product List
        /// </summary>                
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpGet]
        [Route("list")]
        [Authorize]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Liststudent([FromQuery][Required][CustomizeValidator(Interceptor = typeof(FluentInterceptor))] SearchRequestModel model)
        {

            var list = await _productRepository.List(model);

            if (list != null && list.Count > 0) {
                foreach (var item in list)
                {
                    if(item.ProductImages != null && item.ProductImages.Count > 0)
                    {
                        foreach (var inneritem in item.ProductImages)
                        {
                            inneritem.ImageUrl = _hostingEnvironment.ContentRootPath + inneritem.ImageUrl;
                        }
                    }
                }
            }

            return Ok(list);
        }
        #endregion

        #region Create Product
        /// <summary>
        /// Create Product
        /// </summary>           
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostProduct([FromForm][Required][CustomizeValidator(Interceptor = typeof(FluentInterceptor))] ProductRequestModel productRequestModel)
        {
            var files = new List<string>();
            string filename = string.Empty;
            long AllowedDocumentMaxFileSize = 5000;
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                if (Request.Form.Files.Count > 5)
                {
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Max 5 is All0wed");
                }
                foreach (var file in Request.Form.Files)
                {

                    var mediaFile = new MediaFile();
                    mediaFile.filename = file.FileName;

                    if (!string.IsNullOrWhiteSpace(mediaFile.filename) && mediaFile.filename.Length > 105)
                    {
                        throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "File name too big");
                    }

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        mediaFile.file = stream.ToArray();
                        if (stream != null)
                        {
                            if (!string.IsNullOrEmpty(mediaFile.filename))
                            {
                                Regex filetypeTypeRegex = new Regex(@"(.*?)\.(jpg|JPG|jpeg|JPEG|png|PNG)$");
                                Int64 filesize = stream.Length / 1024;
                                mediaFile.fileType = Path.GetExtension(mediaFile.filename).Replace('.', ' ').Trim();

                                if (!filetypeTypeRegex.IsMatch(mediaFile.filename))
                                {
                                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Enter only image");
                                }

                                if (filesize > AllowedDocumentMaxFileSize)
                                {
                                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Fille size is big");
                                }
                                mediaFile.fileSize = Convert.ToInt32(stream.Length);
                                var fileupload = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/uploads/", mediaFile.filename);

                                using (var fileStream = new FileStream(fileupload, FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);

                                }
                                files.Add("/uploads/" + mediaFile.filename);
                            }
                        }
                        else
                        {
                            throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "File is required");
                        }
                    }
                }
            }
            await _productRepository.CreateProduct(productRequestModel, files);
            return StatusCode(StatusCodes.Status201Created);

        }
        #endregion

        #region Update Product
        /// <summary>
        /// Update Product
        /// </summary>           
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpPut]
        [Authorize]
        [Route("{product_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromForm][Required][CustomizeValidator(Interceptor = typeof(FluentInterceptor))] ProductUpdateRequestModel productUpdateRequestModel, [FromRoute] int product_id)
        {
            var files = new List<string>();
            string filename = string.Empty;
            long AllowedDocumentMaxFileSize = 5000;
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                if (Request.Form.Files.Count > 5)
                {
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Max 5 is All0wed");
                }
                foreach (var file in Request.Form.Files)
                {

                    var mediaFile = new MediaFile();
                    mediaFile.filename = file.FileName;

                    if (!string.IsNullOrWhiteSpace(mediaFile.filename) && mediaFile.filename.Length > 105)
                    {
                        throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "File name too big");
                    }

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        mediaFile.file = stream.ToArray();
                        if (stream != null)
                        {
                            if (!string.IsNullOrEmpty(mediaFile.filename))
                            {
                                Regex filetypeTypeRegex = new Regex(@"(.*?)\.(jpg|JPG|jpeg|JPEG|png|PNG)$");
                                Int64 filesize = stream.Length / 1024;
                                mediaFile.fileType = Path.GetExtension(mediaFile.filename).Replace('.', ' ').Trim();

                                if (!filetypeTypeRegex.IsMatch(mediaFile.filename))
                                {
                                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Enter only image");
                                }

                                if (filesize > AllowedDocumentMaxFileSize)
                                {
                                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Fille size is big");
                                }
                                mediaFile.fileSize = Convert.ToInt32(stream.Length);
                                var fileupload = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/uploads/", mediaFile.filename);

                                using (var fileStream = new FileStream(fileupload, FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);

                                }
                                files.Add("/uploads/" + mediaFile.filename);
                            }
                        }
                        else
                        {
                            throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "File is required");
                        }
                    }
                }
            }
            await _productRepository.UpdateProduct(productUpdateRequestModel, product_id, files);
            return StatusCode(StatusCodes.Status204NoContent);

        }
        #endregion

        #region Delete
        /// <summary>
        /// Get Product Delete
        /// </summary>                
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpDelete]
        [Route("{product_id}/delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deletestudent([FromRoute][Required] int product_id)
        {

            await _productRepository.Delete(product_id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        #endregion


    }
}
