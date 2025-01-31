﻿namespace SMS.Net.Channel.Twilio
{
    using SMS.Net.Exceptions;

    /// <summary>
    /// the options for configuring the Twilio SMS delivery channel
    /// </summary>
    public class TwilioSmsDeliveryChannelOptions
    {
        /// <summary>
        /// Get or Set your Twilio user name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Get or Set your Twilio password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Get or Set your Twilio password.
        /// </summary>
        public string AccountSID { get; set; }

        /// <summary>
        /// validate if the options are all set correctly
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Username))
                throw new RequiredOptionValueNotSpecifiedException<TwilioSmsDeliveryChannelOptions>(
                    $"{nameof(Username)}", "the given TwilioSmsDeliveryChannelOptions.ApiKey value is null or empty.");

            if (string.IsNullOrWhiteSpace(Password))
                throw new RequiredOptionValueNotSpecifiedException<TwilioSmsDeliveryChannelOptions>(
                    $"{nameof(Password)}", "the given TwilioSmsDeliveryChannelOptions.ApiKey value is null or empty.");
        }
    }
}
