namespace Application
{
    public ref struct ValidatorRules
    {
        public const int ShortParagraph = 50;
        public const int LongParagraph = 200;

        #region Regex

        public const string RegexOnlyLetters = @"^[A-Za-zა-ჰ\s]+";
        public const string RegexOnlyNumbers = @"^[0-9\s]*$";
        public const string RegexPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,}$";

        #endregion
    }
}
