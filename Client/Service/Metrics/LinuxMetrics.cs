using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Metrix;
using System.IO;
using System.Net;
using DTO;
using DTO.EntityDto;
using Client.Interfaces;

namespace Client.Service.Metrics
{
    public class LinuxMetrics : IMetricsService
    {
        private readonly IDiskSpaceService DiskSpace;

        public LinuxMetrics(IDiskSpaceService diskSpace)
        {
            DiskSpace = diskSpace;
        }

        public CreateMetricsDto GetMetrics()
        {
            CreateMetricsDto metricsDto = new CreateMetricsDto
            (
                GetIPAddressLinux(),
                GetDiskSpaceLinux(),
                GetMemoryLinux(),
                GetProcessorTimeLinux()
            );

            return metricsDto;
        }
        private MemoryDto GetMemoryLinux()
        {
            long totalMemory = 0;
            try
            {
                var lines = File.ReadAllLines("/proc/meminfo");
                foreach (var line in lines)
                {
                    if (line.StartsWith("MemTotal"))
                    {
                        var values = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        totalMemory = Convert.ToInt64(values[values.Length - 2]) / 1024;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            long usageMemory = 0;
            Process.GetProcesses().ToList().ForEach(p =>
            {
                usageMemory += p.WorkingSet64 / 1024 / 1024;
            });

            MemoryDto result = new MemoryDto(totalMemory, totalMemory - usageMemory);
            return result;
        }
        private IPAddressDto GetIPAddressLinux()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            IPAddress[] addresses = hostEntry.AddressList;

            IPAddressDto result = new IPAddressDto();
            result.iPAddress = addresses[1].ToString();
            return result;
        }

        private CPUDto GetProcessorTimeLinux()
        {
            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;

            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);

            CPUDto result = new CPUDto(cpuUsageTotal * 100);
            return result;
        }

        private List<DiskSpaceDto> GetDiskSpaceLinux()
        {
            return DiskSpace.CollectLinux();

        }
    }
}



