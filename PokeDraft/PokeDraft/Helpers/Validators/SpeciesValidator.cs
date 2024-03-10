using FluentValidation;
using FluentValidation.AspNetCore;
using PokeDraft.DTOs;
using PokeDraft.Models;
using Type = PokeDraft.Models.Type;


namespace PokeDraft.Helpers.Validators
{
    public class SpeciesValidator : AbstractValidator<Species>
    {
        public SpeciesValidator() {
            RuleFor(x => x.SpeciesName).MinimumLength(3).WithMessage("Species name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Species name must be at most 100 characters long.")
                .Must(x => StringFormatValidator.IsOnlyASCIILettersOrNumbers(x)).WithMessage("Species name must begin with a capital letter followed only by lowercase letters or numbers");

            RuleFor(x => x.EvolutionLevel).GreaterThan(6).WithMessage("Evolution level must be at least 7.")
                .LessThan(65).WithMessage("Evolution level must be at most 64.");

            RuleFor(x => x.PrimaryType).Length(3, 100).WithMessage("Name must be between 3-100 characters.");
            RuleFor(x => x.PrimaryType).Must(x => StringFormatValidator.IsOnlyASCIILetters(x)).WithMessage("First letter must be capital, other letters must be lowercase.");

            RuleFor(x => x.SecondaryType).Length(3, 100).WithMessage("Name must be between 3-100 characters.");
            RuleFor(x => x.SecondaryType).Must(x => (x!=null && StringFormatValidator.IsOnlyASCIILetters(x) || x == null)).WithMessage("First letter must be capital, other letters must be lowercase.");

            RuleFor(x => x.ImageName).MinimumLength(4).WithMessage("Image name must be at least 4 characters long.")
                .Must(x => x.Substring(x.Length - 4) == ".png").WithMessage("Image name must end with the .png extension");
        }
    }
}
