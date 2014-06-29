using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace DepInjMEFCalculator
{
    public interface IOperation
    {
        char Symbol { get; }
        int Operate(int a, int b);
    }

    [Export(typeof(IOperation))]
    public class OpPlus : IOperation
    {
        public char Symbol { get { return '+'; } }
        public int Operate(int a, int b)
        {
            return a + b;
        }
    }

    [Export(typeof(IOperation))]
    public class OpMinus : IOperation
    {
        public char Symbol { get { return '-'; } }
        public int Operate(int a, int b)
        {
            return a - b;
        }
    }

    [Export(typeof(IOperation))]
    public class OpMult : IOperation
    {
        public char Symbol { get { return '*'; } }
        public int Operate(int a, int b)
        {
            return a * b;
        }
    }
    [Export(typeof(IOperation))]
    public class OpDiv : IOperation
    {
        public char Symbol { get { return '/'; } }
        public int Operate(int a, int b)
        {
            return a / b;
        }
    }



    class Calculator
    {
        [ImportMany(typeof(IOperation))]
        public List<IOperation> operations;

        public string Calculate(string input)
        {
            int left;
            int right;
            char opSymbol;
            int opInx = FindFirstNonDigit(input);
            if (opInx < 0)
                return "No operator specified.";
            try
            {
                left = int.Parse(input.Substring(0, opInx));
                right = int.Parse(input.Substring(opInx + 1));
            }
            catch (Exception)
            {
                return "Error parsing command.";
            }

            opSymbol = input[opInx];
            foreach (IOperation op in operations)
            {
                if (op.Symbol == opSymbol)
                {
                    return op.Operate(left, right).ToString();
                }
            }
            return "Unknown operator " + opSymbol + ".";
        }

        private static int FindFirstNonDigit(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!(Char.IsDigit(s[i]))) return i;
            }
            return -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            CompositionContainer container = new CompositionContainer(catalog);

            Calculator calc = new Calculator();

            container.ComposeParts(calc);

            

            // calc.operations = new List<IOperation>(new IOperation[] { new OpPlus(), new OpMinus(), new OpMult(), new OpDiv()});

            String s;
            Console.WriteLine("Enter Command:");
            while (true)
            {
                s = Console.ReadLine();
                Console.WriteLine(calc.Calculate(s));
            }
        }
    }
}
