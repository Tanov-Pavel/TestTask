namespace Repository;

using Domain;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public abstract class AbstractRepository<T> : IRepository<T> where T : PersistentObject
{
    private string _connectionString = "Server=localhost;Port=5432;Database=systemmetrixdb;UserId=postgres;Password=123456";

    abstract protected T Map(DbDataReader reader);
    protected string GetTableName()
    {
        return this.GetType()
            .GetInterfaces()
            .Where(intType => intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IRepository<>))
            .Select(t => t.GetGenericArguments()[0])
            .ToList()
            .Select(type =>
            {
                TableAttribute? tableAttribute = null;
                var attrs = type.GetCustomAttributes(true);
                if (attrs != null)
                {
                    var tableAttr = attrs.FirstOrDefault(attr => attr.GetType() == typeof(TableAttribute));
                    tableAttribute = tableAttr as TableAttribute;
                }
                return tableAttribute!.Name ?? throw new Exception();
            }).First();
    }
    public string MapEntity(T entity)
    {
        var properties = entity.GetType().GetProperties();
        string result = "";

        foreach (var property in properties)
        {
            if (property == null)
            {
                return null;
            }
            Type propertytype = property.GetType();

            //PropertyInfo propertyInfo = propertytype.GetProperty(property.Name);


            //if (propertyInfo == null)

            //{
            //    continue;
            //}


            var value = property.GetValue(entity);
            ;
            if (value != null && propertytype.Name == "update_date" )
                
            result = result + (value ?? "null");
            result = result + ",";
        }
        return result;
    }
    public virtual IQueryable<T> GetAll()
    {
        IList<T> metrics = new List<T>();
        var tableName = GetTableName();
        var sql = $"SELECT * FROM public.{tableName} ORDER BY id ASC";

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                DbCommand command = (DbCommand)connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                connection.Open();

                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    metrics.Add(Map(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception.Message: {0}", ex.Message);
            }
        }

        return metrics.AsQueryable();
    }
    public async Task<IQueryable<T>> GetAllAsync()
    {
        IList<T> metrics = new List<T>();
        var tableName = GetTableName();
        var sql = $"SELECT * FROM public.{tableName} ORDER BY id ASC";

        await using (var connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                DbCommand command = (DbCommand)connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                connection.Open();

                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    metrics.Add(Map(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception.Message: {0}", ex.Message);
            }
        }

        return metrics.AsQueryable();
    }
    public void Create(T item)
    {
        var tableName = GetTableName();
        string result = MapEntity(item);
        var sql = $"INSERT INTO public.{tableName} values ({result})";
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            DbCommand command = (DbCommand)connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            connection.Open();
            DbDataReader reader = command.ExecuteReader();
        }
    }



    public virtual IQueryable<T> Get()
    {
        IList<T> metrics = new List<T>();
        var tableName = GetTableName();

        var sql = $"SELECT * FROM public.{tableName} ORDER BY id ASC";

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                DbCommand command = (DbCommand)connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                connection.Open();

                DbDataReader reader = command.ExecuteReader();
                reader.Read();

                metrics.Add(Map(reader));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception.Message: {0}", ex.Message);
            }
        }

        return (IQueryable<T>)metrics.FirstOrDefault();
    }
    public async Task<IQueryable<T>> GetAsync()
    {
        IList<T> metrics = new List<T>();
        var tableName = GetTableName();
        var sql = $"SELECT * FROM public.{tableName} ORDER BY id ASC";

        await using (var connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                DbCommand command = (DbCommand)connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                connection.Open();

                DbDataReader reader = command.ExecuteReader();
                reader.Read();
                metrics.Add(Map(reader));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception.Message: {0}", ex.Message);
            }
        }

        return (IQueryable<T>)metrics.FirstOrDefault();
    }
    public Task CreateAsync(T item)
    {
        var tableName = GetTableName();
        var sql = $"CREATE * FROM public.{tableName} ORDER BY id ASC";
        throw new NotImplementedException();
    }
    public void Delete(T entity)
    {
        var tableName = GetTableName();
        string result = MapEntity(entity);
        var sql = $"DELETE From public.{tableName} WHERE ({result})";
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            DbCommand command = (DbCommand)connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            connection.Open();
            DbDataReader reader = command.ExecuteReader();
        }
    }
    public void Remove(T entity)
    {
        throw new NotImplementedException();
    }
    public Task RemoveRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }
    public int SaveChanges()
    {
        throw new NotImplementedException();
    }
    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
    public Task UpdateRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }
    public Task AddRange(IEnumerable<T> newEntities)
    {
        throw new NotImplementedException();
    }
}
