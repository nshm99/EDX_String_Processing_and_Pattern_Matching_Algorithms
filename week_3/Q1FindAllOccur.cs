using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String, long[]>)Solve,"\n");

        public long[] Solve(string text, string pattern)
        {
            string s = pattern + '$' + text;
            long[] sArray = ComputePrefixFunc(s);
            List<long> result = new List<long>();
            for(int i=pattern.Length+1; i<s.Length;i++)
            {
                if (sArray[i] == pattern.Length)
                    result.Add(i - (2 * pattern.Length));
            }
            if (result.Count == 0)
                result.Add(-1);
            return result.ToArray();
            
        }

        private long[] ComputePrefixFunc(string s)
        {
            long[] result = new long[s.Length];
            long border = 0;
            result[0] = 0;
            for(int i=1;i<s.Length;i++)
            {
                while(border>0 && s[i]!= s[(int) border])
                {
                    border = result[border - 1];
                }
                if (s[i] == s[(int)border])
                    border++;
                else
                    border = 0;
                result[i] = border;
            }

            return result;
        }
    }
}
