namespace SimpleXmlFeed.Services.Data.Contracts
{
    using System;

    using Models.Data;
    
    public interface IBetsService : ITransactable
    {
        Bet Add(string name, int feedId, Match match, bool isLive, bool saveChanges = true);
        
        Guid GetBetIdByFeedId(int feedId);

        Bet GetBetByFeedId(int feedId);

        Bet UpdateBet(string name, int feedId, bool isLive, bool saveChanges = true);
    }
}
