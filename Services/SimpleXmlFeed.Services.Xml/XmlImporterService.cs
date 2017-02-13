namespace SimpleXmlFeed.Services.Xml
{
    using System.Collections.Generic;

    using Models.Xml;
    using Data.Contracts;
    using SimpleXmlFeed.Services.Xml.Contracts;
    using Models.Data;

    public class XmlImporterService : IXmlImporterService
    {
        private readonly ISportsService sportsService;
        private readonly IEventsService eventsService;
        private readonly IMatchesService matchesService;
        private readonly IBetsService betsService;
        private readonly IOddsService oddsService;

        public XmlImporterService(ISportsService sportsService, IEventsService eventsService, IMatchesService matchesService, IBetsService betsService, IOddsService oddsService)
        {
            this.sportsService = sportsService;
            this.eventsService = eventsService;
            this.matchesService = matchesService;
            this.betsService = betsService;
            this.oddsService = oddsService;
        }


        public Sport ImportSport(XmlSportModel xmlSport, bool saveChanges = true)
        {
            Sport dbSport = this.sportsService.GetSportByFeedId(xmlSport.FeedId);

            if (dbSport != null)
            {
                this.sportsService.UpdateSport(xmlSport.Name, xmlSport.FeedId);
            }
            else
            {
                dbSport = this.sportsService.Add(xmlSport.Name, xmlSport.FeedId, saveChanges);
            }

            return dbSport;
        }

        public Event ImportEvent(XmlEventModel xmlEvent, Sport sport, bool saveChanges = true)
        {
            Event dbEvent = this.eventsService.GetEventByFeedId(xmlEvent.FeedId);

            if (dbEvent != null)
            {
                this.eventsService.UpdateEvent(xmlEvent.Name, xmlEvent.FeedId, xmlEvent.IsLive, xmlEvent.CategoryId);
            }
            else
            {
                dbEvent = this.eventsService.Add(xmlEvent.Name, xmlEvent.FeedId, sport, xmlEvent.IsLive, xmlEvent.CategoryId, saveChanges);
            }

            return dbEvent;
        }

        public Match ImportMatch(XmlMatchModel xmlMatch, Event evnt, bool saveChanges = true)
        {
            Match dbMatch = this.matchesService.GetMatchByFeedId(xmlMatch.FeedId);

            if (dbMatch != null)
            {
                this.matchesService.UpdateMatch(xmlMatch.Name, xmlMatch.FeedId, xmlMatch.StartDate, xmlMatch.MatchType);
            }
            else
            {
                dbMatch = this.matchesService.Add(xmlMatch.Name, xmlMatch.FeedId, evnt, xmlMatch.StartDate, xmlMatch.MatchType, saveChanges);
            }

            return dbMatch;
        }

        public Bet ImportBet(XmlBetModel xmlBet, Match match, bool saveChanges = true)
        {
            Bet dbBet = this.betsService.GetBetByFeedId(xmlBet.FeedId);

            if (dbBet != null)
            {
                this.betsService.UpdateBet(xmlBet.Name, xmlBet.FeedId, xmlBet.IsLive, saveChanges: true);
            }
            else
            {
                dbBet = this.betsService.Add(xmlBet.Name, xmlBet.FeedId, match, xmlBet.IsLive, saveChanges);
            }

            return dbBet;
        }

        public Odd ImportOdd(XmlOddModel xmlOdd, Bet bet, bool saveChanges = true)
        {
            Odd dbOdd = this.oddsService.GetOddByFeedId(xmlOdd.FeedId);

            if (dbOdd != null)
            {
                this.oddsService.UpdateOdd(xmlOdd.Name, xmlOdd.FeedId, xmlOdd.Value, xmlOdd.SpecialBetValue, saveChanges: true);
            }
            else
            {
                dbOdd = this.oddsService.Add(xmlOdd.Name, xmlOdd.FeedId, bet, xmlOdd.Value, xmlOdd.SpecialBetValue, saveChanges: false);
            }

            return dbOdd;
        }

        public void Import(IEnumerable<XmlSportModel> xmlSports)
        {
            foreach (var xmlSport in xmlSports)
            {
                Sport currentSport = this.ImportSport(xmlSport, saveChanges: false);

                foreach (var xmlEvent in xmlSport.Events)
                {
                    Event currentEvent = this.ImportEvent(xmlEvent, currentSport, saveChanges: false);

                    foreach (var xmlMatch in xmlEvent.Matches)
                    {
                        Match currentMatch = this.ImportMatch(xmlMatch, currentEvent, saveChanges: true);

                        foreach (var xmlBet in xmlMatch.Bets)
                        {
                            Bet currentBet = this.ImportBet(xmlBet, currentMatch, saveChanges: false);

                            foreach (var xmlOdd in xmlBet.Odds)
                            {
                               this.ImportOdd(xmlOdd, currentBet, saveChanges: false);
                            }
                        }
                    }

                    this.sportsService.SaveChanges();
                }
            }
        }
    }
}
