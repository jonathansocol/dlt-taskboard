using Deloitte.TaskBoard.Api.Models.Dtos;
using FluentValidation;

namespace Deloitte.TaskBoard.Api.Validators
{
    public class CreateAssignmentDtoValidator : AbstractValidator<CreateAssignmentDto>
    {
        public CreateAssignmentDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Status)
                .NotEmpty()
                .Must(x => ValidStatusList.Values.Contains(x))
                .WithMessage($"Only the following values are valid for Status: {string.Join(", ", ValidStatusList.Values)}");

            RuleFor(x => x.Priority)
                .NotEmpty()
                .Must(x => ValidPriorityList.Values.Contains(x))
                .WithMessage($"Only the following values are valid for Priority: {string.Join(", ", ValidPriorityList.Values)}");

            RuleFor(x => x.Requester)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Order)
                .GreaterThan(0);
        }
    }
}
