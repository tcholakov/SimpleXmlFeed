namespace SimpleXmlFeed.Services.Data.Contracts
{
    using System;

    using Models.Data;

    public interface ISportsService : ITransactable
    {
        Sport Add(string name, int feedId, bool saveChanges = true);

        Guid GetSportIdByFeedId(int feedId);

        Sport GetSportByFeedId(int feedId);

        Sport UpdateSport(string name, int feedId, bool saveChanges = true);
    }
}
