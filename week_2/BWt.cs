using System;
using System.Collections.Generic;

namespace A6
{
    internal class BWt
    {
        public string text;
        public long[] count = new long[5];//a:0     g:1     c:2     t:3   $:4
        public long[] startINdex = new long[5];
        public List<Tuple<int, long,int>> processedArray;//int= number of letter      long = count

        public BWt(string bwt )
        {
            this.text = bwt;
            count[0] = 0;
            count[1] = 0;
            count[2] = 0;
            count[3] = 0;
            count[4] = 0;
            startINdex[0] = -1;
            startINdex[1] = -1;
            startINdex[2] = -1;
            startINdex[3] = -1;
            startINdex[4] = -1;
            processedArray = new List<Tuple<int, long,int>>();
            makeArray();
        }

        private void makeArray()
        {
            for(int i = 0; i < text.Length; i++)
            {

                switch(text[i])
                {
                    case 'A':
                        count[0]++;
                        processedArray.Add(new Tuple<int, long,int>(0, count[0],i));
                        if (count[0] == 1)
                            startINdex[0] = processedArray.Count-1;
                        break;
                    case 'G':
                        count[1]++;
                        processedArray.Add(new Tuple<int, long,int>(1, count[1],i));
                        if (count[1] == 1)
                            startINdex[1] = processedArray.Count - 1;
                        break;
                    case 'C':
                        count[2]++;
                        processedArray.Add(new Tuple<int, long,int>(2, count[2],i));
                        if (count[2] == 1)
                            startINdex[2] = processedArray.Count - 1;
                        break;
                    case 'T':
                        count[3]++;
                        processedArray.Add(new Tuple<int, long,int>(3, count[3],i));
                        if (count[3] == 1)
                            startINdex[3] = processedArray.Count - 1;
                        break;
                    case '$':
                        count[4]++;
                        processedArray.Add(new Tuple<int, long, int>(4, count[4], i));
                        if (count[4] == 1)
                            startINdex[4] = processedArray.Count - 1;
                        break;
                }
            }
        }
    }
}