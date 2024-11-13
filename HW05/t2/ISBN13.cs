using System.Text.RegularExpressions;

namespace t2
{
    public readonly struct ISBN13
    {
        public readonly string ISBN;

        public ISBN13(string isbn) 
        {
            if (!IsValidISBNFormat(isbn))
                throw new ArgumentException($"Invaldi ISNB code format {isbn}");

            ISBN = SimplifyISBN(isbn);    
        }

        private bool IsValidISBNFormat(string isbn)
        {
            string shortFormRegex = "^[0-9]{13}$";
            string regex = "^[0-9]{3}-[0-9]{1}-[0-9]{2}-[0-9]{6}-[0-9]{1}$";
            return Regex.IsMatch(isbn, shortFormRegex) || Regex.IsMatch(isbn, regex);
        }

        private string SimplifyISBN(string isbn)
        {
            return isbn.Replace("-", "");
        }

        public override bool Equals(object? obj)
        {
            return obj is ISBN13 other && other.ISBN == ISBN;
        }

        public override int GetHashCode()
        {
            return ISBN.GetHashCode();
        }

        public static implicit operator ISBN13(string isbn) => new ISBN13(isbn);
        public static explicit operator string(ISBN13 isbn) => isbn.ISBN;
    }
}
