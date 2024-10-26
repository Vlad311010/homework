namespace t3
{
    abstract class Lesson : TrainingEntity
    {
        public Lesson(string? description) : base(description) { }

        public abstract Lesson Copy();
    }
}
