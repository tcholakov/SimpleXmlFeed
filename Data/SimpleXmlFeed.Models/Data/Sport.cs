namespace SimpleXmlFeed.Models.Data
{
    using Abstractions;
    using System.Collections.Generic;

    public class Sport : BaseFeedItem
    {
        private ICollection<Event> events;

        public Sport()
        {
            this.events = new HashSet<Event>();
        }

        public virtual ICollection<Event> Events
        {
            get { return this.events; }
            set { this.events = value; }
        }
    }
}
