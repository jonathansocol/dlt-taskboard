using Deloitte.TaskBoard.Api.Models.Dtos;
using FluentValidation;

namespace Deloitte.TaskBoard.Api.Validators
{
    public class UpdateAssignmentDtoValidator : AbstractValidator<UpdateAssignmentDto>
    {
        public UpdateAssignmentDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .When(x => x.Title != null);

            RuleFor(x => x.Description)
                .NotEmpty()
                .When(x => x.Description != null);

            RuleFor(x => x.Status)
                .NotEmpty()
                .Must(x => ValidStatusList.Values.Contains(x))
                .When(x => x.Status != null)
                .WithMessage($"Only the following values are valid for Status: {string.Join(", ", ValidStatusList.Values)}");

            RuleFor(x => x.Priority)
                .NotEmpty()
                .Must(x => ValidPriorityList.Values.Contains(x))
                .When(x => x.Priority != null)
                .WithMessage($"Only the following values are valid for Priority: {string.Join(", ", ValidPriorityList.Values)}");

            RuleFor(x => x.Requester)
                .NotEmpty()
                .When(x => x.Requester != null);

            RuleFor(x => x.Order)
                .GreaterThan(0)
                .When(x => x.Order.HasValue);
        }
    }
}
