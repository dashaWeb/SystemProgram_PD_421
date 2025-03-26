using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _03_thread_example_app;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Thread thread = null;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        //HardWork();
        thread = new Thread(HardWork);
        thread.Start();
    }
    private void HardWork()
    {
        bool flag = false;
        Application.Current.Dispatcher.Invoke(() =>
        {
            if (progress.Value > 0)
            {
                progress.Value = progress.Minimum;
            }
            flag = progress.Value < progress.Maximum;
        });


        while (flag)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                progress.Value++;
                flag = progress.Value < progress.Maximum;
            });
            Thread.Sleep(100);
        }
    }
}