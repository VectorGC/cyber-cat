namespace TaskUnits.Messages
{
    public struct NeedUpdateTaskData
    {
        public string TaskId { get; }
        public string Token { get; }

        public NeedUpdateTaskData(string taskId, string token)
        {
            TaskId = taskId;
            Token = token;
        }
    }
}