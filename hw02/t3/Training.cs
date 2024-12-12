namespace t3
{
    internal class Training : TrainingEntity
    {

        private Lesson[] _trainingPlan = new Lesson[2];
        private int _planElements = 0;

        public Training(string? description) : base(description) { }


        public void Add(Lesson lesson)
        {
            if (_planElements == _trainingPlan.Length)
            {
                // Extend _trainingPlan size if needed (2 times of current size to reduce realocation)
                Array.Resize(ref _trainingPlan, _trainingPlan.Length * 2);
            }

            _trainingPlan[_planElements++] = lesson;
        }

        public bool IsPractical()
        {
            bool isPractical = true;
            for (int i = 0; i < _planElements; i++)
            {
                if (!(_trainingPlan[i] is PracticalLesson))
                {
                    isPractical = false;
                }
            }
            return isPractical;
        }

        public Training Clone()
        {
            Training trainingCopy = new Training(new string(this.TextDescription));
            trainingCopy.TextDescription = this.TextDescription == null ? string.Copy(this.TextDescription)  : null;
            foreach (Lesson lesson in _trainingPlan)
            {
                trainingCopy.Add(lesson.Copy());
            }

            return trainingCopy;
        }
    }
}
