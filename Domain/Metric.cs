namespace Domain;

using DTO.EntityDto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

[Table(name: "metrics", Schema = "public")]
public class Metric : PersistentObject
{

    [Column("ip_address")]
    public string? IpAddress { get; set; }
    [Column("cpu")]
    public double Cpu { get; set; }
    [Column("ram_total")]
    public long TotalMemory { get; set; }
    [Column("ram_free")]
    public float FreeMemory { get; set; }
}
