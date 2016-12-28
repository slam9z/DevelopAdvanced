using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden.Converter
{
    public interface IFileConverter
    {
        /// <summary>
        /// 转换器能够接受多种类型文件
        /// </summary>
        IEnumerable<string> AcceptTypes { get; }

        /// <summary>
        /// 一个转换器只能输入一种类型
        /// </summary>
        string OutputType { get; }


        string Description { get; }

        /// <summary>
        /// 转换的权重，花的代价的评估
        /// </summary>
        int ConvertWeight { get; set; }

        IFileConverter Next { get; set; }

        FileConverterContext Convert(FileConverterContext context);


        Task<FileConverterContext> ConvertAsync(FileConverterContext context);

    }
}
