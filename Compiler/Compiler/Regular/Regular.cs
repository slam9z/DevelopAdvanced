using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Regular
{
    //不写代码总感觉自己啥都不会
    //自己从理解的角度当然是从最简单的开始
    //最简单的清楚了，再学习更复杂的
    public class Regular
    {
        private string _pattern;

        public Regular(string pattern)
        {
            _pattern = pattern;
        }

        public bool Match(string value)
        {
            return true;
        }

    }
}
