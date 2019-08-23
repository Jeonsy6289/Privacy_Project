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
        #region 전역변수
        public string selectfolder = ""; // 개인정보를 가지고 있는 파일의 상위 폴더 경로

        string valuse = ""; // 파일을 탐색해서 찾은 전화번호

        string input = ""; // DetectPhoneNumber함수에 들어갈 인수 (선택한 폴더의 하위파일 내용)

        string selected = "";

        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        public string ReadFile(string path) // 파일 읽고 텍스트를 추출하는 함수
        {
            return File.ReadAllText(path);
        }

        private void Open(object sender, RoutedEventArgs e)  // "폴더 열기" 버튼 이벤트 폴더 창 띄우기
        {
            FolderBrowserDialog folderopen = new FolderBrowserDialog();
            folderopen.Description = "검색할 폴더";
            folderopen.ShowDialog();

            selectfolder = folderopen.SelectedPath;

            PathBox.Text = selectfolder;
        }

        private void Search(object sender, RoutedEventArgs e) // "검색" 버튼 이벤트 - (현재 전화번호만 식별가능) ListView에 개인정보를 포함하고 있는 파일 보이기
        {
            string[] paths = Directory.GetFiles(selectfolder);

            foreach (string file in paths)
            {
                if (System.IO.Path.GetExtension(file) == ".txt")
                {
                                        input = ReadFile(file);
                    int Privacy = Detectpattern(input, ref valuse);
                    if (Privacy > 0)
                    {
                        ListView.Items.Add(file);
                    }
                }
            }
        }

        public int Detectpattern (String input, ref String valuse) // 전화번호를 포함하고 있는 파일 추출
        {
            String[] patterns =
            {
                @"(^|\W)01[01]\d{7,8}(\W|$)",
                @"(^|\W)01[01]-\d{3,4}-\d{4}(\W|$)",
                @"((\w+\.)*\w+@(\w+\.)+[A-Za-z]+)",
                @"([0-9]{6}-[0-9]{7})",
                @"([0-9][0-9][01][0-9][0123][0-9]-[1234][0-9]{6})"
            };
            int count = 0;

            foreach (String pattern in patterns)
            {
                System.Text.RegularExpressions.MatchCollection matches =
                    System.Text.RegularExpressions.Regex.Matches(input, pattern);

                foreach (System.Text.RegularExpressions.Match m in matches)
                {
                    ++count;
                    valuse += m + "\n";
                }
            }
            return count;
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e) // ListView 더블클릭 시 TextBox에 데이터 보이기
        {
            string select = ListView.SelectedItem as string;
            selected = ReadFile(select);
            TextBox.Text = selected;
        }

        //private void Save(object sender, RoutedEventArgs e)
        //{
        //    File.WriteAllLines(selectfolder, filename);
        //}
    }
}




/* 
 * 저장 버튼 만들기
 * 검색창 만들기
 */
