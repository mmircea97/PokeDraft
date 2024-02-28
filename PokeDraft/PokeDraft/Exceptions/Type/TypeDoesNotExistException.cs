namespace PokeDraft.Exceptions.Type
{
    public class TypeDoesNotExistException : Exception
    {
        public TypeDoesNotExistException(string message) : base(message) { }
    }
}
