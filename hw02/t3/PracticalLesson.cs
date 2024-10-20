
namespace t3
{
    internal class PracticalLesson : Lesson
    {
        public string? TaskConditionLink { get; private set; }
        public string? SolutionLink { get; private set; }

        public PracticalLesson(string? description, string? taskCondition, string? solution) : base(description)
        {
            TaskConditionLink = taskCondition;
            SolutionLink = solution;
        }

        public override PracticalLesson Copy()
        {
            return new PracticalLesson(
                    this.TextDescription != null ? string.Copy(this.TextDescription) : null,
                    this.TaskConditionLink != null ? string.Copy(this.TaskConditionLink) : null,
                    this.SolutionLink != null ? string.Copy(this.SolutionLink) : null
                );
        }
    }
}
