using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Metrix
{
    public interface IMetricsService
    {
        CreateMetricsDto GetMetrics();
    }
}
