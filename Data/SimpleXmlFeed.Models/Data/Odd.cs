namespace SimpleXmlFeed.Models.Data
{
    using Abstractions;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Odd : BaseFeedItem
    {
        public decimal Value { get; set; }

        public string SpecialBetValue { get; set; }

        [Required]
        public Guid BetId { get; set; }

        public virtual Bet Bet { get; set; }
    }
}
