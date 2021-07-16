using System.Collections.Generic;
using System.IO;

namespace Chess
{
    public sealed class Log
    {
        internal Log()
        {
            this.Print("Log initialized");
        }
        internal void WriteLog()
        {
            using (StreamWriter writer = new StreamWriter("chess.log"))
            {
                foreach (string message in this.mMessages)
                {
                    writer.WriteLine(message);
                }
                writer.Close();
            }
        }
        public void Print(string text)
        {
            this.mMessages.Add(text); // i should probably do something more than this later
        }
        private List<string> mMessages = new List<string>();
    }
}