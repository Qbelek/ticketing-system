using System;
using System.Runtime.CompilerServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMS.Shared;
using TMS.Shared.ApiErrors;
using TMS.Shared.DTO;

namespace TMS.Web.Api
{
    public class BaseController : ControllerBase
    {
        protected const string InvalidGuidMessage = "It is not a correct GUID. " +
                                                    "Please provide GUID in the following format: " +
                                                    "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
        
        protected readonly IMediator Mediator;
        
        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }

        /// <summary>
        /// Processes response and returns appropriate HTTP code
        /// </summary>
        /// <remarks>
        /// If response is valid and actionOnSuccess is not specified, the method returns by default:
        /// 200 - for GET requests,
        /// 201 - for POST requests,
        /// 204 - for DELETE requests,
        /// 200 - for PUT requests,
        /// 200 - for PATCH requests,
        /// 200 - for HEAD requests (headers only)
        /// </remarks>
        /// <param name="response"></param>
        /// <param name="actionOnSuccess"></param>
        /// <returns>IActionResult</returns>
        protected IActionResult ProcessResponse<T>(Response<T> response, IActionResult actionOnSuccess = null) 
            where T : BaseDTO
        {
            if (!response.IsValid) 
                return ReturnError(response.Error);

            return actionOnSuccess ?? ReturnData(response.Value);
        }

        private IActionResult ReturnData(BaseDTO data)
        {
            return this.Request.Method switch
            {
                "GET" => Ok(data),
                "POST" => Created($"{Request.Path}/{data.Id}", data),
                "DELETE" => NoContent(),
                "PUT" => Ok(data),
                "PATCH" => Ok(data),
                "HEAD" => Ok(),
                _ => Ok()
            };
        }
        
        private IActionResult ReturnError(Error error)
        {
            return error.GetType().Name switch
            {
                nameof(BadRequestError) => BadRequest(error),
                nameof(NotFoundError) => NotFound(error),
                nameof(ConflictError) => Conflict(error),
                _ => BadRequest(error)
            };
        }
    }
}