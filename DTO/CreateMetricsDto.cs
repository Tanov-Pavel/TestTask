using DTO.EntityDto;

namespace DTO
{
    public class CreateMetricsDto
    {
        public IPAddressDto IpAddress { get; set; }
        public List<DiskSpaceDto> DiskSpaces { get; set; }
        public MemoryDto Memory { get; set; }
        public CPUDto CPU { get; set; }

        public CreateMetricsDto(IPAddressDto ipAddress, List<DiskSpaceDto> diskSpaces, MemoryDto memory, CPUDto cpu)
        {
            IpAddress = ipAddress;
            DiskSpaces = diskSpaces;
            Memory = memory;
            CPU = cpu;
        }
    }
}
