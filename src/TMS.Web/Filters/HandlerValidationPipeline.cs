using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TMS.Shared;
using TMS.Shared.ApiErrors;

namespace TMS.Web.Filters
{
    public class HandlerValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response
    {
        private readonly MachineDateTime _mdt;

        private readonly IValidator<TRequest> _validator;

        public HandlerValidationPipeline(IValidator<TRequest> validator, MachineDateTime mdt)
        {
            _validator = validator;
            _mdt = mdt;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
            {
                var response = new Response(new BadRequestError(result.Errors.Select(x => x.ErrorMessage).ToList()),
                    _mdt);
                return (TResponse) response;
            }

            return await next();
        }
    }
}