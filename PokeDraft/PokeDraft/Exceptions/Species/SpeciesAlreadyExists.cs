namespace PokeDraft.Exceptions.Species
{
    public class SpeciesAlreadyExists : Exception
    {
        public SpeciesAlreadyExists(string message) : base(message) { }
    }
}
