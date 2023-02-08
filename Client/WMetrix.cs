using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Client.Metrix;
using System.Net;
using DTO;

namespace Client
{
    public class WMetrix : ImetricsRepository
    {
        public static PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public static PerformanceCounter ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");
        public void GetMetrix()
        {
            
            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            IPAddress[] addresses = hostEntry.AddressList;

            Console.WriteLine("IP Address: ");

            foreach (IPAddress address in addresses)
            {
                Console.WriteLine(address);
            }

            ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);

            foreach (ManagementObject item in searcher.Get())
            {
                var memoryKb = Convert.ToInt32(item["TotalVisibleMemorySize"].ToString());

                Console.WriteLine("Показатели RAM " + "Cвободно "
                    + ramCounterAvailable.NextValue() + " MB" + " | " + "Общий размер " + memoryKb / 1024 + " MB");
            }

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    Console.WriteLine("Состояния диска " + d.Name
                        + " " + "Свободно " + d.TotalFreeSpace / 1024 / 1024 / 1024
                        + " GB" + " | " + "Общий размер " + d.TotalSize / 1024 / 1024 / 1024 + " GB");
                }
            }
            
            Console.WriteLine("Загруженность CPU: " + cpuCounter.NextValue() + " %");
     
        }
        
    }
}