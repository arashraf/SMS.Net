﻿namespace SMS.Net.Channel.RavenSMS;

public class RavenSmsHub : Hub
{
    private readonly ILogger _logger;
    private readonly IRavenSmsClientsManager _clientsManager;

    public RavenSmsHub(IRavenSmsClientsManager manager, ILogger<RavenSmsHub> logger)
    {
        _logger = logger;
        _clientsManager = manager;
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // disconnecting the client
        _logger.LogInformation("client associated with connection Id: {connectionId} has been disconnected", Context.ConnectionId);
        await _clientsManager.ClientDisconnectedAsync(Context.ConnectionId);
    }

    public async Task ClientConnectedAsync(string clientId, bool forceConnection)
    {
        // get the client associated with the given id
        var client = await _clientsManager.FindClientByIdAsync(clientId);
        if (client is null)
        {
            await Clients.Caller.SendAsync("forceDisconnect", "client_not_found");
            return;
        }

        // check if the client already connected
        if (client.Status == RavenSmsClientStatus.Connected)
        {
            // check if the client is already connected
            if (client.ConnectionId == Context.ConnectionId)
                return;

            // new connection, check if we need to force the connection, or not
            if (!forceConnection)
            {
                await Clients.Caller.SendAsync("forceDisconnect", "client_already_connected");
                return;
            }
        }

        // attach the client to the current connection
        _logger.LogInformation("connecting client with Id: {clientId}, connection Id: {connectionId}", client.Id, Context.ConnectionId);
        await _clientsManager.ClientConnectedAsync(client, Context.ConnectionId);
    }
}

public static class RavenSmsHubExtensions
{
    public static async Task<Result> SendSmsMessageAsync(this IHubContext<RavenSmsHub> hub, RavenSmsClient client, RavenSmsMessage message)
    {
        if (client.ConnectionId is null)
            throw new ArgumentNullException($"{nameof(client)}.{nameof(client.ConnectionId)}");

        try
        {
            await hub.Clients.Client(client.ConnectionId).SendAsync("sendSmsMessage", new
            {
                from = message.From.ToString(),
                to = message.To.ToString(),
                content = message.Body,
                date = message.SentOn,
                id = message.Id,
            });

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure()
                .WithErrors(ex);
        }
    }
}