using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q3PatternMatchingSuffixArray : Processor
    {
        public Q3PatternMatchingSuffixArray(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
            
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve, "\n");

        public long[] Solve(string text, long n, string[] patterns)
        {
            long[] startIndex = new long[4];//&,A,C,G,T
            long[] totalCount = new long[4];//&,A,C,G,T;
            /*for (int i = 0; i < startIndex.Length; i++)
            {
                startIndex[i] = -1;
                totalCount[i] = 0;
            }*/
            long[] suffixArray = Solve(text, totalCount, startIndex);
            bool[] IsInResult = new bool[text.Length];
            List<long> result = new List<long>();
            foreach (var item in patterns)
            {
                int index = FindeLetterndex(item[0]);
                if (totalCount[index] > 0)
                    for (long i = startIndex[index]; i < startIndex[index] + totalCount[index]; i++)
                    {
                        if (CheckEqual(suffixArray, i, item, text))
                        {
                            if (IsInResult[suffixArray[i]] != true)
                            {
                                result.Add(suffixArray[i]);
                                IsInResult[suffixArray[i]] = true;
                            }
                        }

                    }
                //result.Add(FindPattern(suffixArray, item,text));
            }
            if (result.Count == 0)
                result.Add(-1);
            return result.ToArray();
        }

        private int FindeLetterndex(char v)
        {
            int index = -1;
            switch (v)
            {
                case 'A':
                    {
                        index = 0;
                        break;
                    }
                case 'C':
                    {
                        index = 1;
                        break;
                    }
                case 'G':
                    {
                        index = 2;
                        break;
                    }
                case 'T':
                    {
                        index = 3;
                        break;
                    }

            }
            return index;
        }


        private bool CheckEqual(long[] suffixArray, long i, string item, string text)
        {
            if (i == -1)
                return false;
            long index = suffixArray[i];
            int stringIndex = 0;
            while (index < suffixArray.Length && stringIndex < item.Length)
            {
                if (item[stringIndex] != text[(int)index])
                    return false;
                index++;
                stringIndex++;
            }
            if (stringIndex < item.Length)
                return false;
            return true;

        }

        public long[] Solve(string text, long[] totalCount, long[] startIndex)
        {
            long[] order = new long[text.Length];
            long[] classes = new long[text.Length];
            order = SortCharacters(text, totalCount, startIndex);
            classes = ComputeCharClasses(order, text);
            long l = 1;
            while (l < text.Length)
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
            for (int i = 1; i < n; i++)
            {
                long cur = order[i];
                long prev = order[i - 1];
                long mid = (cur + l) % n;
                long midPrev = (prev + l) % n;
                if (classes[(int)cur] != classes[(int)prev] ||
                    classes[(int)mid] != classes[(int)midPrev])
                    newClass[(int)cur] = newClass[(int)prev] + 1;
                else
                    newClass[(int)cur] = newClass[(int)prev];

            }
            return newClass;

        }

        public long[] SortDoubled(string text, long l, long[] order, long[] classes)
        {
            long[] count = new long[text.Length];
            long[] newOrder = new long[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                count[(int)classes[i]]++;
            }

            for (int i = 1; i < text.Length; i++)
                count[i] = count[i] + count[i - 1];

            for (int i = text.Length - 1; i >= 0; i--)
            {
                long start = (order[i] - l + text.Length) % (text.Length);
                long cl = classes[(int)start];
                count[(int)cl]--;
                newOrder[(int)count[(int)cl]] = start;
            }
            return newOrder;
        }

        public long[] ComputeCharClasses(long[] order, string text)
        {
            long[] classes = new long[text.Length];
            classes[(int)order[0]] = 0;
            for (int i = 1; i < text.Length; i++)
            {
                if (text[(int)order[i]] != text[(int)order[i - 1]])
                    classes[(int)order[i]] = classes[(int)order[i - 1]] + 1;

                else
                    classes[(int)order[i]] = classes[(int)order[i - 1]];
            }
            return classes;

        }

        public long[] SortCharacters(string text, long[] totalCount, long[] startIndex)
        {
            //List<long> order = new List<long>();
            long[] order = new long[text.Length];
            long[] count = new long[4];//a=1    c=2     g=3     t=4     $=5
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case 'A':
                        {
                            count[0]++;
                            totalCount[0]++;
                            
                            break;
                        }
                    case 'C':
                        {
                            count[1]++;
                            totalCount[1]++;
                            
                            break;
                        }
                    case 'G':
                        {
                            count[2]++;
                            totalCount[2]++;
                            
                            break;
                        }
                    case 'T':
                        {
                            count[3]++;
                            totalCount[3]++;
                            
                            break;
                        }



                }
            }
            startIndex[0] = 0;
            for (int i = 1; i < 4; i++)
            {
                count[i] = count[i] + count[i - 1];
                startIndex[i] =  count[i-1];
            }
            for (int i = text.Length - 1; i >= 0; i--)
            {
                int index = -1;
                switch (text[i])
                {
                    case 'A':
                        index = 0;
                        break;
                    case 'C':
                        index = 1;
                        break;
                    case 'G':
                        index = 2;
                        break;
                    case 'T':
                        index = 3;
                        break;


                }
                order[(int)(--count[index])] = i;
                /*if (startIndex[index] == -1)
                    startIndex[index] = count[index];
                else
                    if (count[index] < startIndex[index])
                    startIndex[index] = count[index];
                    */
            }
            return order;
        }
    }
}
