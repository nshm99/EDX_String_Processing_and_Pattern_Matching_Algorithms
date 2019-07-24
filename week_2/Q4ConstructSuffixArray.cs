using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) => 
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        public long[] Solve(string text)
        {
            sufficArray array = new sufficArray(text);
            array.MakeSuffix();
            return MakeResult(array.suffix);
        }

        private long[] MakeResult(List<Tuple<string, long>> suffix)
        {
            suffix.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            List<long> result = new List<long>();
            for(int i=0;i<suffix.Count;i++)
            {
                result.Add(suffix[i].Item2);
            }
            return result.ToArray();
        }
    }
}
