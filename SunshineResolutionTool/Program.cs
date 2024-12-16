using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SunshineResolutionTool
{
    internal class Program
    {
        static int defaultWidth, streamWidth = 1920;
        static int defaultHeight, streamHeight = 1080;
        static int defaultRefreshRate, streamRefreshRate = 60;

        static bool defaultHDR, streamHDR = false;

        static string logFilePath = @"C:\\Program Files\\Sunshine\\config\\sunshine.log";

        static bool connected = false;

        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "-hide")
            {
                Process proc = new Process();

                proc.StartInfo.FileName = Process.GetCurrentProcess().ProcessName;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                proc.Start();
                Environment.Exit(1);
            }

            if (!File.Exists("settings.cfg"))
            {
                Console.WriteLine("Settings file (settings.cfg) does not exist. Creating settings file.");

                File.WriteAllText("settings.cfg",
                    "#This is the display mode that will be restore when game streaming ends\r\ndefaultWidth=5120\r\ndefaultHeight=1440\r\ndefaultRefreshRate=240\r\ndefaultHDR=1\r\n\r\n#This is the display mode that will be used for game streaming\r\nstreamWidth=1920\r\nstreamHeight=1080\r\nstreamRefreshRate=60\r\nstreamHDR=0\r\n\r\n#This is the location of the sunshine.log file\r\nlogFilePath=C:\\Program Files\\Sunshine\\config\\sunshine.log");

                Console.WriteLine("Please modify the settings file (settings.cfg) and try again.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }

            foreach (string line in File.ReadAllLines("settings.cfg"))
            {
                if (line.IndexOf("=") == -1)
                    continue;

                string key = line.Split('=')[0];
                string value = line.Split('=')[1];
                switch (key)
                {
                    case "defaultWidth":
                        defaultWidth = Convert.ToInt32(value);
                        break;
                    case "defaultHeight":
                        defaultHeight = Convert.ToInt32(value);
                        break;
                    case "streamWidth":
                        streamWidth = Convert.ToInt32(value);
                        break;
                    case "streamHeight":
                        streamHeight = Convert.ToInt32(value);
                        break;
                    case "defaultRefreshRate":
                        defaultRefreshRate = Convert.ToInt32(value);
                        break;
                    case "streamRefreshRate":
                        streamRefreshRate = Convert.ToInt32(value);
                        break;
                    case "defaultHDR":
                        defaultHDR = value == "1";
                        break;
                    case "streamHDR":
                        streamHDR = value == "1";
                        break;
                    case "logFilePath":
                        logFilePath = value;
                        break;
                }
            }

            Console.WriteLine("Settings loaded.\n\nDefault display mode:\n{0}x{1}@{2}" + (defaultHDR ? " (HDR)" : "") + "\n\nStream display mode:\n{3}x{4}@{5}" + (streamHDR ? " (HDR)" : ""),
                defaultWidth, defaultHeight, defaultRefreshRate, streamWidth, streamHeight, streamRefreshRate);

            connected = processConnection();

            Console.WriteLine("Waiting for client connection to change.");

            DateTime lastModified = File.GetLastWriteTime(logFilePath);
            while (true)
            {
                DateTime currentModified = File.GetLastWriteTime(logFilePath);

                if (currentModified > lastModified)
                {
                    Console.WriteLine("Log updated. Reading new log.");
                    lastModified = currentModified;
                    processConnection();
                }

                Thread.Sleep(1000);
            }
        }

        static bool processConnection()
        {
            bool connectedNow = isSunshineConnected();
            if (connectedNow && !connected)
            {
                connected = true;
                Console.WriteLine("Client connected. Setting stream resolution...");
                setDisplayMode(streamWidth, streamHeight, streamRefreshRate, streamHDR);
            }
            else
            {
                if (!connectedNow && connected)
                {
                    connected = false;
                    Console.WriteLine("Client disconnected. Setting default resolution...");
                    setDisplayMode(defaultWidth, defaultHeight, defaultRefreshRate, defaultHDR);
                }
            }
            return connectedNow;
        }

        static bool isSunshineConnected()
        {
            List<string> log = new List<string>();
            bool connected = false;

            try
            {
                using (var fileStream = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);

                    using (var reader = new StreamReader(fileStream))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains("CLIENT CONNECTED"))
                                connected = true;
                            if (line.Contains("CLIENT DISCONNECTED"))
                                connected = false;
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return connected;
        }

        static void setDisplayMode(int width, int height, int refreshRate, bool HDR)
        {
            Process myProc = new Process();
            myProc.StartInfo.FileName = "powershell";
            myProc.StartInfo.Arguments = "-ExecutionPolicy unrestricted -Command Import-Module WindowsDisplayManager; $primaryDisplay = WindowsDisplayManager\\GetPrimaryDisplay; $primaryDisplay.SetResolution(" + width + "," + height + "," + refreshRate + "); $primaryDisplay." + (HDR ? "En" : "Dis") + "ableHdr();";
            myProc.StartInfo.CreateNoWindow = true;
            myProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProc.Start();
        }
    }
}
