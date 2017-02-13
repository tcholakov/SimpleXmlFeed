namespace SimpleXmlFeed.Services.Xml.Contracts
{
    using System.Collections.Generic;

    using Models.Xml;
    using Models.Data;

    public interface IXmlImporterService
    {
        void Import(IEnumerable<XmlSportModel> xmlSports);

        Sport ImportSport(XmlSportModel xmlSport, bool saveChanges = true);

        Event ImportEvent(XmlEventModel xmlEvent, Sport sport, bool saveChanges = true);

        Match ImportMatch(XmlMatchModel xmlMatch, Event evnt, bool saveChanges = true);

        Bet ImportBet(XmlBetModel xmlBet, Match match, bool saveChanges = true);

        Odd ImportOdd(XmlOddModel xmlOdd, Bet bet, bool saveChanges = true);
    }
}
