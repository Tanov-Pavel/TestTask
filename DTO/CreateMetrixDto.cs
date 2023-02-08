namespace DTO
{
    public class CreateMetrixDto
    {
        public string IpAddress { get; set; }
        public int DiskSpace { get; set; }
        public double Cpu { get; set; }
        public int RamSpaceFree { get; set; }
        public int RamSpaceTotal { get; set; }

        public CreateMetrixDto(string ipAddress, int diskSpace, double cpu, int ramSpaceFree, int ramSpaceTotal)
        {
            this.IpAddress = ipAddress;
            this.DiskSpace = diskSpace;
            this.Cpu = cpu;
            this.RamSpaceFree = ramSpaceFree;
            this.RamSpaceTotal = ramSpaceTotal;
        }
    }
}
