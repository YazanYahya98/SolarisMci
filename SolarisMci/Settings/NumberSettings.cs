namespace SolarisMci.Settings
{

    public interface INumberSettings
    {
        int NumOfDigits { get; set; }
        int MaxDigit { get; set; }
        int MinDigit { get; set; }

        int NumOfLetters { get; set; }
        char MaxLetter { get; set; }
        char MinLetter { get; set; }


    }
    public class NumberSettings : INumberSettings
    {
        public int NumOfDigits { get; set; }
        public int MaxDigit { get; set; }
        public int MinDigit { get; set; }

        public int NumOfLetters { get; set; }
        public char MaxLetter { get; set; }
        public char MinLetter { get; set; }
    }
}
