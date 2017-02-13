namespace SimpleXmlFeed.Services.Xml
{
    using System;
    using System.Xml;
    using System.Collections.Generic;

    using Contracts;
    using Models.Xml;

    public class XmlConverterService : IXmlConverterService
    {
        public XmlSportModel ConvertToXmlSportModel(XmlNode sportNode)
        {
            XmlSportModel xmlSportModel = new XmlSportModel();

            if (sportNode.Attributes["Name"] != null && sportNode.Attributes["ID"] != null)
            {
                xmlSportModel = this.ConvertSportNodeToXmlSportModel(sportNode);

                if (sportNode.HasChildNodes)
                {
                    foreach (XmlNode eventNode in sportNode.ChildNodes)
                    {
                        XmlEventModel currentXmlEventModel = this.ConvertEventNodeToXmlEventModel(eventNode);

                        xmlSportModel.Events.Add(currentXmlEventModel);

                        if (eventNode.HasChildNodes)
                        {
                            foreach (XmlNode matchNode in eventNode.ChildNodes)
                            {
                                XmlMatchModel currentXmlMatchModel = this.ConvertMatchNodeToXmlMatchModel(matchNode);

                                currentXmlEventModel.Matches.Add(currentXmlMatchModel);

                                if (matchNode.HasChildNodes)
                                {
                                    foreach (XmlNode betNode in matchNode.ChildNodes)
                                    {
                                        XmlBetModel currentXmlBetModel = this.ConverBetNodeToXmlBetModel(betNode);

                                        currentXmlMatchModel.Bets.Add(currentXmlBetModel);

                                        if (betNode.HasChildNodes)
                                        {
                                            foreach (XmlNode oddNode in betNode.ChildNodes)
                                            {
                                                XmlOddModel currentXmlOddModel = this.ConvertOddNodeToXmlOddMOdel(oddNode);

                                                currentXmlBetModel.Odds.Add(currentXmlOddModel);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return xmlSportModel;
        }

        public IEnumerable<XmlSportModel> ConvertXmlDocumentToCollectionOfXmlSportModel(XmlDocument document)
        {
            XmlNodeList sportsNodeList = document.SelectNodes("//Sport");

            XmlSportModel xmlSportModel = new XmlSportModel();

            List<XmlSportModel> xmlSportModels = new List<XmlSportModel>();

            foreach (XmlNode currentSportNode in sportsNodeList)
            {
                xmlSportModels.Add(this.ConvertToXmlSportModel(currentSportNode));
            }

            return xmlSportModels;
        }

        private XmlSportModel ConvertSportNodeToXmlSportModel(XmlNode sportNode)
        {
            XmlSportModel xmlSportModel = new XmlSportModel();

            if (sportNode.Attributes["Name"] != null && sportNode.Attributes["ID"] != null)
            {
                string sportName = sportNode.Attributes["Name"].Value;
                int sportFeedId = int.Parse(sportNode.Attributes["ID"].Value);

                xmlSportModel.Name = sportName;
                xmlSportModel.FeedId = sportFeedId;
            }

            return xmlSportModel;
        }

        private XmlEventModel ConvertEventNodeToXmlEventModel(XmlNode eventNode)
        {
            XmlEventModel xmlEventModel = new XmlEventModel();

            if (eventNode.Attributes["Name"] != null && eventNode.Attributes["ID"] != null
                && eventNode.Attributes["IsLive"] != null && eventNode.Attributes["CategoryID"] != null)
            {
                xmlEventModel.Name = eventNode.Attributes["Name"].Value;
                xmlEventModel.FeedId = int.Parse(eventNode.Attributes["ID"].Value);
                xmlEventModel.IsLive = bool.Parse(eventNode.Attributes["IsLive"].Value);
                xmlEventModel.CategoryId = int.Parse(eventNode.Attributes["CategoryID"].Value);
            }

            return xmlEventModel;
        }

        private XmlMatchModel ConvertMatchNodeToXmlMatchModel(XmlNode matchNode)
        {
            XmlMatchModel xmlMatchModel = new XmlMatchModel();

            if (matchNode.Attributes["Name"] != null && matchNode.Attributes["ID"] != null
                && matchNode.Attributes["StartDate"] != null && matchNode.Attributes["MatchType"] != null)
            {
                xmlMatchModel.Name = matchNode.Attributes["Name"].Value;
                xmlMatchModel.FeedId = int.Parse(matchNode.Attributes["ID"].Value);
                xmlMatchModel.StartDate = DateTime.Parse(matchNode.Attributes["StartDate"].Value);
                xmlMatchModel.MatchType = matchNode.Attributes["MatchType"].Value;
            }

            return xmlMatchModel;
        }

        private XmlBetModel ConverBetNodeToXmlBetModel(XmlNode betNode)
        {
            XmlBetModel xmlBetModel = new XmlBetModel();

            if (betNode.Attributes["Name"] != null && betNode.Attributes["ID"] != null
                && betNode.Attributes["IsLive"] != null)
            {
                xmlBetModel.Name = betNode.Attributes["Name"].Value;
                xmlBetModel.FeedId = int.Parse(betNode.Attributes["ID"].Value);
                xmlBetModel.IsLive = bool.Parse(betNode.Attributes["IsLive"].Value);
            }

            return xmlBetModel;
        }

        private XmlOddModel ConvertOddNodeToXmlOddMOdel(XmlNode oddNode)
        {
            XmlOddModel xmlOddModel = new XmlOddModel();

            if (oddNode.Attributes["Name"] != null && oddNode.Attributes["ID"] != null
                && oddNode.Attributes["Value"] != null)
            {
                xmlOddModel.Name = oddNode.Attributes["Name"].Value;
                xmlOddModel.FeedId = int.Parse(oddNode.Attributes["ID"].Value);
                xmlOddModel.Value = decimal.Parse(oddNode.Attributes["Value"].Value);

                if (oddNode.Attributes["SpecialBetValue"] != null)
                {
                    xmlOddModel.SpecialBetValue = oddNode.Attributes["SpecialBetValue"].Value;
                }

            }

            return xmlOddModel;
        }
    }
}