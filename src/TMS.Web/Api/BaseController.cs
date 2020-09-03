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

        protected readonly MachineDateTime Mdt;

        protected readonly IMediator Mediator;

        public BaseController(IMediator mediator, MachineDateTime mdt)
        {
            Mediator = mediator;
            Mdt = mdt;
        }
        
        ///     Processes response and returns appropriate HTTP code
        ///     If response is valid and actionOnSuccess is not specified, the method returns by default:
        ///     200 - for GET requests,
        ///     201 - for POST requests,
        ///     204 - for DELETE requests,
        ///     200 - for PUT requests,
        ///     200 - for PATCH requests,
        ///     200 - for HEAD requests (headers only)
        protected IActionResult ProcessResponse<T>(Response<T> response, IActionResult actionOnSuccess = null)
            where T : BaseDTO
        {
            if (!response.IsValid)
                return ReturnError(response);

            return actionOnSuccess ?? ReturnData(response);
        }

        private IActionResult ReturnData<T>(Response<T> response) where T : BaseDTO
        {
            return Request.Method switch
            {
                "GET" => Ok(response),
                "POST" => Created($"{Request.Path}/{response.Data.Id}", response),
                "DELETE" => NoContent(),
                "PUT" => Ok(response),
                "PATCH" => Ok(response),
                "HEAD" => Ok(),
                _ => Ok()
            };
        }

        private IActionResult ReturnError<T>(Response<T> response) where T : BaseDTO
        {
            return response.Error.GetType().Name switch
            {
                nameof(BadRequestError) => BadRequest(response),
                nameof(NotFoundError) => NotFound(response),
                nameof(ConflictError) => Conflict(response),
                _ => BadRequest(response)
            };
        }
    }
}