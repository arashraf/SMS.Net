﻿namespace SMS.Net.Channel.RavenSMS.Entities;

/// <summary>
/// a class that defines a client that is used for sending SMS messages.
/// </summary>
public class RavenSmsClient
{
    /// <summary>
    /// create and instance of <see cref="RavenSmsClient"/>
    /// </summary>
    public RavenSmsClient()
    {
        CreatedOn = DateTimeOffset.Now;
        PhoneNumbers = new HashSet<string>();
        Id = Generator.GenerateUniqueId("clt");
    }

    /// <summary>
    /// Get or set the id of the client.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Get or set the date
    /// </summary>
    public DateTimeOffset CreatedOn { get; set; }

    /// <summary>
    /// Get or set for the client.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Get or set a description for the client.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// the phone numbers associated with this client
    /// </summary>
    public IEnumerable<string> PhoneNumbers { get; set; } 
}
