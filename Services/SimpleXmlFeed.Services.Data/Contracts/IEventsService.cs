namespace SimpleXmlFeed.Services.Data.Contracts
{
    using System;

    using Models.Data;

    public interface IEventsService : ITransactable
    {
        Event Add(string name, int feedId, Sport sport, bool isLive, int categoryId, bool saveChanges = true);

        Guid GetEventIdByFeedId(int feedId);

        Event GetEventByFeedId(int feedId);

        Event UpdateEvent(string name, int feedId, bool isLive, int categoryId, bool saveChanges = true);
    }
}
