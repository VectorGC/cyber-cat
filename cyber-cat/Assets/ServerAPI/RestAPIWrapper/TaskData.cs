using TaskUnits;

namespace RestAPIWrapper
{
    public class TaskData : ITaskData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Output { get; set; }
        public bool? IsSolved { get; set; }
        public float ReceivedScore { get; set; }
        public float TotalScore { get; set; }

        public TaskData(string id, string name, string description, string output, bool? isSolved, float receivedScore, float totalScore)
        {
            Id = id;
            Name = name;
            Description = description;
            Output = output;
            IsSolved = isSolved;
            ReceivedScore = receivedScore;
            TotalScore = totalScore;
        }
    }
}