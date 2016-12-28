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
        //一个FileConverterService只能处理一个，避免多线程问题。要简化，调用者自己封装。已经移除Next属性

        //只保存类型可能需要大量反射，而且那些实例值也获取不了。不保留类型，多线程容易有很多坑。真尴尬！

        private static FileConverterService s_defaultInstance;

        public static FileConverterService DefaultInstance
        {
            get
            {
                if (s_defaultInstance == null)
                {
                    s_defaultInstance = new FileConverterService();
                }
                return s_defaultInstance;
            }
        }


        public static int DefaultConvertWeight { get; set; } = 100;

     

        private IDictionary<Type, IFileConverter> _conveters;

        private IList<FileConverterPath> _conveterPaths;

        private IList<FileConverterPath> _calcConveterPathsCache;



        public FileConverterService()
        {
            _conveters = new Dictionary<Type, IFileConverter>();
            _conveterPaths = new List<FileConverterPath>();
            _calcConveterPathsCache = new List<FileConverterPath>();
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

            if (string.IsNullOrEmpty(converter.OutputType))
            {
                throw new InvalidOperationException("converter OutputType can't null or empty");
            }

            if (converter.AcceptTypes == null || converter.AcceptTypes.Count() == 0)
            {
                throw new InvalidOperationException("converter AcceptTypes can't null or empty");
            }


            var type = converter.GetType();
            if (_conveters.ContainsKey(type))
            {
                return;
            }

            _conveters[type] = converter;

            _calcConveterPathsCache.Clear();
        }

        /// <summary>
        /// 按照实际转换顺序添加，指定特定的Path。
        /// </summary>
        /// <param name="converters"></param>
        public void RegisterConverterPath(IList<IFileConverter> converters)
        {
            //能优化吗？适用IList更合理

            if (converters == null || converters.Count() == 0)
            {
                throw new ArgumentException("converterPath can't null or empty");
            }

            foreach (var converter in converters)
            {
                if (string.IsNullOrEmpty(converter.OutputType))
                {
                    throw new ArgumentException("converter OutputType can't null or empty");
                }
            }


            var path = new FileConverterPath(converters);
            path.CheckPath();


            foreach (var converter in converters)
            {
                RegisterConverter(converter);
            }

            _conveterPaths.Add(path);
        }


        public bool Converter(FileConverterContext context)
        {
            var path = GetConvertPath(context);
            return path.Converter(context);
        }


        public bool ConverterAsync(FileConverterContext context)
        {
            var path = GetConvertPath(context);
            return path.ConverterAsync(context);
        }

        private FileConverterPath GetConvertPath(FileConverterContext context)
        {
            foreach (var path in _conveterPaths)
            {
                if (path.CanConvert(context))
                {
                    return path;
                }
            }

            foreach (var path in _calcConveterPathsCache)
            {
                if (path.CanConvert(context))
                {
                    return path;
                }
            }

            return CalculateConvertPath(context);
        }

        //需要使用最短路径算法，感觉有点复杂。这也是实际算法应用的一个挑战,乱复制就有多份了。

        private FileConverterPath CalculateConvertPath(FileConverterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
