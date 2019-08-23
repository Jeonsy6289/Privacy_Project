using System;
using System.Collections.Generic;
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
using System.IO;
using System.Windows.Forms;


namespace PrivacyFinder
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public string selectfolder = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        public string ReadFile(string path) // 파일 읽고 텍스트를 추출하는 함수
        {
            return File.ReadAllText(path);
        }

        private void Open(object sender, RoutedEventArgs e)  // "폴더 열기" 버튼 이벤트
        {
            FolderBrowserDialog folderopen = new FolderBrowserDialog();
            folderopen.Description = "검색할 폴더";
            folderopen.ShowDialog();

            // 선택한 폴더의 경로
            selectfolder = folderopen.SelectedPath;
        }
    }
}
