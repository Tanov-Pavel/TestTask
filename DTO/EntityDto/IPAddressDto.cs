using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DTO.EntityDto
{
    public class IPAddressDto
    {
        public string iPAddress { get; set; }

        public IPAddressDto()
        {
        }

        public IPAddressDto(string iP)
        {
            iPAddress = iP;
        }
    }
}
