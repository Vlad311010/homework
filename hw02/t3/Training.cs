namespace t3
{
    internal class Training
    {
        public string? TextDescription { get; private set; }

        private List<Lesson> _trainingPlan = new List<Lesson>();
        private bool _isPractical = true;

        public Training(string? textDescription)
        {
            TextDescription = textDescription;
        }


        public void Add(Lesson lesson)
        {
            if (lesson is Lecture)
                _isPractical = false;

            _trainingPlan.Add(lesson);
        }

        public bool IsPractical()
        {
            return _isPractical;
        }

        public Training Clone()
        {
            Training trainingCopy = new Training(new string(this.TextDescription));
            trainingCopy.TextDescription = this.TextDescription == null ? string.Copy(this.TextDescription) : null;
            foreach (Lesson lesson in _trainingPlan)
            {
                trainingCopy.Add(lesson.Copy());
            }

            return trainingCopy;
        }
    }
}
