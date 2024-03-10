using FluentValidation;
using Type = PokeDraft.Models.Type;

namespace PokeDraft.Helpers.Validators
{
    public class TypeValidator : AbstractValidator<Type>
    {
        public TypeValidator() { 
            RuleFor(x=>x.TypeName).Length(3,100).WithMessage("Name must be between 3-100 characters.");
            RuleFor(x => x.TypeName).Must(x => StringFormatValidator.IsOnlyASCIILetters(x)).WithMessage("Type name must be only lowercase letters, with the exception of the first which should be capital.");
        }
    }
}
