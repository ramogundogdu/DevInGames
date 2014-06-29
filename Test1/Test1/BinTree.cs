using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    class TreeItem<T>
    {
        public TreeItem<T> Left;
        public TreeItem<T> Right;
        public T Data;
    }

    public class BinTree<T> where T : IComparable<T>
    {
        private TreeItem<T> _root;

        public void Add(T data)
        {
            CheckAndAdd(ref _root, data);
        }


        public void PrintContents()
        {
            PrintNode(_root);
        }

        private void PrintNode(TreeItem<T> node)
        {
            if (node == null)
            {
                return;
            }

            PrintNode(node.Left);
            Console.WriteLine(node.Data);
            PrintNode(node.Right);
        }

        /*
        IEnumerator<T> SortedContents
        {
            get
            {
                

            }
        }
         * */

        private void CheckAndAdd(ref TreeItem<T> node, T data)
        {
            if (node == null)
            {
                node = new TreeItem<T> { Data = data };
                return;
            }

            int cr = data.CompareTo(node.Data);
            if (cr == 0)
                return;
            if (cr < 0)
            {
                CheckAndAdd(ref node.Left, data);
            }
            else // cr > 0
            {
                CheckAndAdd(ref node.Right, data);
            }
        }


    }
}
