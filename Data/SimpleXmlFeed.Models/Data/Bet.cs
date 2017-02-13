namespace SimpleXmlFeed.Models.Data
{
    using Abstractions;
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Bet : BaseFeedItem, ILiveable
    {
        private ICollection<Odd> odds;

        public Bet()
        {
            this.odds = new HashSet<Odd>();
        }

        public bool IsLive { get; set; }

        [Required]
        public Guid MatchId { get; set; }

        public virtual Match Match { get; set; }

        public virtual ICollection<Odd> Odds
        {
            get { return this.odds; }
            set { this.odds = value; }
        }
    }
}
