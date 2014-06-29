using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    public class GraphicsObject
    {
        public string Name;
        public virtual void Render()
        {

        }
    }

    public class Group : GraphicsObject
    {
        public List<GraphicsObject> Children = new List<GraphicsObject>();

        public override void Render()
        {
            Console.WriteLine("I am the Group called " + Name);
            foreach (var child in Children)
            {
                child.Render();
            }
        }
    }

    public class Cube : GraphicsObject
    {
        public override void Render()
        {
            Console.WriteLine("I'm a Cube, my name is: " + Name);
        }
    }

    public class Sphere : GraphicsObject
    {
        public override void Render()
        {
            Console.WriteLine("Here's a Sphere and I am " + Name);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var root = new Group{Name = "Root"};

            root.Children.Add(new Cube   { Name = "A"});
            root.Children.Add(new Sphere { Name = "B"});

            root.Render();

            Console.ReadKey();
        }
    }
}
