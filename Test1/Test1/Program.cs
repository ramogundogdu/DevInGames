using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    class Item
    {
        public int i;
        public string str;
    }

    class GenericList<T>
    {
        private T[] _items;
        private int _currentItem;
        private int _arraySize;

        public GenericList()
        {
            _arraySize = 20;
            _items = new T[_arraySize];
            _currentItem = 0;
        }

        public void Add(T item)
        {
            if (_currentItem >= _arraySize)
            {
                T[] oldArray = _items;
                _arraySize += 20;
                _items = new T[_arraySize];
                for (int i = 0; i < oldArray.Length; i++)
                    _items[i] = oldArray[i];
            }
            _items[_currentItem++] = item;
        }

        public T GetAt(int i)
        {
            return _items[i];
        }
    }

    /// <summary>
    /// This class is very good for solving math problems.
    /// </summary>
    class MathHelper
    {
        static T Min<T>(T a, T b) where T : IComparable<T>
        {
            /*
            if (a < b)
                return a;
            else
                return b;
            */
            return (a.CompareTo(b) < 0) ? a : b;
        }

        /// <summary>
        /// Calculates the maximum of a and b
        /// </summary>
        /// <param name="a">Left parameter</param>
        /// <param name="b">Right parameter</param>
        /// <returns></returns>
        private static double Max(double a, double b)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // GenericListTest();
            List<int> liste = new List<int>();

            liste.Add(4);
            liste.Add(2);
            liste.Add(6);
            liste.Add(3);
            liste.Add(4);
            liste.Add(0);

            /*
            foreach (int listeneintrag in liste)
            {
                Console.WriteLine(listeneintrag);
            }
         

            IEnumerator<int> enumerator = liste.GetEnumerator();
            for (enumerator.Reset(); enumerator.MoveNext();)
                Console.WriteLine(enumerator.Current);
               */
            
            
            
            BinTree<int> binTree = new BinTree<int>();

            binTree.Add(4);
            binTree.Add(2);
            binTree.Add(6);
            binTree.Add(3);
            binTree.Add(4);
            binTree.Add(0);

            binTree.PrintContents();
            /*
            foreach (int baumEintrag in binTree)
            {
                Console.WriteLine(baumEintrag);
            }
             * */


            Console.ReadKey();
        }

        private static void GenericListTest()
        {
            GenericList<Item> itemList = new GenericList<Item>();

            // while (UserHatNichtAbgebrochen)
            //   itemList.Add( GetItemFromUser() );

            Item i = itemList.GetAt(42);


            GenericList<int> myList = new GenericList<int>();
        }
    }
}
