﻿namespace SMS.Net.Channel.RavenSMS.Managers;

/// <summary>
/// the ravenSMs client manager implementation
/// </summary>
public partial class RavenSmsClientsManager
{
    /// <inheritdoc/>
    public Task<long> ClientsCountAsync()
        => _clientsStore.ClientsCountAsync();

    /// <inheritdoc/>
    public Task<bool> AnyClientAsync(string clientId)
        => _clientsStore.AnyAsync(clientId);

    /// <inheritdoc/>
    public Task<bool> AnyClientAsync(PhoneNumber phoneNumber)
        => _clientsStore.AnyAsync(phoneNumber);

    /// <inheritdoc/>
    public async Task<RavenSmsClient[]> GetAllConnectedClientsAsync()
    {
        var clients = await GetAllClientsAsync(new RavenSmsClientsFilter
        {
            IgnorePagination = true,
        });

        return clients.clients;
    }

    /// <inheritdoc/>
    public Task<RavenSmsClient[]> GetAllClientsAsync()
        => _clientsStore.GetAllAsync();

    /// <inheritdoc/>
    public Task<(RavenSmsClient[] clients, int rowsCount)> GetAllClientsAsync(RavenSmsClientsFilter filter)
        => _clientsStore.GetAllAsync(filter);

    /// <inheritdoc/>
    public Task<RavenSmsClient?> FindClientByIdAsync(string clientId)
        => _clientsStore.FindByIdAsync(clientId);

    /// <inheritdoc/>
    public Task<RavenSmsClient?> FindClientByPhoneNumberAsync(PhoneNumber phoneNumber)
        => _clientsStore.FindByPhoneNumberAsync(phoneNumber);

    /// <inheritdoc/>
    public async Task<Result<RavenSmsClient>> SaveClientAsync(RavenSmsClient model)
    {
        return await _clientsStore.IsExistClientAsync(model.Id)
            ? await _clientsStore.UpdateAsync(model)
            : await _clientsStore.SaveAsync(model);
    }
    
    /// <inheritdoc/>
    public Task<Result> DeleteClientAsync(RavenSmsClient client)
        => _clientsStore.DeleteClientAsync(client);

    /// <inheritdoc/>
    public async Task<Result> DeleteClientAsync(string clientId)
    {
        var client = await _clientsStore.FindByIdAsync(clientId).ConfigureAwait(false);
        if (client is null)
        {
            return Result.Failure()
                .WithMessage("there is no client with the given id")
                .WithCode(ResultCode.NotFound);
        }

        return await DeleteClientAsync(client);
    }

    /// <inheritdoc/>
    public async Task ClientDisconnectedAsync(string connectionId)
    {
        var client = await _clientsStore.FindByConnectionIdAsync(connectionId);
        if (client is null)
            return;

        client.ConnectionId = string.Empty;
        client.Status = RavenSmsClientStatus.Disconnected;
        await _clientsStore.UpdateAsync(client);
    }

    /// <inheritdoc/>
    public async Task<Result<RavenSmsClient>> ClientConnectedAsync(RavenSmsClient client, string connectionId)
    {
        // set the client id
        client.ConnectionId = connectionId;
        client.Status = RavenSmsClientStatus.Connected;

        // attach the connection id to the client in database
        return await _clientsStore.UpdateAsync(client);
    }
}

/// <summary>
/// partial part for <see cref="RavenSmsClientsManager"/>
/// </summary>
public partial class RavenSmsClientsManager : IRavenSmsClientsManager
{
    private readonly IRavenSmsClientsStore _clientsStore;

    public RavenSmsClientsManager(IRavenSmsClientsStore clientsStore) 
        => _clientsStore = clientsStore;
}