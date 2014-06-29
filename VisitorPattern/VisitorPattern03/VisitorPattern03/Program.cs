using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GraphicsObject
    {
        public string Name;
    }

    public class Group : GraphicsObject
    {
        public List<GraphicsObject> Children = new List<GraphicsObject>();
    }

    public class Cube : GraphicsObject
    {
    }

    public class Sphere : GraphicsObject
    {
    }


    public class SomeNewGo : GraphicsObject
    {
    }


    // Not so nice implementation of a Visitor base class. Two drawbacks here.
    // 1. if-chain  in Traverse eats performance during traversal
    // 2. With additional class among the visitable objects (derivates of GraphicsObjet) we need to
    //    change the base class
    public class VisitorBase1
    {
        public void Traverse(GraphicsObject go)
        {
            var group = go as Group;
            if (group != null)
            {
                Visit(group);
                TraverseChildren(group.Children);
                return;
            }
            var cube = go as Cube;
            if (cube != null)
            {
                Visit(cube);
                return;
            }
            var sphere = go as Sphere;
            if (sphere != null)
            {
                Visit(sphere);
                return;
            }
            Visit(go);
        }

        public virtual void TraverseChildren(IEnumerable<GraphicsObject> children)
        {
            foreach (var child in children)
            {
                Traverse(child);
            }
        }

        public virtual void Visit(Group group)
        {
        }

        public virtual void Visit(Cube cube)
        {
        }

        public virtual void Visit(Sphere sphere)
        {
        }

        public virtual void Visit(GraphicsObject go)
        {
        }
    }


    public class Visitor
    {
        public delegate void VisitNode(GraphicsObject go);

        private Dictionary<Type, VisitNode> _visitors;

        // Here is the Reflection Magic. In a loop, find all "Visit" methods within
        // this visitor object (which might be a derivative of the class we're actually in).
        // Store those methods in the "_visitors" dictionary and make them 
        public Visitor()
        {
            _visitors = new Dictionary<Type, VisitNode>();

            // Loop over all of our methods
            foreach (MethodInfo mi in GetType().GetMethods())
            {
                // If the current method is called Visit
                if (mi.Name == "Visit")
                {
                    ParameterInfo[] pi = mi.GetParameters();
                    // And if it has only one parameter
                    if (pi.Length == 1)
                    {
                        Type paramType = pi[0].ParameterType;
                        // If the parameter is a Group
                        if (typeof(Group).IsAssignableFrom(paramType))
                        {
                            // Enlist the call to this method into _visitors. As it is called for Group objects, build the traversal over the children directly into the method
                            _visitors.Add(pi[0].ParameterType, delegate(GraphicsObject go)
                            {
                                mi.Invoke(this, new object[] { go });
                                TraverseChildren(((Group)go).Children);
                            });
                        }
                        // No group - but is it at least another derivative from GraphicsObject?
                        else if (typeof(GraphicsObject).IsAssignableFrom(paramType))
                        {
                            // Enlist the call to this method into _visitors
                            _visitors.Add(pi[0].ParameterType, delegate(GraphicsObject go)
                            {
                                mi.Invoke(this, new object[] { go });
                            });
                        }
                    }
                }
            }
        }

        public void Traverse(GraphicsObject go)
        {
            VisitNode visitNode;
            if (_visitors.TryGetValue(go.GetType(), out visitNode))
            {
                visitNode(go);
            }
            else
            {
                Visit(go);
            }
        }

        public virtual void TraverseChildren(IEnumerable<GraphicsObject> children)
        {
            foreach (var child in children)
            {
                Traverse(child);
            }
        }

        public virtual void Visit(GraphicsObject go)
        {
        }
    }


//--------------------- GRAPHICS LIBRARY CODE ABOVE THIS LINE -------------------------------------
//--------------------- USER / APPLICATION CODE BELOW THIS LINE -----------------------------------




    public class RenderingVisitor : Visitor
    {
        public void Visit(Group group)
        {
            Console.WriteLine("The Group is here " + group.Name);
        }

        public void Visit(Cube cube)
        {
            Console.WriteLine("A Cube named: " + cube.Name);
        }

        public void Visit(Sphere sphere)
        {
            Console.WriteLine("The Sphere thats called " + sphere.Name);
        }

        public override void Visit(GraphicsObject go)
        {
            Console.WriteLine("Not sure about this one " + go.Name);
        }
    
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var root = new Group{Name = "Root"};

            root.Children.Add(new Cube   { Name = "A"});
            root.Children.Add(new Sphere { Name = "B"});
            root.Children.Add(new SomeNewGo{ Name = "GO" });

            // root.Accept(new RenderingVisitor());
            new RenderingVisitor().Traverse(root);

            Console.ReadKey();
        }
    }
}
