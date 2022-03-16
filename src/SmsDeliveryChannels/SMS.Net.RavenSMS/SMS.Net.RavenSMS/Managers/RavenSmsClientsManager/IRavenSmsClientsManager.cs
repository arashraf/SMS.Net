﻿namespace SMS.Net.Channel.RavenSMS.Managers;

/// <summary>
/// the ravenSMS client manager
/// </summary>
public interface IRavenSmsClientsManager
{
    /// <summary>
    /// get the clients count
    /// </summary>
    /// <returns>total count of the clients</returns>
    Task<long> ClientsCountAsync();

    /// <summary>
    /// check if there is any client with the given phone number.
    /// </summary>
    /// <param name="phoneNumber">the phone number instance.</param>
    /// <returns>true if exist, false if not.</returns>
    /// <exception cref="ArgumentNullException">if the given phone number instance is null.</exception>
    Task<bool> AnyClientAsync(PhoneNumber phoneNumber);

    /// <summary>
    /// check if there is any client with the given id.
    /// </summary>
    /// <param name="clientId">the id of the client.</param>
    /// <returns>true if exist, false if not.</returns>
    Task<bool> AnyClientAsync(string clientId);

    /// <summary>
    /// create a new ravenSMS client if not exist, if exist the client will be updated.
    /// </summary>
    /// <param name="model">client model instance</param>
    /// <returns>the operation result</returns>
    Task<Result<RavenSmsClient>> SaveClientAsync(RavenSmsClient model);

    /// <summary>
    /// find the client with the given Id.
    /// </summary>
    /// <param name="clientId">the id of the client to find.</param>
    /// <returns>instance of <see cref="RavenSmsClient"/> found, full if not exist.</returns>
    Task<RavenSmsClient?> FindClientByIdAsync(string clientId);

    /// <summary>
    /// find the client with the given phone number.
    /// </summary>
    /// <param name="phoneNumber">the phone number associated with the client to find.</param>
    /// <returns>instance of <see cref="RavenSmsClient"/> found, full if not exist.</returns>
    Task<RavenSmsClient?> FindClientByPhoneNumberAsync(PhoneNumber phoneNumber);

    /// <summary>
    /// get the list of all connected clients.
    /// </summary>
    /// <returns>the list of clients.</returns>
    Task<RavenSmsClient[]> GetAllConnectedClientsAsync();

    /// <summary>
    /// get the list of all registered clients.
    /// </summary>
    /// <returns>the list of clients.</returns>
    Task<RavenSmsClient[]> GetAllClientsAsync();

    /// <summary>
    /// get the list of all clients
    /// </summary>
    /// <param name="filter">the filter used to retrieve the messages.</param>
    /// <returns>the list of messages and total count of rows</returns>
    Task<(RavenSmsClient[] clients, int rowsCount)> GetAllClientsAsync(RavenSmsClientsFilter filter);

    /// <summary>
    /// set the client as Connected, and associated the given connection id with it.
    /// </summary>
    /// <param name="client">the connected client</param>
    /// <param name="connectionId">the connection id</param>
    /// <returns>task</returns>
    Task<Result<RavenSmsClient>> ClientConnectedAsync(RavenSmsClient client, string connectionId);

    /// <summary>
    /// set the client associated with the given connectionId
    /// </summary>
    /// <param name="connectionId">the connection Id</param>
    /// <returns>task</returns>
    Task ClientDisconnectedAsync(string connectionId);

    /// <summary>
    /// delete the client with the given Id
    /// </summary>
    /// <param name="clientId">the id of the client</param>
    /// <returns>the operation result</returns>
    Task<Result> DeleteClientAsync(string clientId);
}