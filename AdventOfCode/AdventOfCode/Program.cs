using Solutions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal class Program
    {
        private string[] _args;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run(args);
            Console.WriteLine("Press any key");
            Console.ReadLine();
        }

        private void Run(string[] args)
        {
            _args = args;
            if (args.Length != 2) 
            {
                Console.WriteLine("Usage: aoc [dayN] [file]");
                return;
            }

            Dictionary<string, Type> solutions = GetSolutions();
            string className = PopArg();
            if (!solutions.ContainsKey(className))
            {
                Console.WriteLine($"Unknown day {className}");
                return;
            }

            BaseSolution solution = (BaseSolution)Activator.CreateInstance(solutions[className]);
            solution.Run(_args, Console.Write, Console.WriteLine);
        }

        public string PopArg()
        {
            string value = _args.Length > 0 ? _args[0] : string.Empty;
            if (_args.Length > 0)
            {
                List<string> newArray = new List<string>(_args);
                newArray.RemoveAt(0);
                _args = newArray.ToArray();
            }
            else
            {
                _args = new string[] { };
            }
            return value;
        }

        Dictionary<string, Type> GetSolutions()
        {
            Dictionary<string, Type> solutions = new Dictionary<string, Type>();
            List<Type> types = typeof(BaseSolution).Assembly.GetTypes()
                .Where(t => !t.IsAbstract)
                .ToList();

            foreach(Type t in types)
            {
                solutions[t.Name.ToLower()] = t;
            }

            return solutions;
        }
    }
}
