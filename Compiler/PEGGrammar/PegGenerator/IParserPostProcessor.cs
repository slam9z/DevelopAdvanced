using Peg.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg.Generator
{

    public struct ParserPostProcessParams
    {
        public ParserPostProcessParams(string outputDirectory, string sourceFileTitle, string grammarFileName, PegNode root, string src, TextWriter errOut)
        {
            outputDirectory_ = outputDirectory;
            sourceFileTitle_ = sourceFileTitle;
            grammarFileName_ = grammarFileName;
            root_ = root;
            src_ = src;
            byteSrc_ = null;
            errOut_ = errOut;
            maxLineLength_ = 60;
            spacesPerTap_ = 4;
        }
        public ParserPostProcessParams(string outputDirectory, string sourceFileTitle, string grammarFileName, PegNode root, byte[] byteSrc, TextWriter errOut)
        {
            outputDirectory_ = outputDirectory;
            sourceFileTitle_ = sourceFileTitle;
            grammarFileName_ = grammarFileName;
            root_ = root;
            src_ = null;
            byteSrc_ = byteSrc;
            errOut_ = errOut;
            maxLineLength_ = 60;
            spacesPerTap_ = 4;
        }

        public readonly string outputDirectory_;
        public readonly string sourceFileTitle_;
        public readonly string grammarFileName_;
        public readonly PegNode root_;
        public readonly string src_;
        public readonly byte[] byteSrc_;
        public readonly TextWriter errOut_;
        public readonly int maxLineLength_;
        public readonly int spacesPerTap_;
    }


    public interface IParserPostProcessor
    {
        string ShortDesc { get; }
        string ShortestDesc { get; }
        string DetailDesc { get; }
        void Postprocess(ParserPostProcessParams postProcessorParams);
    }
}