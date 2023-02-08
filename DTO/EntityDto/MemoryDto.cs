using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.EntityDto
{
    public class MemoryDto
    {
        public long TotalMemory { get; set; }
        public float FreeMemory { get; set; }

        public MemoryDto()
        {
        }

        public MemoryDto(long totalMemory, float freeMemory)
        {
            TotalMemory = totalMemory;
            FreeMemory = freeMemory;
        }
    }
}
