using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q4SuffixTree : Processor
    {
        public Q4SuffixTree(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String[]>)Solve);

        public string[] Solve(string text)
        {
            List<SuffixNode> tree = new List<SuffixNode>();
            SuffixNode root = new SuffixNode(null);
            root.index = 0;
            tree.Add(root);

            for(int i =0; i<text.Length;i++)
            {
                AddNewSuffix(tree, i, text);
            }
            List<string> result = new List<string>();
            for(int i=1;i< tree.Count;i++)
            {
                result.Add(text.Substring(tree[i].value.Item1, tree[i].value.Item2));
            }
            return result.ToArray();
        }

        private void AddNewSuffix(List<SuffixNode> tree, int i, string text)
        {
            SuffixNode curr = tree[0];
            int sharedLen = i;
            int counter = 0;
            do
            {
                Tuple<int, int> currTuple = FindNode(curr, i+counter, text);// index,curr node index
                if (currTuple.Item2 == -1)
                    break;
                sharedLen = currTuple.Item1;
                curr = tree[currTuple.Item2];
                counter++;
            }while(counter < text.Length-i-1 );
            if (curr == tree[0])
            {
                SuffixNode v = new SuffixNode(new Tuple<int, int>(i, text.Length-i));
                v.parent = tree[0];
                v.index = tree.Count;
                tree[tree[0].index].children.Add(v);
                tree.Add(v);
                return;
            }

            int l = curr.value.Item2;
            SuffixNode p = new SuffixNode();
            p.parent = curr;

            if (curr.children.Count > 0 && sharedLen < l)
            {
                BreakEdge(tree,curr, sharedLen, l);
            }
            else
            {
                p.value = new Tuple<int, int>(sharedLen, text.Length - sharedLen);
                p.index = tree.Count;
                curr.children.Add(p);
                tree.Add(p);
            }
            //nodes.Add(p);
            int len = sharedLen - curr.value.Item1 ;
            //p = new SuffixNode(new Tuple<int, int>(i+ l , text.Length - i - len));
            //p = new SuffixNode(new Tuple<int, int>(subStratIndex, text.Length - subStratIndex));
            p = new SuffixNode(new Tuple<int, int>(i+sharedLen, text.Length - i-sharedLen));
            p.parent = curr;
            //p.value = new Tuple<int, int>(i, text.Length - i);
            p.index = tree.Count;
            curr.children.Add(p);
            tree.Add(p);
            //nodes[curr.index-1].value = new Tuple<int, int>(curr.value.Item1, index); ;
            tree[curr.index].value = new Tuple<int, int>(curr.value.Item1, sharedLen);
            //curr.value = new Tuple<int, int>(curr.value.Item1, index);

        }

        private void BreakEdge(List<SuffixNode> tree,SuffixNode curr, int sharedLen, int l)
        {
            SuffixNode p = new SuffixNode();
            p.children = curr.children;
            p.value = new Tuple<int, int>(curr.value.Item1 +sharedLen , l - sharedLen);
            p.parent = curr;
            p.index = tree.Count;
            /*for (int k = 0; k < curr.children.Count; k++)
            {
                nodes[curr.children[k].index].parent = p;
            }*/
            curr.children = new List<SuffixNode>();
            curr.children.Add(p);
            tree.Add(p);
        }

        private Tuple<int, int> FindNode(SuffixNode curr, int i,string text)
        {

            int index = -1 ;
            int counter = 0;
            int nodeIndex = -1;
            foreach (var child in curr.children)
            {
                //index = curr.children[j].value.Item1;
                index = child.value.Item1;
                char let = text[index];
                if (let == text[i])
                {
                    do
                    {
                        i++;
                        index++;
                        counter++;
                        nodeIndex = child.index;
                        if (index >= child.value.Item2)
                        {
                            if (curr.children.Count > 0)// vaghti ta inja az ferakht yeki boodr va hala chand shake mishe
                            {
                                return new Tuple<int, int>(counter, child.index);
                            }

                        }
                        let = text[index];
                        curr = child;

                    } while (let == text[i] && i < text.Length - 1 && index < child.value.Item2);
                    break;

                }
            }
            return new Tuple<int, int>(counter, nodeIndex);
        }

        /* private string[] Print(SuffixTree tree)
         {
             List<string> result = new List<string>();
             SuffixNode curr = tree.root;
             Queue<SuffixNode> queue = new Queue<SuffixNode>();
             queue.Enqueue(curr);
             while(queue.Count != 0)
             {
                 for(int i = curr.children.Count-1;i>=0;i--)
                 {
                     queue.Enqueue(curr.children[i]);
                 }
             }
             {

             }
         }*/
    }
}
