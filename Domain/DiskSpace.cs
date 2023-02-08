using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table(name: "disk_space", Schema = "public")]
    public class DiskSpace : PersistentObject
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("total_disk_space")]
        public long TotalDiskSpace { get; set; }
        [Column("free_disk_space")]
        public long FreeDiskSpace { get; set; }
        [Column("metric_id")]
        public long MetricId { get; set; }
        [Column("metric")]
        public Metric Metric { get; set; }
    }
}
