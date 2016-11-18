using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SymbolViewer
{
	/// <summary>
	/// Interaction logic for SymbolWindow.xaml
	/// </summary>
	public partial class SymbolWindow : Window
	{
		public SymbolWindow()
		{
			InitializeComponent();
			this.Loaded += Segoe_UI_Symbol_Loaded;
		}


		public FontFamily _fontFamily { get; set; }
		public FontStyle _fontStyle { get; set; }
		public string _fontText { get; set; }
		public double _fontSize { get; set; }
		public int _stroke { get; set; }

		public SolidColorBrush _foreground { get; set; }


		private Typeface _fypeface;
		/// <summary>
		/// 作为从 MainWindow.xaml 传递来的参数
		/// </summary>
		public static FontFamily CurrentFontFamily;

		private void Segoe_UI_Symbol_Loaded(object sender, RoutedEventArgs e)
		{
			// FontFamily ff = new FontFamily("Segoe MDL2 Assets");

			if (CurrentFontFamily != null)
			{
				this.FontFamily = CurrentFontFamily;
				_fontFamily = CurrentFontFamily;

				this.Title = " 当前字体 : " + CurrentFontFamily.Source;

				listBox.ItemsSource = FontItem.EnumeratorFontFamily(CurrentFontFamily);
			}

			fontDispaly.FontFamily = CurrentFontFamily;
		}


		private void SetFont()
		{
			double fontSize;
			double.TryParse(fontSizeInput.Text, out fontSize);
			_fontSize = fontSize == 0d ? 16 : fontSize;

			if (fontColorInput.SelectedIndex == 0)
			{
				_foreground = new SolidColorBrush(Colors.Black);
			}
			else
			{
				_foreground = new SolidColorBrush(Colors.White);
			}

			_fontText = FontItem.ByteStringToString(fontInput.Text);
		}


		private void fontPreview_Click(object sender, RoutedEventArgs e)
		{
			SetFont();

			fontDispaly.FontSize = _fontSize;

			fontDispaly.Foreground = _foreground;

			fontDispaly.Text = _fontText;

		}




		private void fontOutput_Click(object sender, RoutedEventArgs e)
		{
			SetFont();
			var dlg = new SaveFileDialog();
			dlg.FileName =string.Format("font_{0}_{1}_{2}", fontInput.Text, _fontSize,_fontText) ; // Default file name
			dlg.DefaultExt = ".png"; // Default file extension
			if (dlg.ShowDialog().Value == true)
			{
				string localFilePath = dlg.FileName.ToString(); //获得文件路径 
				string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

				//获取文件路径，不带文件名
				var filePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

				//给文件名前加上时间
				var newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;


				var fs = (System.IO.FileStream)dlg.OpenFile();//输出文件 

			
				var ms = GetImageFromText();
				ms.CopyTo(fs);
				fs.Close();
				ms.Close();
				//fs输出带文字或图片的文件，就看需求了 
			}

		}







		private Stream GetImageFromText()
		{
			MemoryStream ms = null;
			DrawingVisual drawingVisual = new DrawingVisual();
			using (DrawingContext dc = drawingVisual.RenderOpen())
			{
				var typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretches.Normal);
				FormattedText ft = new FormattedText(_fontText, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, _fontSize, _foreground);
				Geometry geometry = ft.BuildGeometry(new Point(0.0, 0.0));
				dc.DrawText(ft, new Point(0.0, 0.0));
				dc.Close();

			}

			//dpi可以自己设定   // 获取dpi方法：PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice

			RenderTargetBitmap bitmap = new RenderTargetBitmap(100, 100, 72, 72, PixelFormats.Pbgra32);
			bitmap.Render(drawingVisual);

			PngBitmapEncoder encode = new PngBitmapEncoder();
			encode.Frames.Add(BitmapFrame.Create(bitmap));
			ms = new MemoryStream();
			encode.Save(ms);
			ms.Position = 0;
			return ms;
		}
	}
}

