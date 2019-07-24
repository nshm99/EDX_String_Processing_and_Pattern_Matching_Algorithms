using System;
using System.Collections.Generic;

namespace A5
{
    internal class DnaTri
    {
        public DnaNode root;
        public List<DnaNode> nodes;

        public DnaTri()
        {
            root = new DnaNode('0', null);
            nodes = new List<DnaNode>();
            nodes.Add(root);
        }

        internal void Add(string v)
        {
            DnaNode current = root;
            for(int i =0;i<v.Length;i++)
            {
                int a = -1;
                char letter = v[i];
                switch(v[i])
                {
                    case 'A':
                        a = 0;
                        break;
                    case 'G':
                        a = 1;
                        break;
                    case 'T':
                        a = 2;
                        break;
                    case 'C':
                        a = 3;
                        break;
                }
                if(current.children[a] == null)
                {
                    current.children[a] = new DnaNode(letter, current);
                    nodes.Add(current.children[a]);
                }

                current = current.children[a];

            }
            current.children[4] = new DnaNode('$', current);
            nodes.Add(current.children[4]);
        }

    }
}