using Algorithm.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.GreedyAlgorithms
{
    public class Frequencytem<T>
    {
        //C#没有bit类型。

        public BitArray Code;

        public T Key { get; set; }

        public int Frequency { get; set; }

        public override string ToString()
        {
            return $"{Key}   {Frequency}   Code:{ Code.ToZeroOneString()} ";
        }
    }
}
