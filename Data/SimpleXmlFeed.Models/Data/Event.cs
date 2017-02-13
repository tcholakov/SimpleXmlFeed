namespace SimpleXmlFeed.Models.Data
{
    using Abstractions;
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event : BaseFeedItem, ILiveable
    {
        private ICollection<Match> matches;

        public Event()
        {
            this.matches = new HashSet<Match>();
        }

        public bool IsLive { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public Guid SportId { get; set; }

        public virtual Sport Sport { get; set; }

        public virtual ICollection<Match> Matches
        {
            get { return this.matches; }
            set { this.matches = value; }
        }
    }
}
