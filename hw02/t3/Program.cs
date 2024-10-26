namespace t3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Training training = new Training("Training descr.");
            Lecture lecture01 = new Lecture("Lect01.", "t01");
            Lecture lecture02 = new Lecture("Lect02.", "t02");
            PracticalLesson practice01 = new PracticalLesson("Pract01.", null, null);

            training.Add(practice01);
            Console.WriteLine(training.IsPractical());
            training.Add(lecture01);
            training.Add(lecture02);
            Console.WriteLine(training.IsPractical());


        }
    }
}
