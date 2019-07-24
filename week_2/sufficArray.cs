using System;
using System.Collections.Generic;

namespace A6
{
    internal class sufficArray
    {
        private string text;
        public List<Tuple<string,long>> suffix = new List<Tuple<string, long>>();

        public sufficArray(string text)
        {
            this.text = text;
        }

        public void MakeSuffix()
        {
            for(int i=0;i<text.Length;i++)
            {
                suffix.Add(new Tuple<string, long>(text.Substring(i), i));
            }
        }
    }
}