using Algorithm.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Regular
{
    public class RegularNodeItem
    {
        public char Char { get; set; }

        /// <summary>
        /// 表明时候是* | 等符号。
        /// </summary>
        public bool IsSymbol { get; set; }
    }
}
