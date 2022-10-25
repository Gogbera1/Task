using FluentValidation;

namespace Application.Tasks.Request
{
    public class TaskModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        //files todo
    }

    public class CreateLandRequestValidator : AbstractValidator<TaskModel>
    {
        public CreateLandRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(ErrorCodes.Required)
                .MaximumLength(ValidatorRules.ShortParagraph)
                .WithMessage(ErrorCodes.ShortParagraph);

            RuleFor(x => x.ShortDescription)
                .MaximumLength(ValidatorRules.ShortParagraph)
                .WithMessage(ErrorCodes.ShortParagraph);

            RuleFor(x => x.Description)
                .MaximumLength(ValidatorRules.LongParagraph)
                .WithMessage(ErrorCodes.LongParagraph);

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage(ErrorCodes.Required);
        }
    }
}
