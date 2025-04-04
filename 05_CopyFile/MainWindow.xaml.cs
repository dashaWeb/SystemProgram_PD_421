using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _05_CopyFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        PercentageInfo info;
        public MainWindow()
        {
            InitializeComponent();
            info = new PercentageInfo() { 
                Percentage = 0,
                Number = ""
            };
            srcTextBox.Text = info.Source = @"C:\Users\konopelko\Downloads\TaskParallelLib.rar";
            destTextBox.Text = info.Destination = @"C:\Users\konopelko\Downloads\Test";
            this.DataContext = info;
        }

        private void OpenSourceBtn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                srcTextBox.Text = info.Source = dialog.FileName;
            }
        }

        private void OpenDestBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                destTextBox.Text = info.Destination = dialog.FileName;
            }
        }

        private async void CopyBtn(object sender, RoutedEventArgs e)
        {
            // way1
            string FileName = System.IO.Path.GetFileName(info.Source);
            string destFilePath = System.IO.Path.Combine(info.Destination,FileName);
            /*MessageBox.Show(destFilePath);
            File.Copy(Source, destFilePath, true);*/

            //way2 
            /*using (FileStream srcStream= new FileStream(Source,FileMode.Open, FileAccess.Read))
            {
                using(FileStream destStream = new FileStream(destFilePath,FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024 * 8]; // 8Kb
                    int bytes = 0;
                    do
                    {
                        bytes =srcStream.Read(buffer, 0, buffer.Length);
                        destStream.Write(buffer, 0, bytes);
                        float percentage = srcStream.Length / destStream.Length * 100;
                        progress.Value = percentage;

                    } while (bytes > 0);
                }
            }*/
            await CopyFileAsync(info.Source,destFilePath);
            MessageBox.Show("Complate");
        }
        private Task CopyFileAsync(string src, string dest)
        {
            return Task.Run(() => {
                using (FileStream srcStream = new FileStream(src, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream destStream = new FileStream(dest, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[1024 * 8]; // 8Kb
                        int bytes = 0;
                        do
                        {
                            bytes = srcStream.Read(buffer, 0, buffer.Length);
                            destStream.Write(buffer, 0, bytes);
                            float percentage = destStream.Length / (srcStream.Length / 100);
                            info.Percentage = (int)percentage;
                            info.Number = $"{info.Percentage}%";
                        } while (bytes > 0);
                    }
                }
            });
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class PercentageInfo
    {
        public int Percentage { get; set; }
        public string Number { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
