namespace SimpleXmlFeed.Services.Data
{
    using System;
    using System.Linq;

    using SimpleXmlFeed.Data.Contracts;
    using Contracts;
    using Models.Data;

    public class EventsService : IEventsService
    {
        private readonly IRepository<Event> events;

        public EventsService(IRepository<Event> events)
        {
            this.events = events;
        }

        public Event Add(string name, int feedId, Sport sport, bool isLive, int categoryId, bool saveChanges = true)
        {
            Event eventToAdd = new Event
            {
                Name = name,
                FeedId = feedId,
                Sport = sport,
                IsLive = isLive,
                CategoryId = categoryId
            };

            this.events.Add(eventToAdd);

            if (saveChanges)
            {
                this.SaveChanges();
            }

            return eventToAdd;
        }

        public Guid GetEventIdByFeedId(int feedId)
        {
            Guid eventId = this.events
                                .All()
                                .Where(ev => ev.FeedId == feedId)
                                .Select(ev => ev.Id)
                                .FirstOrDefault();

            return eventId;
        }

        public Event GetEventByFeedId(int feedId)
        {
            Event resultEvent = this.events
                                .All()
                                .Where(ev => ev.FeedId == feedId)
                                .FirstOrDefault();

            return resultEvent;
        }

        public Event UpdateEvent(string name, int feedId, bool isLive, int categoryId, bool saveChanges = true)
        {
            Event eventToUpdate = this.GetEventByFeedId(feedId);

            if (eventToUpdate != null)
            {
                if (eventToUpdate.Name != name || eventToUpdate.IsLive != isLive || eventToUpdate.CategoryId != categoryId)
                {
                    eventToUpdate.Name = name;
                    eventToUpdate.IsLive = isLive;
                    eventToUpdate.CategoryId = categoryId;
                }

                this.events.Update(eventToUpdate);

                if (saveChanges)
                {
                    this.SaveChanges();
                }
            }

            return eventToUpdate;
        }

        public int SaveChanges()
        {
            return this.events.SaveChanges();
        }
    }
}
