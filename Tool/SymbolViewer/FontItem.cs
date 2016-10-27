using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SymbolViewer
{
	class FontItem
	{
		public string key { set; get; }
		public string value { get; set; }

		public string font { set; get; }

		public static List<FontItem> EnumeratorFontFamily(FontFamily family)
		{
			Dictionary<int, FontItem> fonts = new Dictionary<int, FontItem>();

			var typefaces = family.GetTypefaces();
			foreach (Typeface typeface in typefaces)
			{
				// 对应字体文件中包含的物理字体
				GlyphTypeface glyph;

				typeface.TryGetGlyphTypeface(out glyph);

				// 根据字体“CMAP”表的定义获取 Unicode 码位与标志符号索引之间的名义映射。 
				IDictionary<int, ushort> characterMap = glyph.CharacterToGlyphMap;


				foreach (KeyValuePair<int, ushort> kvp in characterMap)
				{
					//  Console.WriteLine(String.Format("{0}:{1}", kvp.Key, kvp.Value));

					int Key = kvp.Key; // 没有用到 kvp.Value

					// 如果大于 0xffff（为空字符，显示为小方框），则跳出循环
					if (Key > 0xffff) // 0 ~ 65535
					{
						break;
					}

					// 方法 1： Unicode 转换为 “符号” 字符表示
					// 例如，0xE189 转换为 byte[]{ 137, 225 }
					byte[] bytes = new byte[]
					 {
					   (byte) Key,
					   (byte) (Key >> 8)
					 };

					string Character = ByteToString(bytes);

					// 方法 2：Unicode 转换为 “符号” 字符
					//       可以用来转换 Key= 65535 以下的 Unicode，如果大于 65535，则会抛出：
					//       “ System.OverflowException：值对于字符太大或太小” 的异常
					// char c = Convert.ToChar(Key);
					// string Character = c.ToString();

					// 过滤掉空字符
					if (!string.IsNullOrEmpty(Character) &&
						!string.IsNullOrWhiteSpace(Character) &&
						!fonts.Keys.Contains(Key)// 去掉重复的
					   )
					{

						fonts.Add(Key, new FontItem { key = Key.ToString("X4"), font = Character });
					}
				}
			}

			return fonts.Values.ToList();
		}


		// 把二进制数转换成字符
		public static string ByteToString(byte[] array)
		{
			var enc = Encoding.Unicode;
			var chars = enc.GetChars(array);
			return new string(chars);
		}



		public static string ByteStringToString(string hexString)
		{
			try
			{
				var enc = Encoding.Unicode;
				int num = int.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
				var symbol = char.ConvertFromUtf32(num);
				return symbol;
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}
	}
}
