using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q1ConstructTrie : Processor
    {
        public Q1ConstructTrie(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, String[], String[]>) Solve);

        public string[] Solve(long n, string[] patterns)
        {
            return MakeTrie(patterns);
        }

        private string[] MakeTrie(string[] patterns)
        {
            DnaNode.nodesCount = 0;
            DnaTri trie = new DnaTri();
            for(int i =0;i<patterns.Length; i++)
            {
                trie.Add(patterns[i]);
            }

            return MakeArray(trie);
        }

        private string[] MakeArray(DnaTri trie)
        {
            string[] output = new string[trie.nodes.Count];
            for(int i =1;i<trie.nodes.Count;i++)
            {
                if(trie.nodes[i].letter != '$')
                output[i] = (trie.nodes[i].parent.number-1).ToString() + "->"
                + (trie.nodes[i].number-1).ToString() + ":" + trie.nodes[i].letter;
            }
            Array.Sort(output);
            return output;
        }
    }
}
