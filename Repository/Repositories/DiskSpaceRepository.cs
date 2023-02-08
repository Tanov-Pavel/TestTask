using Domain;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DiskSpaceRepository : AbstractRepository<DiskSpace>, IDiskSpaceRepository
    {
        protected override DiskSpace Map(DbDataReader reader)
        {
            var result = new DiskSpace
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                TotalDiskSpace = reader.GetInt64(2),
                FreeDiskSpace = reader.GetInt64(3),
                IsDeleted = reader.GetBoolean(4),
                CreateDate = reader.GetDateTime(5),
                UpdateDate = !reader.IsDBNull(6) ? reader.GetDateTime(6) : null,
                DeleteDate = !reader.IsDBNull(7) ? reader.GetDateTime(7) : null

            };
            return result;
        }
    }
}
