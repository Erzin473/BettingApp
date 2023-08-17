using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace BettingWebApp.Models
{
    public class ViewModels
    {
        public List<Event> EventData { get; set; }
        public List<Match> MatchData { get; set; }
        public List<Bet> BetData { get; set; }
        public List<Odd> OddData { get; set; }
    }
}
