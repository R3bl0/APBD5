using Animals.DTOs;
using FluentValidation;

namespace Animals.Validators
{
    public class AnimalReplaceRequestValidator : AbstractValidator<ReplaceAnimalRequest>
    {
        public AnimalReplaceRequestValidator()
        {
            RuleFor(s => s.Name).MaximumLength(200).NotNull();
            RuleFor(s => s.Category).MaximumLength(200).NotNull();
            RuleFor(s => s.Area).MaximumLength(200).NotNull();
        }
    }
}