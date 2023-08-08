using AppGiaoHangAPI.Interface.IRepository;
using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AppGiaoHangAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet("", Name = "GetAllProduct")]
        public async Task<ActionResult<ResponeInfo>> getAllProduct()
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await productRepository.getAllProduct();
                if (error.isErrorEx || !error.isSuccess)
                {

                    responeInfo.error_code = error.error_code;
                    responeInfo.message = error.message;
                    return BadRequest(responeInfo);
                }
                responeInfo.data = error.data;
                return await Task.FromResult(responeInfo);
            }
            catch (Exception ex)
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.InternalServerError;
                responeInfo.message = ex.Message;
                return BadRequest(responeInfo);
            }
        }
        [HttpPost("", Name = "PostNewProduct")]
        public async Task<ActionResult<ResponeInfo>> createNewProduct(Product product)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await productRepository.createNewProduct(product);
                if (error.isErrorEx || !error.isSuccess)
                {

                    responeInfo.error_code = error.error_code;
                    responeInfo.message = error.message;
                    return BadRequest(responeInfo);
                }
                responeInfo.data = error.data;
                return await Task.FromResult(responeInfo);
            }
            catch (Exception ex)
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.InternalServerError;
                responeInfo.message = ex.Message;
                return BadRequest(responeInfo);
            }
        }
        [HttpGet("{id}", Name = "GetProductByID")]
        public async Task<ActionResult<ResponeInfo>> getProductByID(long id)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await productRepository.getProductByID(id);
                if (error.isErrorEx || !error.isSuccess)
                {

                    responeInfo.error_code = error.error_code;
                    responeInfo.message = error.message;
                    return BadRequest(responeInfo);
                }
                responeInfo.data = error.data;
                return await Task.FromResult(responeInfo);
            }
            catch (Exception ex)
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.InternalServerError;
                responeInfo.message = ex.Message;
                return BadRequest(responeInfo);
            }
        }
        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<ActionResult<ResponeInfo>> updateProduct(long id, Product product)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await productRepository.updateNewProduct(id, product);
                if (error.isErrorEx || !error.isSuccess)
                {

                    responeInfo.error_code = error.error_code;
                    responeInfo.message = error.message;
                    return BadRequest(responeInfo);
                }
                responeInfo.data = error.data;
                return Ok(responeInfo);
            }
            catch (Exception ex)
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.InternalServerError;
                responeInfo.message = ex.Message;
                return BadRequest(responeInfo);
            }
        }
        [HttpDelete("{id}", Name = "DeleteProduct")]
        public async Task<ActionResult<ResponeInfo>> deleteProduct(long id)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await productRepository.deleteProduct(id);
                if (error.isErrorEx || !error.isSuccess)
                {
                    responeInfo.error_code = error.error_code;
                    responeInfo.message = error.message;
                    return BadRequest(responeInfo);
                }
                responeInfo.data = error.data;
                return Ok(responeInfo);
            }
            catch (Exception ex)
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.InternalServerError;
                responeInfo.message = ex.Message;
                return BadRequest(responeInfo);
            }
        }
    }
}
