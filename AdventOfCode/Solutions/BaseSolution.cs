using System;

namespace Solutions
{
    public abstract class BaseSolution
    {
        protected string[] Args { get; private set; }

        protected Action<string> Write { get; private set; }

        protected Action<string> WriteLine { get; private set; }

        public void Run(string[] args, Action<string> write, Action<string> writeLine)
        {
            Args = args;
            Write = write;
            WriteLine = writeLine;
            OnRun();
        }

        protected abstract void OnRun();
    }
}
