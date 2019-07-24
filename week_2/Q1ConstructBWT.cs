using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string text)
        {
            List<string> rotationString = new List<string>();
            string result = null;
            for (int i =0 ; i<text.Length ; i++)
            {
                rotationString.Add(rotation(text, i));
            }
            rotationString.Sort();
            foreach (var item in rotationString)
                result += item[item.Length-1];
            return result;

            throw new NotImplementedException();
        }

        private string rotation(string text, int i)
        {
            //char[] result = new char[text.Length];
            string result =null ;
            
            for(int j =0;j<text.Length;j++)
            {
                //result[j] =text[i % (text.Length)]; 
                result += text[i % (text.Length)];
                i++;
            }
            return result;
        }
    }
}
