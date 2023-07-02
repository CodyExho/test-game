// See https://aka.ms/new-console-template for more information
using Game.Common.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost/newsHub")
                .Build();
//Make proxy to hub based on hub name on server
await connection.StartAsync().ContinueWith(async task =>
{
    if (task.IsFaulted)
    {
        Console.WriteLine("Failed to start: {0}", task.Exception.GetBaseException());
    }
    else
    {
        Console.WriteLine("Success! Connected with client connection id {0}", connection.ConnectionId);
        await connection.InvokeAsync("GetData", connection.ConnectionId);
        // Do more stuff here
    }
});

connection.On<string>("newsUpdate", param => {
    var res = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(param);
    foreach (var el in res)
    {
        Console.WriteLine(el.Name);
    }
});

Console.Read();