using Microsoft.Identity.Client;

namespace PokeDraft.Helpers
{
    public class FailureMessages
    {
        public const string ElementNotFound = "The requested {0} could not be found!";
        public const string ElementAlreadyExists = "The {0} {1} already exists!";
        public const string ElementModificationConflict = "The {0} could not be modified as {1} already exists!";
        public const string SpeciesWrongType = "The species cannot be created because {0} is not a type that exists.";
        public const string SpeciesEvolutionDoesNotExist = "The species indicated by the id {0} does not exist. The update failed!";
        public const string InvalidInput = "Validation error: invalid input.";
    }
}
