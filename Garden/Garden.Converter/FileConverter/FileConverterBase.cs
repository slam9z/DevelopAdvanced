using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden.Converter
{
    public class PdfToSwfConverter : IFileConverter
    {
        public  vitual IEnumerable<string> AcceptTypes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int ConvertWeight
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string OutputType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public FileConverterContext Convert(FileConverterContext context)
        {
            throw new NotImplementedException();
        }

        public Task<FileConverterContext> ConvertAsync(FileConverterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
