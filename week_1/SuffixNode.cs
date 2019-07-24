using System;
using System.Collections.Generic;

namespace A5
{
    internal class SuffixNode
    {
        public Tuple<int, int> value  ; // Tuple<int, int>   ---> Tuple<index, length>
        public List<SuffixNode> children = new List<SuffixNode>();
        public SuffixNode parent;
        public int index;
        public static int numbers = 0;
        public SuffixNode()
        {
            numbers++;
            index = numbers;
        }
        public SuffixNode(Tuple<int, int> value)
        {
            numbers++;
            index = numbers;
            this.value = value;
        }
    }
}