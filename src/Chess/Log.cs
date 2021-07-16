using System.Collections.Generic;
using System.IO;

namespace Chess
{
    public sealed class Log
    {
        internal Log()
        {
            mMessages = new List<string>();
            Print("Log initialized");
        }
        internal void WriteLog()
        {
            using var writer = new StreamWriter("chess.log");
            foreach (string message in mMessages)
            {
                writer.WriteLine(message);
            }
            writer.Close();
        }
        public void Print(string text)
        {
            mMessages.Add(text); // i should probably do something more than this later
        }
        private readonly List<string> mMessages;
    }
}