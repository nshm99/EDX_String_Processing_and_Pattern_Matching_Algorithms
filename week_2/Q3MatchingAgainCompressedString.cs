using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) => 
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, String[] patterns)
        {
            string sortedbwt = Sort(text);
            BWt first = new BWt(sortedbwt);
            BWt second = new BWt(text);
            List<long> result = new List<long>();
            for(int i=0;i<patterns.Length;i++)
            {
                result.Add(BWMatching(first, second,patterns[i].ToList()));
            }
            return result.ToArray();
        }

        private long BWMatching(BWt first, BWt second, List<char> list)
        {
            long top = 0;
            long bottom = second.processedArray.Count()-1;
            while(top<=bottom)
            {
                if (list.Count != 0)
                {
                    char symbol = list[list.Count - 1];
                    int symNum = SymbolNumber(symbol);
                    list.RemoveAt(list.Count - 1);
                    Tuple<int, long, int> firstOcc = FindLett(top, bottom, second, symNum);
                    if (firstOcc.Item1 == -5)
                        return 0;
                    top = first.startINdex[firstOcc.Item1] + firstOcc.Item2 - 1;
                    long countAfterBott = AfterBott(bottom, second, symNum);
                    bottom = first.startINdex[firstOcc.Item1] + first.count[firstOcc.Item1]-countAfterBott-1;


                }
                else
                    return bottom - top + 1;
            }
            return -4;
        }

        private long AfterBott(long bottom, BWt second, int symNum)
        {
            for(int i=(int)bottom+1;i < second.text.Length;i++)
            {
                if (second.processedArray[i].Item1 == symNum)
                    return second.count[symNum]-second.processedArray[i].Item2+1;
            }
            return 0;
        }

        private int SymbolNumber(char symbol)
        {
            int symNum = -2;
            switch (symbol)
            {
                case 'A':
                    symNum = 0;
                    break;
                case 'G':
                    symNum = 1;
                    break;
                case 'C':
                    symNum = 2;
                    break;
                case 'T':
                    symNum = 3;
                    break;
                case '$':
                    symNum = 4;
                    break;
            }
            return symNum;
        }

        private Tuple<int, long,int> FindLett(long top, long bottom, BWt second, int symNum)
        {
            
            for(int i=(int)top;i<=(int)bottom;i++)
            {
                if(second.processedArray[i].Item1 == symNum)
                {
                    return second.processedArray[i];
                }
            }
            return new Tuple<int, long, int>(-5, -5, -5);
        }

        private string Sort(string bwt)
        {
            char[] a = bwt.ToArray();
            Array.Sort(a);
            return new string(a);
        }
    }
}
