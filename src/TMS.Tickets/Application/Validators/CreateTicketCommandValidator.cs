using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TMS.Tickets.Integration.Commands;
using TMS.Tickets.Persistence;

namespace TMS.Tickets.Application.Validators
{
    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        private readonly TicketsDbContext _dbContext;

        public CreateTicketCommandValidator(TicketsDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.TicketDto.Title)
                .MaximumLength(128)
                .NotEmpty();

            RuleFor(x => x.TicketDto.Category)
                .MaximumLength(64)
                .NotEmpty()
                .MustAsync(CategoryExists)
                .WithMessage((u, category) => $"{category} category does not exist.");

            RuleFor(x => x.TicketDto.Description)
                .MaximumLength(512)
                .NotEmpty();
        }

        private async Task<bool> CategoryExists(CreateTicketCommand command, string category,
            CancellationToken cancellationToken)
        {
            var dbCategories = await _dbContext.Categories.ToListAsync(cancellationToken);

            var categories = dbCategories.Select(x => x.Name).ToList();

            return categories.Contains(category);
        }
    }
}