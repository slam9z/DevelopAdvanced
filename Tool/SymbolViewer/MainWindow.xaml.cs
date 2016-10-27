using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SymbolViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;


            txtSample.Text = "\uE189";
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FontFamilyMapCollection ffCollection;

            // 遍历当前 PC 上的所有字体。参考 MSDN：
            // https://msdn.microsoft.com/zh-cn/library/system.windows.media.fontfamily.aspx
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                if (fontFamily.Source == "Segoe UI Symbol" || fontFamily.Source == "Segoe MDL2 Assets" || fontFamily.Source == "Segoe UI Emoji")
                {
                    ffCollection = fontFamily.FamilyMaps;

                    listBox.Items.Add(fontFamily.Source);
                }

                fullFamily.Items.Add(fontFamily.Source);
            }
        }

        // 选中相应 “图标字体” 后，跳转到 “查看窗口”
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string fontName = (sender as TextBlock).Text;

            SymbolWindow sw = new SymbolWindow();

            // 使用静态属性，作为参数传递
            SymbolWindow.CurrentFontFamily = new FontFamily(fontName); // 选中的字体

            sw.ShowDialog();
        }
    }
}
