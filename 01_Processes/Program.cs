using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        //Process pr = new Process();
        #region Working with Current Process
        Process current = Process.GetCurrentProcess();

        // Process Priority:
        // * Idle
        // * BelowNormal
        // * Normal (def)
        // * AboveNormal
        // * High
        // * RealTime (only set by OS)
        // */
        /* current.PriorityClass = ProcessPriorityClass.High;

         //////////////////////// Process Info
         Console.WriteLine("----------- Current proccess info ------------");
         Console.WriteLine("Priority class: " + current.PriorityClass);
         Console.WriteLine("Name: " + current.ProcessName);
         Console.WriteLine("Id: " + current.Id);
         Console.WriteLine("MachineName: " + current.MachineName);
         Console.WriteLine("PrivateMemorySize (KB): " + current.PrivateMemorySize64 / 1024);
         Console.WriteLine("StartTime: " + current.StartTime);
         Console.WriteLine("TotalProcessorTime: " + current.TotalProcessorTime);
         Console.WriteLine("UserProcessorTime: " + current.UserProcessorTime);
         NextExample();*/
        #endregion

        #region All Processes

        /*Process[] processes = Process.GetProcesses();

        Console.WriteLine("Process Name\t\t\tPID\t\t\tPriority\tMachine name");
        Console.WriteLine("___________________________________________");
        foreach (var p in processes)
        {
            
            try
            {
                Console.WriteLine($"{p.ProcessName}\t{p.Id}\t{p.PriorityClass}\t{p.StartTime}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error with {p.ProcessName}");
                Console.ResetColor();
            }
        }
        NextExample();*/
        #endregion

        #region Start Process

        //Process.Start(@"file_path");
        //Process.Start("mspaint.exe");
        /* Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", "stackoverflow.com google.com");
         NextExample();*/

        /*ProcessStartInfo info = new ProcessStartInfo
        {
            FileName = @"notepad",
            Arguments = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\777.txt",
            WindowStyle = ProcessWindowStyle.Maximized
        };
        Process pr = Process.Start(info);

        Console.WriteLine("Press key to do operation...");
        //Console.ReadKey();

        //////////////////// Process Methods
        //pr.Close();               // clear resources
        //pr.Refresh();             // clear cashe
        //pr.CloseMainWindow();     // close process by normal mode = Alt+F4
        //edge.Kill();                // imediatelly stops a process = End Task
        Thread.Sleep(2000);
        pr.Kill();*/
        var pr = Process.Start("mspaint.exe");
        Console.WriteLine("Operation has done!");
        Console.WriteLine("Wait for exit...");
        Thread.Sleep(2000);
        pr.Kill();
        pr.WaitForExit(); // wait until proccess runing
        Console.WriteLine("Process was exited...");
        Console.WriteLine("ExitCode: " + pr.ExitCode);
        Console.WriteLine("ExitTime: " + pr.ExitTime);
        

        Console.ReadKey();
        #endregion
    }

    private static void NextExample()
    {
        Console.ReadKey();
        Console.Clear();
    }
}