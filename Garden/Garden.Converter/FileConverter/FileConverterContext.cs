using System.IO;

namespace Garden.Converter
{
    public class FileConverterContext
    {
        public string InputPath { get; set; }

        public string InputType
        {
            get { return string.IsNullOrWhiteSpace(InputPath) ? string.Empty : FileConveterUtil.GetFileExtention(InputPath); }
        }

        /// <summary>
        /// 最终期望的输出的文件的文件夹或者是完整的文件名
        /// </summary>
        public string TargetPath { get; private set; }

        /// <summary>
        /// 当TargetPath是文件夹时必须指定
        /// </summary>
        public string TargetType { get; private set; }


        public string ErrorMessage { get; set; }

        public bool IsConvertSuccess { get; set; } = true;


        public FileConverterContext(string targetPath, string targetType = null)
        {
            TargetPath = targetPath;
            TargetType = targetType;
            if (string.IsNullOrEmpty(targetPath))
            {
                TargetType = FileConveterUtil.GetFileExtention(InputPath);
            }
        }

    }
}