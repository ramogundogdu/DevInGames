using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepInjMEFCalculator
{
    public interface IOperation
    {
        char Symbol { get; }
        int Operate(int a, int b);
    }

    public class OpPlus : IOperation
    {
        public char Symbol { get { return '+'; } }
        public int Operate(int a, int b)
        {
            return a + b;
        }
    }

    public class OpMinus : IOperation
    {
        public char Symbol { get { return '-'; } }
        public int Operate(int a, int b)
        {
            return a - b;
        }
    }

    public class OpMult : IOperation
    {
        public char Symbol { get { return '*'; } }
        public int Operate(int a, int b)
        {
            return a * b;
        }
    }
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
            Calculator calc = new Calculator();

            calc.operations = new List<IOperation>(new IOperation[] { new OpPlus(), new OpMinus(), new OpMult(), new OpDiv()});

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
