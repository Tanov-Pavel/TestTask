namespace SessionStorage;

using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public interface IDbContext
{
    IDbConnection CreateConnection(string tableName);
    IDbConnection GetConnection(string tableName);
    void RegisterRepository(object repository);
    void RemoveRepository(object repository);
}