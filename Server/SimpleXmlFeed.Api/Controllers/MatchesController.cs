namespace SimpleXmlFeed.Api.Controllers
{
    using System.Web.Http;
    using System.Linq;
    using System.Web.Http.Cors;

    using AutoMapper.QueryableExtensions;

    using Common;
    using Services.Data.Contracts;
    using Models.Match;

    public class MatchesController : ApiController
    {
        private readonly IMatchesService matchesService;

        public MatchesController(IMatchesService matchesService)
        {
            this.matchesService = matchesService;
        }

        [EnableCors(origins: GlobalConstants.ClientAppOrigin, headers: "*", methods: "get")]
        [Route("~/matches/with/odds/today")]
        [HttpGet]
        public IHttpActionResult GetMatchesWithOddsForToday(int page = 1)
        {
            var result = this.matchesService
                .AllMatchesWithOddsForNextHours(GlobalConstants.HoursForOneDay)
                .OrderBy(match => match.StartDate)
                .ProjectTo<MatchDetailsResponseModel>()
                .ToList();

            if (result.Count == 0)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}
