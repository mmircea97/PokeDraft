namespace PokeDraft.Exceptions.Species
{
    public class SpeciesDoesNotExistException : Exception
    {
        public SpeciesDoesNotExistException(string message) : base(message) { }
    }
}
