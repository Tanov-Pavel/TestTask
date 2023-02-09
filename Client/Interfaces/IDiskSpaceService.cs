using DTO.EntityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface IDiskSpaceService
    {
        List<DiskSpaceDto> CollectWindows();
        List<DiskSpaceDto> CollectLinux();
    }
}
