﻿namespace SMS.Net.Channel.RavenSMS.Models;

/// <summary>
/// a class that defines the RavenSMS Message
/// </summary>
public class RavenSmsMessage
{
    /// <summary>
    /// create an instance of <see cref="RavenSmsMessage"/>
    /// </summary>
    public RavenSmsMessage()
    {
        Id = Guid.NewGuid();
        CreateOn = DateTimeOffset.Now;
    }

    /// <summary>
    /// Get or set the id of the message.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Get or set the date this message was create on.
    /// </summary>
    public DateTimeOffset CreateOn { get; set; }

    /// <summary>
    /// Gets or sets the priority of this e-mail message.
    /// </summary>
    public Priority Priority { get; set; }

    /// <summary>
    /// Get or set the message body.
    /// </summary>
    public string Body { get; set; } = default!;

    /// <summary>
    /// Get or set the phone numbers of recipients to send the SMS message to.
    /// </summary>
    public PhoneNumber To { get; set; } = default!;

    /// <summary>
    /// Get or set the phone number used to send the SMS message from it.
    /// </summary>
    public PhoneNumber From { get; set; } = default!;
}
