namespace SimpleXmlFeed.Services.Data.Contracts
{
    using System;

    using Models.Data;
    
    public interface IOddsService : ITransactable
    {
        Odd Add(string name, int feedId, Bet bet, decimal value, string specialValue, bool saveChanges = true);

        Guid GetOddIdByFeedId(int feedId);

        Odd GetOddByFeedId(int feedId);

        Odd UpdateOdd(string name, int feedId, decimal value, string specialValue, bool saveChanges = true);
    }
}
