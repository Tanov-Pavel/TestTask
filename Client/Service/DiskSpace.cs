using Client.Interfaces;
using DTO.EntityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Service
{
    public class DiskSpace : IDiskSpace
    {
        public List<DiskSpaceDto> CollectLinux()
        {
            var diskSpaces = DriveInfo.GetDrives();
            List<DiskSpaceDto> result = new List<DiskSpaceDto>();
            foreach (var disk in diskSpaces)
            {
                result.Add(new DiskSpaceDto(disk.Name, disk.TotalFreeSpace, disk.TotalSize));
            }

            return result;
        }

        public List<DiskSpaceDto> CollectWindows()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            List<DiskSpaceDto> result = new List<DiskSpaceDto>();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    result.Add(new DiskSpaceDto(d.Name, d.TotalFreeSpace / 1024 / 1024 / 1024, d.TotalSize / 1024 / 1024 / 1024));
                }
            }
            return result;
        }
    }
}
