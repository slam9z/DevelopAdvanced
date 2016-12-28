using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden.Converter
{
    public class FileConverterPath
    {
        /// <summary>
        /// 转换器能够接受多种类型文件
        /// </summary>
        public IEnumerable<string> AcceptTypes { get; }

        /// <summary>
        /// 一个转换器只能输入一种类型
        /// </summary>
        public string TargetType { get; }


        private IList<IFileConverter> _conveters;

        public FileConverterPath(IList<IFileConverter> conveters)
        {
            _conveters = conveters;

            TargetType = conveters.Last().OutputType;

            AcceptTypes = conveters.First().AcceptTypes;
        }


        public bool CheckPath()
        {
            IFileConverter currentConverter = null;
            foreach (var converter in _conveters)
            {

                if (currentConverter == null)
                {
                    currentConverter = converter;
                    continue;
                }

                if (!converter.AcceptTypes.Contains(currentConverter.OutputType))
                {
                    throw new ArgumentException("converter AcceptTypes and OutputType must sequence");
                }

                currentConverter = converter;
            }

            return true;
        }

        public bool CanConvert(FileConverterContext context)
        {
            return TargetType == context.TargetType
                   && AcceptTypes.Contains(context.InputType);
        }


        public bool Converter(FileConverterContext context)
        {
            foreach (var conveter in _conveters)
            {
                if (context.IsConvertSuccess)
                {
                    conveter.Convert(context);
                }
            }
            return true;
        }

        public bool ConverterAsync(FileConverterContext context)
        {
            return true;
        }
    }
}
