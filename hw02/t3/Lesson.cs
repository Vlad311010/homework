namespace t3
{
    abstract class Lesson
    {
        public string? TextDescription { get; protected set; }

        public Lesson(string? description) 
        {
            TextDescription = description;
        }

        public abstract Lesson Copy();
    }
}
