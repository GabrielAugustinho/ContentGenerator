﻿namespace ContentGenerator.Api.Core.InputPort.PublicationPort
{
    public class PublicationInput
    {
        public required int AssuntoId { get; set; }
        public List<int> WhatsAppNumbers { get; set; }
        public List<string> Emails { get; set; }
    }
}
