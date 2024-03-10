namespace PokeDraft.Helpers
{
    public class StringFormatValidator
    {
        public static bool IsOnlyASCIILettersOrNumbers(string value)
        {
            if (char.IsLower(value[0]))
            {
                return false;
            }
            foreach (char c in value.Substring(1))
            {
                if (c is >= 'a' and <='z' or >='0' and <='9')
                    continue;
                else
                    return false;
            }
            return true;
        }

        public static bool IsOnlyASCIILetters(string value)
        {
            if (char.IsLower(value[0]))
            {
                return false;
            }
            foreach (char c in value.Substring(1))
            {
                if (c is >= 'a' and <= 'z')
                    continue;
                else
                    return false;
            }
            return true;
        }

    }
}
