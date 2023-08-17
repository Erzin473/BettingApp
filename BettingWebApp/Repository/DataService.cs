using BettingWebApp.Data;
using BettingWebApp.Models;

namespace BettingWebApp.Repository
{
    public interface IDataServices
    {
        List<Event> GetEventData();
        List<Match> GetMatchData();
        List<Bet> GetBetData();
        List<Odd> GetOddData();
        Match GetMatchById(int id);
    }

    public class DataService : IDataServices
    {
        private readonly ApplicationDbContext _dbContext;

        public DataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Event> GetEventData()
        {
            return _dbContext.Events.ToList();
        }

        public List<Match> GetMatchData()
        {
            return _dbContext.Matchs.ToList();
        }

        public List<Bet> GetBetData()
        {
            return _dbContext.Bets.ToList();
        }

        public List<Odd> GetOddData()
        {
            return _dbContext.Odds.ToList();
        }

        public Match GetMatchById(int id) 
        {
            return _dbContext.Matchs.FirstOrDefault(m => m.ID == id);
        }
    }
}

