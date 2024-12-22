namespace t3
{
    internal class TrainingEntity
    {
        public string? TextDescription { get; protected set; }

        public TrainingEntity(string? description) 
        { 
            TextDescription = description;
        }
    }
}
