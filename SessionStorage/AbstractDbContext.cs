namespace SessionStorage;

using Domain;
using Microsoft.Extensions.Logging;
using Npgsql;
using Repository;
using Repository.Plugable;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public abstract class AbstractDbContext : IDbContext, IDisposable
{
    private readonly ILogger logger = LoggerFactory.Create(b => b.SetMinimumLevel(LogLevel.Debug))
        .CreateLogger<AbstractDbContext>();

    private readonly ConcurrentDictionary<string, IDbConnection> _connections = 
        new ConcurrentDictionary<string, IDbConnection>();
    private readonly string _connectionString;

    public AbstractDbContext(string connectionString)
    {
        this._connectionString = connectionString;
        logger.LogDebug("Connection string is: ", connectionString);
    }

    public IDbConnection CreateConnection(string tableName)
    {
        if (string.IsNullOrEmpty(tableName))
            throw new ArgumentException("table name is null or empty");
        var conn = new NpgsqlConnection(_connectionString);
        var connection = _connections.GetOrAdd(tableName, conn);
        return conn;
    }

    public IDbConnection GetConnection(string tableName)
    {
        if (!_connections.ContainsKey(tableName))
            throw new InvalidOperationException("Create connection before Gets it");
        return _connections[tableName];
    }

    public void Dispose()
    {
        if (_connections.IsEmpty)
            return;
        _connections.ToImmutableList().ForEach(pair =>
        {
            var connection = pair.Value;
            if (connection != null)
                connection.Close();
        });
        _connections.Clear();
    }

    public void RegisterRepository(object repository)
    {
        repository.GetType()
            .GetInterfaces()
            .Where(intType => intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IRepository<>))
            .Select(t => t.GetGenericArguments()[0])
            .ToList()
            .ForEach(type =>
            {
                var attrs = type.GetCustomAttributes(true);
                if (attrs != null)
                {
                    var tableAttr = attrs.FirstOrDefault(attr => attr.GetType() == typeof(TableAttribute));
                    TableAttribute? tableAttribute = tableAttr as TableAttribute;
                    if (tableAttribute != null)
                    {
                        var tableName = tableAttribute.Name;
                        var schema = tableAttribute.Schema;
                        var connection = this.CreateConnection(tableName);
                        PlugableConnection? plugableConnection = repository as PlugableConnection;
                        if (plugableConnection != null && connection != null)
                        {
                            plugableConnection.Plug(connection);
                        }
                    }
                }
            });
    }

    public void RemoveRepository(object repository)
    {
        throw new NotImplementedException();
    }
}
