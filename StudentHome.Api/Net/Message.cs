using System;

namespace StudentHome.Api.Net
{
    [Serializable]
    public class Message
    {
        public string Title { get; set; }
        public object Body { get; set; }

        public Message(string title, object body)
        {
            Title = title;
            Body = body;
        }

        public Message(string title)
        {
            Title = title;
        }
    }
}
