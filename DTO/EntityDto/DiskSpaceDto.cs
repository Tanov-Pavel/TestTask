using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.EntityDto
{
    public class DiskSpaceDto
    {
        public string Name { get; set; }
        public long FreeSpace { get; set; }
        public long TotalSpace { get; set; }

        public DiskSpaceDto()
        {
        }

        public DiskSpaceDto(string name, long freeSpace, long totalSpace)
        {
            Name = name;
            FreeSpace = freeSpace;
            TotalSpace = totalSpace;
        }
    }
    
}
