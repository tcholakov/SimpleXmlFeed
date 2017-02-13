namespace SimpleXmlFeed.Models.Data
{
    using Abstractions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Match : BaseFeedItem
    {
        private ICollection<Bet> bets;

        public Match()
        {
            this.bets = new HashSet<Bet>();
        }

        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

        [Required]
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        public virtual ICollection<Bet> Bets
        {
            get { return this.bets; }
            set { this.bets = value; }
        }
    }
}
