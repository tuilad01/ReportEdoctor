using System.Collections.Generic;

namespace AutomationTest1
{
    public class Config
    {
        public Config()
        {
            ExamPacket = new List<NameValue>();
            Room = new List<NameValue>();
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public IList<NameValue> ExamPacket { get; set; }

        public IList<NameValue> Room { get; set; }

    }

    public class NameValue
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
