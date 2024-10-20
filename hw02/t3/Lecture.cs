namespace t3
{
    internal class Lecture : Lesson
    {
        public string? Topic { get; private set; }

        public Lecture(string? description, string? topic) : base(description)
        {
            Topic = topic;
        }

        public override Lecture Copy()
        {
            return new Lecture(
                    this.TextDescription != null ? string.Copy(this.TextDescription) : null,
                    this.Topic != null ? string.Copy(this.Topic) : null
                );
        }
    }
}
