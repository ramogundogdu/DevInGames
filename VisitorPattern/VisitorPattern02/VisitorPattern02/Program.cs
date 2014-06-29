using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Visitor
    {
        public virtual void Visit(Group group)
        {
        }

        public virtual void Visit(Cube cube)
        {
        }

        public virtual void Visit(Sphere sphere)
        {
        }
    }

    public class GraphicsObject
    {
        public string Name;
        public virtual void Accept(Visitor visitor)
        {

        }
    }

    public class Group : GraphicsObject
    {
        public List<GraphicsObject> Children = new List<GraphicsObject>();

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
            foreach (var child in Children)
            {
                child.Accept(visitor);
            }
        }
    }

    public class Cube : GraphicsObject
    {
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Sphere : GraphicsObject
    {
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }


//--------------------- GRAPHICS LIBRARY CODE ABOVE THIS LINE -------------------------------------
//--------------------- USER / APPLICATION CODE BELOW THIS LINE -----------------------------------


    public class RenderingVisitor : Visitor
    {
        public override void Visit(Group group)
        {
            Console.WriteLine("The Group is here " + group.Name);
        }

        public override void Visit(Cube cube)
        {
            Console.WriteLine("A Cube named: " + cube.Name);
        }

        public override void Visit(Sphere sphere)
        {
            Console.WriteLine("The Sphere thats called " + sphere.Name);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var root = new Group{Name = "Root"};

            root.Children.Add(new Cube   { Name = "A"});
            root.Children.Add(new Sphere { Name = "B"});

            root.Accept(new RenderingVisitor());

            Console.ReadKey();
        }
    }
}
