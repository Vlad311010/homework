namespace t2.Library
{
    public class Author
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateOnly? BirthDate { get; private set; }
        public string FullName => FirstName + LastName;

        private const int _nameMaxLength = 200;

        public Author(string firstName, string lastName, DateOnly? birthDate)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));

            if (firstName.Length > _nameMaxLength)
                throw new ArgumentOutOfRangeException(nameof(firstName), $"First name length must not exceed {_nameMaxLength} characters.");

            if (lastName.Length > _nameMaxLength)
                throw new ArgumentOutOfRangeException(nameof(lastName), $"Last name length must not exceed {_nameMaxLength} characters.");

            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public override bool Equals(object? obj)
        {
            return obj is Author other
                && FirstName == other.FirstName
                && LastName == other.LastName
                && BirthDate == other.BirthDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, BirthDate);
        }
    }
}