using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using SquarePixel.Models.Entities;

namespace SquarePixel.Services;

public class RabbitMqService : IDisposable, IAsyncDisposable
{
    private IConnection _connection;
    private IChannel _channel;
    private DbService _dbService;
    public enum Queues
    {
        Images, GeneratedCaptions
    }
    public RabbitMqService(DbService dbService)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
        _dbService = dbService ?? throw new ArgumentNullException();

        Observable.StartAsync(async () =>
        {
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            
            await _channel.QueueDeclareAsync(Queues.Images.ToString(), durable: true, exclusive: false, autoDelete: false,
                arguments: null);
            
            await _channel.QueueDeclareAsync(Queues.GeneratedCaptions.ToString(), durable: true, exclusive: false, autoDelete: false,
                arguments: null);
        });
    }
    
    
    public async Task PublishImageAsync(IEnumerable<Photo> image, CancellationToken ct)
    {
        //check if image already has caption if so dont queue and return
        
    }



    public async Task ReadCaptionsAsync(CancellationToken ct)
    {
        //todo: whenever generations has data read it, find guid from db and update its caption

        await _dbService.SaveCaptionAsync(Guid.Empty, "", ct);
    }

    
    
    
    public void Dispose()
    {
        _connection.Dispose();
        _channel.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _channel.DisposeAsync();
    }
}