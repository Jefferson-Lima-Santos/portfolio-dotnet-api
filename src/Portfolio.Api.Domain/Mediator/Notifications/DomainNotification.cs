﻿using Newtonsoft.Json;
using Portfolio.Api.Domain.Mediator.Messages;

namespace Portfolio.Api.Domain.Mediator.Notifications
{
    public class DomainNotification : Event
    {
        [JsonProperty("domainNotificationId")]
        public Guid DomainNotificationId { get; private set; }
        [JsonProperty("key")]
        public string Key { get; private set; }
        [JsonProperty("value")]
        public string Value { get; private set; }
        [JsonProperty("version")]
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
            Version = 1;
        }
    }
}
