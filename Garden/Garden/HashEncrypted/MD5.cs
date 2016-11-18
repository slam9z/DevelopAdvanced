using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Garden.HashEncrypted
{

    public class MD5
    {
        ////Note: All variables are unsigned 32 bits and wrap modulo 2^32 when calculating
        //var int[64] r, k

        ////r specifies the per-round shift amounts
        //r[ 0..15]：= {7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22} 
        //r[16..31]：= {5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20}
        //r[32..47]：= {4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23}
        //r[48..63]：= {6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21}

        ////Use binary integer part of the sines of integers as constants:
        //for i from 0 to 63
        //    k[i] := floor(abs(sin(i + 1)) × 2^32)

        //var int h0 := 0x67452301
        //var int h1 := 0xEFCDAB89
        //var int h2 := 0x98BADCFE
        //var int h3 := 0x10325476

        private static int[] leftrotates = { 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
                            5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,
                            4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,
                            6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21
                          };
        private static uint[] table = new uint[64];

        private static uint[] tempresults = new uint[4];

        private string hexOutput = "";

        private static byte[] MD5ByteArray = new byte[16];

        public string MD5HexOutput
        {
            get
            {

                for (int i = 0; i < 16; i++)
                {
                    hexOutput += String.Format("{0:x2}", MD5ByteArray[i]);
                }
                return hexOutput;
            }
        }
        //常量表的初始化应该已经正确了啊
        static MD5()
        {
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = (uint)Math.Floor(Math.Abs(Math.Sin(i + 1)) * (2L << 31));


            };
        }


        private static void MD5Init()
        {

            //results[0] = 0x01234567;
            //results[1] = 0x89abcdef;
            //results[2] = 0xfedcba98;
            //results[3] = 0x76543210;

            tempresults[0] = 0x67452301;  //in memory, this is 0x01234567
            tempresults[1] = 0xefcdab89;  //in memory, this is 0x89abcdef
            tempresults[2] = 0x98badcfe;  //in memory, this is 0xfedcba98
            tempresults[3] = 0x10325476;  //in memory, this is 0x76543210 


        }

        private static byte[] UlongToBytes(ulong inputUlong)
        {
            // Console.WriteLine(inputUlong);
            byte[] outputBytes = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                outputBytes[7 - i] = (byte)((inputUlong >> 8 * (7 - i)) & 0xFF);
                //   Console.WriteLine(outputBytes[i]);
            }

            return outputBytes;

        }

        private static uint LeftRotate(uint num, int bit)
        {
            return (num << bit) | (num >> (32 - bit));
        }
        //输出结果填充也没错啊
        private static uint[] MD5Append(byte[] Source)
        {
            ////Pre-processing:
            //append "1" bit to message
            //append "0" bits until message length in bits ≡ 448 (mod 512)
            //append bit length of message as 64-bit little-endian integer to message

            byte[] PaddingData = {128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                             0, 0, 0, 0 };
            int paddedLength;
            int needPaddingSize;
            int SourceLength = Source.Length;
            byte[] paddedSource;
            byte[] lengthBytes = UlongToBytes((ulong)SourceLength * 8);

            paddedLength = 64 * (SourceLength / 64 + 1);

            paddedSource = new byte[paddedLength];

            needPaddingSize = paddedLength - SourceLength - 8;

            for (int i = 0; i < paddedLength; i++)
            {
                if (i < SourceLength)
                {
                    paddedSource[i] = Source[i];
                }
                else if (i < needPaddingSize + SourceLength)
                {
                    paddedSource[i] = PaddingData[i - SourceLength];
                }
                else
                {
                    paddedSource[i] = lengthBytes[i - SourceLength - needPaddingSize];
                }

            }
            // Program.PrintBytes(Source);
            // Program.PrintBytes(paddedSource);

            uint[] target = new uint[paddedLength / 4];


            for (int i = 0, j = 0; i < paddedLength; j++, i += 4)
            {
                target[j] = (uint)(paddedSource[i] | paddedSource[i + 1] << 8 | paddedSource[i + 2] << 16 | paddedSource[i + 3] << 24);
                //target[j] = (uint)(paddedSource[i]<<24 | paddedSource[i + 1] << 16| paddedSource[i + 2] << 8 | paddedSource[i + 3] );
                //  Console.Write("uint: {0:x}\t" , target[j]);

            }
            return target;

        }

        private static uint[] MD5Trasform(uint[] input)
        {
            //    //Initialize hash value for this chunk:
            //    var int a := h0
            //    var int b := h1
            //    var int c := h2
            //    var int d := h3
            uint a = tempresults[0];
            uint b = tempresults[1];
            uint c = tempresults[2];
            uint d = tempresults[3];
            uint calValue;
            uint index;

            //    Console.WriteLine("{0:x} {1:x} {2:x} {3:x} ", a, b, c, d);
            //Main loop:
            //for i from 0 to 63
            for (uint i = 0; i < 64; i++)
            {
                //if 0 ≤ i ≤ 15 then
                //      f := (b and c) or ((not b) and d)
                //      g := i
                if (i <= 15)
                {

                    calValue = (b & c) | ((~b) & d);
                    index = i;
                }
                //else if 16 ≤ i ≤ 31
                //       f := (d and b) or ((not d) and c)
                //       g := (5×i + 1) mod 16
                else if (i <= 31)
                {

                    calValue = (d & b) | ((~d) & c);
                    index = (5 * i + 1) % 16;
                }
                //else if 32 ≤ i ≤ 47
                //     f := b xor c xor d
                //     g := (3×i + 5) mod 16
                else if (i <= 47)
                {
                    calValue = b ^ c ^ d;
                    index = (3 * i + 5) % 16;
                }
                //else if 48 ≤ i ≤ 63
                //      f := c xor (b or (not d))
                //      g := (7×i) mod 16

                else
                {
                    calValue = c ^ (b | (~d));
                    index = (7 * i) % 16;
                }
                //    temp := d
                //    d := c
                //    c := b
                //    b := leftrotate((a + f + k[i] + w[g]),r[i]) + b
                //    a := temp

                uint temp = a + calValue + table[i] + input[index];
                temp = LeftRotate(temp, leftrotates[i]);
                a = b + temp;

                //a,b,c,d循环右移

                //效果都是一样的不过有的清晰罢了

                //   if( (i+1) %4==1)
                //     Console.WriteLine("{0:x} {1:x} {2:x} {3:x} 0x{4:x} 0x{5:x} {6}  i {7}", a, b, c, d, table[i], input[index], leftrotates[i], i + 1);

                temp = d;
                d = c;
                c = b;
                b = a;
                a = temp;

                //uint temp = d;
                //d = c;
                //c = b;
                //b = LeftRotate((a + calValue + table[i] + input[index]), leftrotates[i]) + b;
                //a = temp;


            }
            //    Next i
            //    //Add this chunk's hash to result so far:
            //    h0 := h0 + a
            //    h1 := h1 + b 
            //    h2 := h2 + c
            //    h3 := h3 + d
            tempresults[0] += a;
            tempresults[1] += b;
            tempresults[2] += c;
            tempresults[3] += d;

            //  Console.WriteLine("{0:x} {1:x} {2:x} {3:x} ", tempresults[0], tempresults[1], tempresults[2], tempresults[3]);

            return tempresults;
        }

        private static void GetMD5ByteArray(uint[] MD5UnintArray)
        {
            MD5ByteArray = new byte[16];
            for (int i = 0, j = 0; i < 4; i++, j += 4)
            {
                MD5ByteArray[j] = (byte)(MD5UnintArray[i] & 0xff);
                MD5ByteArray[j + 1] = (byte)((MD5UnintArray[i] >> 8) & 0xff);
                MD5ByteArray[j + 2] = (byte)((MD5UnintArray[i] >> 16) & 0xff);
                MD5ByteArray[j + 3] = (byte)((MD5UnintArray[i] >> 24) & 0xff);
            }

        }

        public MD5(byte[] Date)
        {
            uint[] PaddedData;
            uint[] block = new uint[16];
            MD5Init();

            PaddedData = MD5Append(Date);

            for (int i = 0; i < PaddedData.Length / 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    block[j] = PaddedData[i * 16 + j];
                }
                MD5Trasform(block);
            }
            //最终results数组存储MD5的32位无符号整数形式

            GetMD5ByteArray(tempresults);


        }

        public MD5(string StringDate) : this(System.Text.Encoding.Default.GetBytes(StringDate))
        {

        }




        ////Process the message in successive 512-bit chunks:
        //for each 512-bit chunk of message
        //    break chunk into sixteen 32-bit little-endian words w[i], 0 ≤ i ≤ 15


        //End ForEach
        //var int digest := h0 append h1 append h2 append h3 //(expressed as little-endian)


    }


}
