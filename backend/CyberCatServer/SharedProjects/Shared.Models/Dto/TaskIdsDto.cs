using System;
using System.Collections.Generic;

namespace Shared.Models.Dto
{
    [Serializable]
    public class TaskIdsDto
    {
        public List<string> TaskIds
        {
            get => taskIds;
            set => taskIds = value;
        }

        public List<string> taskIds;

        public override string ToString()
        {
            return string.Join(", ", taskIds);
        }
    }
}