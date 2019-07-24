using A6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q2CunstructSuffixArray : Processor
    {
        public Q2CunstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long[]>)Solve);

        public long[] Solve(string text)
        {
            long[] order = new long[text.Length];
            long[] classes = new long[text.Length];
            order = SortCharacters(text);
            classes = ComputeCharClasses(order, text);
            long l = 1;
            while(l<text.Length)
            {
                order = SortDoubled(text, l, order, classes);
                classes = UpdateClasses(order, classes, l);
                l *= 2;
            }
            return order.ToArray();
          /*  sufficArray array = new sufficArray(text);
            array.MakeSuffix();
            return MakeResult(array.suffix);*/
        }

        public long[] UpdateClasses(long[] order, long[] classes, long l)
        {
            int n = order.Length;
            long[] newClass = new long[n];
            newClass[(int)order[0]] = 0;
            for(int i =1;i<n;i++)
            {
                long cur = order[i];
                long prev = order[i - 1];
                long mid = (cur + l) % n;
                long midPrev = (prev + l) % n;
                if (classes[(int)cur] != classes[(int)prev] ||
                    classes[(int)mid] != classes[(int)midPrev])
                    newClass[(int)cur] = newClass[(int)prev] + 1;
                else
                    newClass[(int)cur] = newClass[(int)prev] ;

            }
            return newClass;
            
        }

        public long[] SortDoubled(string text, long l, long[] order,long[] classes)
        {
            long[] count = new long[text.Length];
            long[] newOrder = new long[text.Length];
            for (int i = 0; i < text.Length; i++)
                count[(int)classes[i]]++;

            for(int i=1;i<text.Length;i++)
                count[i] = count[i] + count[i - 1];

            for(int i= text.Length-1;i>=0;i--)
            {
                long start = (order[i] - l + text.Length) % (text.Length);
                long cl = classes[(int) start];
                count[(int)cl]--;
                newOrder[(int) count[(int)cl]] = start;
            }
            return newOrder;
        }

        public  long[] ComputeCharClasses(long[] order, string text)
        {
            long[] classes = new long[text.Length];
            classes[(int)order[0]] = 0;
            for(int i=1;i<text.Length;i++)
            {
                if(text[(int) order[i]] != text[(int)order[i-1]])
                    classes[(int)order[i]] = classes[(int)order[i - 1]] + 1;
                
                else
                    classes[(int)order[i]] = classes[(int)order[i - 1]] ;
            }
            return classes;

        }

        public  long[] SortCharacters(string text)
        {
            //List<long> order = new List<long>();
            long[] order = new long[text.Length];
            long[] count = new long[5];//a=1    c=2     g=3     t=4     $=5
            for(int i=0; i<text.Length;i++)
            {
                switch(text[i])
                {
                    case 'A':
                        count[1]++;
                        break;
                    case 'C':
                        count[2]++;
                        break;
                    case 'G':
                        count[3]++;
                        break;
                    case 'T':
                        count[4]++;
                        break;
                    case '$':
                        count[0]++;
                        break;


                }
            }
            for (int i = 1; i < 5; i++)
                count[i] = count[i] + count[i - 1];
            for(int i= text.Length-1;i>=0;i--)
            {
                int index = -1;
                switch (text[i])
                {
                    case 'A':
                        index = 1;
                        break;
                    case 'C':
                        index = 2;
                        break;
                    case 'G':
                        index = 3;
                        break;
                    case 'T':
                        index = 4;
                        break;
                    case '$':
                        index = 0;
                        break;

                }
                order[(int)(--count[index])] = i;

            }
            return order; 
        }
        /*private long[] MakeResult(List<Tuple<string, long>> suffix)
{
   suffix.Sort((x, y) => x.Item1.CompareTo(y.Item1));
   List<long> result = new List<long>();
   for (int i = 0; i < suffix.Count; i++)
   {
       result.Add(suffix[i].Item2);
   }
   return result.ToArray();
}
*/
    }
}
