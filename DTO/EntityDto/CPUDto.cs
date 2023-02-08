using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.EntityDto
{
    public class CPUDto
    {
        public double Usage { get; set; }

        public CPUDto()
        {
        }

        public CPUDto(double usage)
        {
            Usage = usage;
        }
    }
}
