using FluentValidation;
using Type = PokeDraft.Models.Type;

namespace PokeDraft.Helpers.Validators
{
    public class TypeValidator : AbstractValidator<Type>
    {
        public TypeValidator() { 
            RuleFor(x=>x.TypeName).Length(3,100).WithMessage("Name must be between 3-100 characters.");
            RuleFor(x => x.TypeName).Matches("^[A-Z][a-z]*$").WithMessage("First letter must be capital, other letters must be lowercase.");
            RuleFor(x => x.TypeName).Matches("^[a-zA-Z]+$").WithMessage("Only letters are permitted.");
        }
    }
}
