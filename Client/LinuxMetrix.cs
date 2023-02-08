using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Metrix;
using System.IO;
using System.Net;

namespace Client
{
    public class LinuxMetrix : ImetricsRepository
    {
        public void GetMetrix()
        {
            {
                CollectInfo();
            }
            static void CollectInfo()
            {
                // Показатели ОП - свободно/общий размер
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = "-c \"free -m\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                Console.WriteLine("Memory Info: \n" + result);

                // Загруженность ЦП - %
                process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = "-c \"top -bn1 | grep 'Cpu(s)'\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                result = process.StandardOutput.ReadToEnd();
                Console.WriteLine("CPU Load Info: \n" + result);

                // Состояние дисков - свободно/общий размер
                process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = "-c \"df -h\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                // IP Addresse
                string hostName = Dns.GetHostName();
                IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);
                Console.WriteLine("Host Name: " + hostName);
                Console.WriteLine("IP Addresses:");
                foreach (IPAddress ipAddress in ipAddresses)
                {
                    Console.WriteLine(ipAddress);
                }
                process.Start();
                result = process.StandardOutput.ReadToEnd();
                Console.WriteLine("Disk Info: \n" + result);
            }
        }
    }
}



