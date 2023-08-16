using AppGiaoHangAPI.Interface.IRepository;
using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using AppGiaoHangAPI.Model.Model;

namespace AppGiaoHangAPI.Controllers
{
    [Route("api/customer-order")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private ICustomerOrderRepository orderRepository;
        public CustomerOrderController(ICustomerOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        [HttpGet]
        public async Task<ActionResult<ResponeInfo>> getAllCustomerOrder()
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                ErrorMessageInfo error = await orderRepository.getAllCustomerOrder();
                if (error.isErrorEx || !error.isSuccess)
                {

                    responeInfo.error_code = error.error_code;
                    responeInfo.message = error.message;
                    return BadRequest(responeInfo);
                }
                responeInfo.data = error.data;
                // Serialize the response object with ReferenceHandler.Preserve
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true, // Optional: format JSON for readability
                };
                string json = JsonSerializer.Serialize(responeInfo, options);

                // Return the JSON response
                return Content(json, "application/json");

            }
            catch (Exception ex)
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.InternalServerError;
                responeInfo.message = ex.Message;
                return BadRequest(responeInfo);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ResponeInfo>> postCustomerOrder(List<CustomerOrder> customerOrder)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await orderRepository.postCustomerOrder(customerOrder);
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
        [HttpGet("customer-id/{customerID}")]
        public async Task<ActionResult<ResponeInfo>> getCustomerOrderByCustomerID(long customerID)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                ErrorMessageInfo error = await orderRepository.getCustomerOrderByCustomerID(customerID);
                if (error.isErrorEx || !error.isSuccess)
                {

                    responeInfo.error_code = error.error_code;
                    responeInfo.message = error.message;
                    return BadRequest(responeInfo);
                }
                responeInfo.data = error.data;
                // Serialize the response object with ReferenceHandler.Preserve
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true, // Optional: format JSON for readability
                };
                string json = JsonSerializer.Serialize(responeInfo, options);

                // Return the JSON response
                return Content(json, "application/json");

            }
            catch (Exception ex)
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.InternalServerError;
                responeInfo.message = ex.Message;
                return BadRequest(responeInfo);
            }
        }
        [HttpGet("employee-id/{employeeID}")]
        public async Task<ActionResult<ResponeInfo>> getCustomerOrderByEmployeeID(long employeeID)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                ErrorMessageInfo error = await orderRepository.getCustomerOrderByEmployeeID(employeeID);
                if (error.isErrorEx || !error.isSuccess)
                {

                    responeInfo.error_code = error.error_code;
                    responeInfo.message = error.message;
                    return BadRequest(responeInfo);
                }
                responeInfo.data = error.data;
                // Serialize the response object with ReferenceHandler.Preserve
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true, // Optional: format JSON for readability
                };
                string json = JsonSerializer.Serialize(responeInfo, options);

                // Return the JSON response
                return Content(json, "application/json");

            }
            catch (Exception ex)
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.InternalServerError;
                responeInfo.message = ex.Message;
                return BadRequest(responeInfo);
            }
        }
        [HttpGet("{OrderID}")]
        public async Task<ActionResult<ResponeInfo>> getCustomerOrderByID(long OrderID)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                ErrorMessageInfo error = await orderRepository.getCustomerOrderByOrderID(OrderID);
                if (error.isErrorEx || !error.isSuccess)
                {

                    responeInfo.error_code = error.error_code;
                    responeInfo.message = error.message;
                    return BadRequest(responeInfo);
                }
                responeInfo.data = error.data;
                // Serialize the response object with ReferenceHandler.Preserve
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true, // Optional: format JSON for readability
                };
                string json = JsonSerializer.Serialize(responeInfo, options);

                // Return the JSON response
                return Content(json, "application/json");

            }
            catch (Exception ex)
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.InternalServerError;
                responeInfo.message = ex.Message;
                return BadRequest(responeInfo);
            }
        }
        [HttpPut("{OrderID}")]
        public async Task<ActionResult<ResponeInfo>> putCustomerOrder(List<CustomerOrder> customerOrders)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await orderRepository.updateCustomerOrder(customerOrders);
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
        [HttpDelete("{OrderID}")]
        public async Task<ActionResult<ResponeInfo>> deleteCustomerOrder(List<CustomerOrder> customerOrders)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await orderRepository.deleteCustomerOrder(customerOrders);
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
        [HttpPost("employee-id-OrderStatus")]
        public async Task<ActionResult<ResponeInfo>> getCustomerOrderByCustomerID(CustomerOrder customerOrder)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                ErrorMessageInfo error = await orderRepository.getAllCustomerOrderByOrderStatusAndEmployeeID(customerOrder);
                if (error.isErrorEx || !error.isSuccess)
                {

                    responeInfo.error_code = error.error_code;
                    responeInfo.message = error.message;
                    return BadRequest(responeInfo);
                }
                responeInfo.data = error.data;
                // Serialize the response object with ReferenceHandler.Preserve
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true, // Optional: format JSON for readability
                };
                string json = JsonSerializer.Serialize(responeInfo, options);

                // Return the JSON response
                return Content(json, "application/json");

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
