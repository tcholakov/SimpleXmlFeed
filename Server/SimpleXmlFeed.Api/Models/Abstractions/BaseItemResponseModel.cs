namespace SimpleXmlFeed.Api.Models.Abstractions
{
    using System;

    public abstract class BaseItemResponseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}