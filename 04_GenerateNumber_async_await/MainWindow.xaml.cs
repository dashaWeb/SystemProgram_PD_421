using System;
using System.Collections.Generic;
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

namespace _04_GenerateNumber_async_await
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Generate(object sender, RoutedEventArgs e)
        {

            /*int value = GenerateValue();
            list.Items.Add(value);*/
            /*Task<int> task = Task.Run(GenerateValue);
            list.Items.Add(task.Result); // Freeze
            task.Wait();// Freeze*/
            /*Task<int> task = Task.Run(GenerateValue);
            await task;
            MessageBox.Show("Complated");*/
            /*int val = task.Result;*/
            //list.Items.Add(val);
            //list.Items.Add(task.Result);
            //list.Items.Add(await Task.Run(GenerateValue));
            list.Items.Add(await GenerateValueAsync());
            
        }
        int GenerateValue()
        {
            Thread.Sleep(rnd.Next(5000));

            return rnd.Next(1000);
        }
        Task<int> GenerateValueAsync()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(rnd.Next(5000));

                return rnd.Next(1000);
            });
        }
    }

}
