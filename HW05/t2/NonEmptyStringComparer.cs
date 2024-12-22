namespace t2
{
    /// <summary>
    /// StringComparer that treats empty strings as unequal to each other
    /// </summary>
    public class NonEmptyStringComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            if (string.IsNullOrEmpty(x) && string.IsNullOrEmpty(y))
                return false;

            return string.Equals(x, y, StringComparison.Ordinal);
        }

        public int GetHashCode(string obj)
        {
            if (obj == null)
                return 0;

            return obj.GetHashCode();
        }
    }
}
