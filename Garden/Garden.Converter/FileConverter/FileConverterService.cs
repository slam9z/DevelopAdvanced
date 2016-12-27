using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Garden.Converter
{
    /// <summary>
    /// 适用于有大量的转换
    /// </summary>
    public class FileConverterService
    {
        //一个FileConverterService只能处理一个，避免多线程问题。要简化，调用者自己封装。

        //private static FileConverterService s_defaultInstance;

        //public static FileConverterService DefaultInstance
        //{
        //    get
        //    {
        //        if (s_defaultInstance == null)
        //        {
        //            s_defaultInstance = new FileConverterService();
        //        }
        //        return s_defaultInstance;
        //    }
        //}


        public static int DefaultConvertWeight { get; set; } = 100;

        //只保存类型可能需要大量反射，而且那些实例值也获取不了。不保留类型，多线程容易有很多坑。真尴尬！

        private IDictionary<Type, IFileConverter> _conveters;

        private IList<IList<IFileConverter>> _conveterPaths;


        public FileConverterService()
        {
            _conveters = new Dictionary<Type, IFileConverter>();
            _conveterPaths = new List<IList<IFileConverter>>();

        }

        public IFileConverter GetConverter(Type type)
        {
            if (_conveters.ContainsKey(type))
            {
                return null;
            }
            return _conveters[type];
        }

        public void RegisterConverter(IFileConverter converter)
        {
            var type = converter.GetType();
            if (_conveters.ContainsKey(type))
            {
                return;
            }
            _conveters[type] = converter;
        }

        /// <summary>
        /// 按照实际转换顺序添加，指定特定的Path。
        /// </summary>
        /// <param name="converterPath"></param>
        public void RegisterConverterPath(IList<IFileConverter> converterPath)
        {
            //能优化吗？适用IList更合理

            if (converterPath == null || converterPath.Count() == 0)
            {
                throw new ArgumentException("converterPath can't null or empty");
            }

            IFileConverter currentConverter = null;

            foreach (var converter in converterPath)
            {
                if (string.IsNullOrEmpty(converter.OutputType))
                {
                    throw new ArgumentException("converter OutputType can't null or empty");
                }
            }


            foreach (var converter in converterPath)
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


            foreach (var converter in converterPath)
            {
                RegisterConverter(converter);
            }

            _conveterPaths.Add(converterPath);
        }


        public bool Converter(FileConverterContext context)
        {
            var path = GetConvertPath(context);
            return true;
        }


        public bool ConverterAsync(FileConverterContext context)
        {
            var path = GetConvertPath(context);
            return true;
        }

        private IList<IFileConverter> GetConvertPath(FileConverterContext context)
        {
            foreach (var path in _conveterPaths)
            {
                if (path.Last().OutputType == context.TargetType
                    || path.First().AcceptTypes.Contains(context.InputType))
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        path[i].Next = path[i + 1];
                    }
                    return path;
                }
            }

            return CalculateConvertPath(context);
        }

        //需要使用最短路径算法，感觉有点复杂。这也是实际算法应用的一个挑战,乱复制就有多份了。

        private IList<IFileConverter> CalculateConvertPath(FileConverterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
