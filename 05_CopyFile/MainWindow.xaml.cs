using IOExtensions;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        ViewModel model;
        int id = 0;
        public MainWindow()
        {
            InitializeComponent();
            model = new ViewModel()
            {
                Source = @"C:\Users\konopelko\Downloads\TaskParallelLib.rar",
                Destination = @"C:\Users\konopelko\Downloads\Test",
                Progress = 0,
            };
            srcTextBox.Text = model.Source;
            destTextBox.Text = model.Destination;


            this.DataContext = model;
        }

        private void OpenSourceBtn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                srcTextBox.Text = model.Source = dialog.FileName;
            }
        }

        private void OpenDestBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                destTextBox.Text = model.Destination = dialog.FileName;
            }
        }

        private async void CopyBtn(object sender, RoutedEventArgs e)
        {

            string FileName = System.IO.Path.GetFileName(model.Source);
            string destFilePath = System.IO.Path.Combine(model.Destination, $"{FileName}({id++})");

            CopyProcessInfo info = new CopyProcessInfo(FileName);
            model.AddProcess(info);
            info.Percentage = 100;
            await CopyFileAsync(model.Source, destFilePath, info);
            
            
            // way1
            //string FileName = System.IO.Path.GetFileName(model.Source);
            //string destFilePath = System.IO.Path.Combine(model.Destination, FileName);
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
            //await CopyFileAsync(model.Source, destFilePath);

        }
        private Task CopyFileAsync(string src, string dest, CopyProcessInfo info)
        {


            return FileTransferManager.CopyWithProgressAsync(src, dest, (progress) => {
                model.Progress = progress.Percentage;
                info.Percentage = progress.Percentage;
                info.BytesSecond = progress.BytesPerSecond;
              
            },false);


            /*return Task.Run(() =>
            {
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
                            *//*info.Percentage = (int)percentage;
                            info.Number = $"{info.Percentage}%";*//*
                        } while (bytes > 0);
                    }
                }
            });*/
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        private ObservableCollection<CopyProcessInfo> processes;
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Progress { get; set; }
        public bool IsWaiting => Progress == 0;
        public IEnumerable<CopyProcessInfo> Processes => processes;
        public ViewModel()
        {
            processes = new ObservableCollection<CopyProcessInfo>();
        }
        public void AddProcess(CopyProcessInfo info)
        {
            processes.Add(info);
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class CopyProcessInfo
    {
        public string FileName { get; set; }
        public double Percentage { get; set; }
        public int PercentageInt => (int)Percentage;
        public double BytesSecond { get; set; }
        public double MegaBytesSecond => Math.Round(BytesSecond / 1024 / 1024,1);
        public CopyProcessInfo(string fileName)
        {
            FileName = fileName;
        }
    }
}
