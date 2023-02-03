namespace SessionStorage;

using Domain;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MyAppDbContext : AbstractDbContext
{

    public MyAppDbContext(string connectionString) : base(connectionString)
    {

    }
}