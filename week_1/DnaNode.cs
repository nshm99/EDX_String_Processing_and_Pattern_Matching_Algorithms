namespace A5
{
    public class DnaNode
    {
        public static int nodesCount=0;
        public char letter;
        public int number;
        public DnaNode parent;
        public DnaNode[] children = new DnaNode[5];// 0 ->A     1->G    2->T    3->A    4->'0'

        public DnaNode(char letter , DnaNode parent)
        {
            this.letter = letter;
            this.parent = parent;
            if(letter != '$')
            this.number = ++nodesCount;
        }

        public bool IsLeaf ()
        {
            if (children[4] != null)
                return true;
            return false;
        }

    }
}