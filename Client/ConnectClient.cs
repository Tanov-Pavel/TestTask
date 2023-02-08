﻿using Client;
using Client.Metrix;
using DTO;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualBasic;
using System;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using ObjectQuery = System.Management.ObjectQuery;

WMetrix wMetrix = new WMetrix();

LinuxMetrix linuxMetrix = new LinuxMetrix();

int period = 10000;



var connection = new HubConnectionBuilder()

       .WithUrl(new Uri("http://127.0.0.1:8088/chat"))
       .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
       .Build();

connection.On<string, string>("ReceiveMessage", (user, message) =>
{
Console.WriteLine(user + message);
});

try

{
    await connection.StartAsync();
}

catch (Exception ex)

{
    Console.WriteLine(ex.ToString());
}

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    while (true)
    {
        wMetrix.GetMetrix();
        Console.WriteLine();
        Thread.Sleep(period);
        Console.Clear();
    }
}

else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    while (true)
    {
        linuxMetrix.GetMetrix();
        Console.WriteLine();
        Thread.Sleep(period);
        Console.Clear();
    }
}