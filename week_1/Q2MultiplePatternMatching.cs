using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q2MultiplePatternMatching : Processor
    {
        public Q2MultiplePatternMatching(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, string[] patterns)
        {
            List<long> result = new List<long>();
            List<DnaNode> trieNodes = new List<DnaNode>();
            trieNodes = MakeTrie(patterns);
            for(int i = 0 ; i<text.Length ; i++)
            {
                string subString = text.Substring(i);
                if (MatchPattern(subString, trieNodes))
                    result.Add(i);
            }
            if (result.Count == 0)
                result.Add(-1);
            result.Sort();
            return result.ToArray();
        }

        private bool MatchPattern(string subString, List<DnaNode> trieNodes)
        {
            DnaNode curr = trieNodes[0];
            int i;
            for( i = 0;  i<subString.Length; i++)
            {
                char let = subString[i];
                int a = -1;
                switch(let)
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
                if (curr.children[a] == null)
                    return false;
                curr = curr.children[a];
                if (curr.IsLeaf())
                    return true;
                
            }
            return false;
        }

        private List<DnaNode> MakeTrie(string[] patterns)
        {
            DnaNode.nodesCount = 0;
            DnaTri trie = new DnaTri();
            for (int i = 0; i < patterns.Length; i++)
            {
                trie.Add(patterns[i]);
            }

            return trie.nodes;
        }
    }
}
