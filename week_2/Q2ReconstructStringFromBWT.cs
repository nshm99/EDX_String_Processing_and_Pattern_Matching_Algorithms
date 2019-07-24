using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName) : base(testDataName)
        {
            
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string bwt)
        {
            string sortedbwt = Sort(bwt);
            BWt first = new BWt(sortedbwt);
            BWt second = new BWt(bwt);
            StringBuilder strResult = new StringBuilder(bwt);
            string result ;
            int len = bwt.Length-1;
            strResult[(int)len] = '$';
            len--;
            Tuple<int, long,int> curr = second.processedArray[0];
            
            while(len>=0)
            {
                curr = find(second.processedArray[curr.Item3],first,second);
                if (len < bwt.Length && len>-1)
                    strResult[(int)len] = returnLet(curr.Item1);
                    
                len--;
            }
            result = strResult.ToString();   
            return result;
        }

       

        private char returnLet(int item1)
        {
            switch(item1)
            {
                case 0:
                    return 'A';
                case 1:
                    return 'G';
                case 2:
                    return 'C';
                case 3:
                    return 'T';
                case 4:
                    return '$';
            }
            return ' ';
            
        }

        private string Sort(string bwt)
        {
            char[] a = bwt.ToArray();
            Array.Sort(a);
            return new string(a);
        }

        
        private Tuple<int, long,int> find(Tuple<int, long,int> curr, BWt first, BWt second)
        {
            long count = curr.Item2 - 1;
            long indexF = curr.Item1;
            if(indexF>-1 && indexF <first.startINdex.Length)
            indexF = first.startINdex[indexF]+count;
            if(indexF>-1 && indexF<first.processedArray.Count)
            return first.processedArray[(int)indexF];
            return new Tuple<int, long, int>(-3, -3, -4);
        }

    }
}
