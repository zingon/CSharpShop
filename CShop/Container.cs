using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop
{
    class Container
    {
        interface INode
        {
            bool isIt(string name);
        }
        internal class BasicNode : INode
        {
            private string name;
            public string Name { get => name; internal set => name = value; }
            public bool isIt(string name)
            {
                return (this.Name == name);
            }
        }
        private class Node<I> : BasicNode
        {
            
            private I instance;

    
            public Node(string name, I instance)
            {
                Name = name;
                Instance = instance;
            }

            public I Instance { get => instance; private set => instance = value; }
           

           
        }

        private List<BasicNode> nodes;
        private static Container instance;
        private static readonly object padlock = new object();

        public static Container Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Container();
                    }
                    return instance;
                };
            }
        }

        private Container()
        {
            this.nodes = new List<BasicNode>();
            Add<AdminWindow>("AdminWindow", new AdminWindow());
        }

        public void Add<I>(string name, I instance)
        {
                nodes.Add(new Node<I>(name, instance));            
        }
        
        public I Get<I>(string name)
        {
            foreach (var node in nodes)
            {
                Node<I> maine = (Node<I>)node;
                if(node.isIt(name))
                {
                    
                    I instance1 = maine.Instance;
                    return instance1;
                }
            }
            return default(I);
        }
    }
}
