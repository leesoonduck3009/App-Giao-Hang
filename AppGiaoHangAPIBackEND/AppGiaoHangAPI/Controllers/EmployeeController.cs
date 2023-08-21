using AppGiaoHangAPI.Interface.IRepository;
using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppGiaoHangAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        [HttpGet("", Name = "GetAllEmployee")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponeInfo>> getAllEmployee()
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await employeeRepository.getAllEmployee();
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
        [HttpPost("", Name = "PostNewEmployee")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponeInfo>> createNewEmployee(List<Employee> employee)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await employeeRepository.createNewEmployee(employee);
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
        [HttpGet("{id}", Name = "GetEmployeeByID")]
        public async Task<ActionResult<ResponeInfo>> getEmployeeByID(long id)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await employeeRepository.getEmployeeByID(id);
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
        [HttpPut("", Name = "UpdateEmployee")]
        public async Task<ActionResult<ResponeInfo>> updateEmployee(List<Employee> employee)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await employeeRepository.updateNewEmployee(employee);
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
        [HttpDelete("", Name = "DeleteEmployee")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponeInfo>> deleteEmployee(List<Employee> employees)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await employeeRepository.deleteNewEmployee(employees);
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
        [HttpPut("location/{id}", Name = "UpdateEmployeeLocation")]
        public async Task<ActionResult<ResponeInfo>> updateEmployeeLocation(long id, Employee employee)
        {
            ResponeInfo responeInfo = new ResponeInfo();
            try
            {
                responeInfo.statusCode = System.Net.HttpStatusCode.OK;
                Stream a = HttpContext.Request.Body;
                ErrorMessageInfo error = await employeeRepository.updateEmployeeLocation(id, employee);
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
    }
}
