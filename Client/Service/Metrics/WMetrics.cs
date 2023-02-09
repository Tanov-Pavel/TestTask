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
using DTO.EntityDto;
using Client.Interfaces;

namespace Client.Service.Metrics
{
    public class WMetrics : IMetricsService
    {
        private readonly IDiskSpaceService DiskSpaceService;

        public static PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public static PerformanceCounter ramCounterAvailable = new PerformanceCounter("Memory", "Available MBytes");

        public WMetrics(IDiskSpaceService diskSpaceService)
        {
            DiskSpaceService = diskSpaceService;
        }

        public CreateMetricsDto GetMetrics()
        {
            CreateMetricsDto metricsDto = new CreateMetricsDto
            (
                GetIPAddressWindows(),
                GetDiskSpaceWindows(),
                GetMemoryWindows(),
                GetProcessorTimeWindows()
            );

            return metricsDto;
        }

        private MemoryDto GetMemoryWindows()
        {
            ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);

            MemoryDto result = new MemoryDto();

            foreach (ManagementObject item in searcher.Get())
            {
                var memoryKb = Convert.ToInt32(item["TotalVisibleMemorySize"].ToString());

                result.TotalMemory = memoryKb / 1024;
                result.FreeMemory = ramCounterAvailable.NextValue();
            }
            return result;
        }
        private IPAddressDto GetIPAddressWindows()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            IPAddress[] addresses = hostEntry.AddressList;

            IPAddressDto result = new IPAddressDto();
            result.iPAddress = addresses[1].ToString();
            return result;
        }

        private CPUDto GetProcessorTimeWindows()
        {
            CPUDto result = new CPUDto();
            result.Usage = cpuCounter.NextValue();
            return result;
        }

        private List<DiskSpaceDto> GetDiskSpaceWindows()
        {
            return DiskSpaceService.CollectWindows();
        }
    }
}