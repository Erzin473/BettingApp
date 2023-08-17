using BettingWebApp.Data;
using BettingWebApp.Models;
using BettingWebApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BettingWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataServices _dataService;
        private readonly ILogger<HomeController> _logger;
        private Timer _timer;

        public HomeController(IDataServices dataService, ILogger<HomeController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _timer = new Timer(TimerCallback, null, 0, 60000); // 1 minute interval
            return View();
        }

        private async void TimerCallback(object state)
        {
            try
            {
                string xmlUrl = "https://sports.ultraplay.net/sportsxml?clientKey=9C5E796D-4D54-42FD-A535-D7E77906541A&sportId=2357&days=7";
                string xmlData;

                using (HttpClient client = new HttpClient())
                {
                    xmlData = await client.GetStringAsync(xmlUrl);
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlData);

                string connectionString = "Server=LAPTOP-U48MADB3\\SQLEXPRESS;Database=BettingApp;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    await UpdateEventsAsync(xmlDoc, connection);
                    await UpdateMatchesAsync(xmlDoc, connection);
                    await UpdateBetsAsync(xmlDoc, connection);
                    await UpdateOddsAsync(xmlDoc, connection);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the timer callback.");
            }
        }

        private async Task UpdateEventsAsync(XmlDocument xmlDoc, SqlConnection connection)
        {
            //Event Table
            XmlNodeList eventNodes = xmlDoc.SelectNodes("/XmlSports/Sport/Event");
            foreach (XmlNode eventNode in eventNodes)
            {
                string elementEvent1 = eventNode.Attributes["ID"].Value;
                string elementEvent2 = eventNode.Attributes["Name"]?.Value;
                string elementEvent3 = eventNode.Attributes["IsLive"]?.Value;
                string elementEvent4 = eventNode?.Attributes["CategoryID"]?.Value;

                string existingRecordQuery = "SELECT COUNT(*) FROM Events WHERE ID = @elementEvent1";
                using (SqlCommand existingRecordCommand = new SqlCommand(existingRecordQuery, connection))
                {
                    existingRecordCommand.Parameters.AddWithValue("@elementEvent1", SqlDbType.Int).Value = Convert.ToInt32(elementEvent1);
                    int existingRecordCount = (int)existingRecordCommand.ExecuteScalar();

                    if (existingRecordCount > 0)
                    {
                        // Update existing record
                        string updateQuery = "UPDATE Events SET Name = @elementEvent2, IsLive = @elementEvent3, CategoryID = @elementEvent4 WHERE ID = @elementEvent1";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@elementEvent1", SqlDbType.Int).Value = Convert.ToInt32(elementEvent1);
                            updateCommand.Parameters.AddWithValue("@elementEvent2", SqlDbType.NVarChar).Value = elementEvent2;
                            updateCommand.Parameters.AddWithValue("@elementEvent3", SqlDbType.NVarChar).Value = elementEvent3;
                            updateCommand.Parameters.AddWithValue("@elementEvent4", SqlDbType.Int).Value = Convert.ToInt32(elementEvent4);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert new record
                        string insertQuery = "SET IDENTITY_INSERT Events ON; INSERT INTO Events (ID, Name, IsLive, CategoryID) VALUES (@elementEvent1, @elementEvent2, @elementEvent3, @elementEvent4); SET IDENTITY_INSERT Events OFF;";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@elementEvent1", SqlDbType.Int).Value = Convert.ToInt32(elementEvent1);
                            insertCommand.Parameters.AddWithValue("@elementEvent2", SqlDbType.NVarChar).Value = elementEvent2;
                            insertCommand.Parameters.AddWithValue("@elementEvent3", SqlDbType.NVarChar).Value = elementEvent3;
                            insertCommand.Parameters.AddWithValue("@elementEvent4", SqlDbType.Int).Value = Convert.ToInt32(elementEvent4);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        private async Task UpdateMatchesAsync(XmlDocument xmlDoc, SqlConnection connection)
        {
            //Match Table
            XmlNodeList matchNodes = xmlDoc.SelectNodes("/XmlSports/Sport/Event");
            foreach (XmlNode matchNode in matchNodes)
            {
                XmlNode matchIdNode = matchNode.SelectSingleNode("Match");
                string elementMatch1 = matchIdNode.Attributes["ID"].Value;
                string elementMatch2 = matchIdNode.Attributes["Name"]?.Value;
                string elementMatch3 = matchIdNode.Attributes["StartDate"]?.Value;
                string elementMatch4 = matchIdNode?.Attributes["MatchType"]?.Value;
                string elementMatch5 = matchNode.Attributes["ID"].Value;

                string existingRecordQueryMatch = "SELECT COUNT(*) FROM Matchs WHERE ID = @elementMatch1";
                using (SqlCommand existingRecordCommand = new SqlCommand(existingRecordQueryMatch, connection))
                {
                    existingRecordCommand.Parameters.AddWithValue("@elementMatch1", SqlDbType.Int).Value = Convert.ToInt32(elementMatch1);
                    int existingRecordCount = (int)existingRecordCommand.ExecuteScalar();

                    if (existingRecordCount > 0)
                    {
                        // Update existing record
                        string updateQueryMatch = "UPDATE Matchs SET Name = @elementMatch2, StartDate = @elementMatch3, MatchType = @elementMatch4, EventID = @elementMatch5 WHERE ID = @elementMatch1";
                        using (SqlCommand updateCommand = new SqlCommand(updateQueryMatch, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@elementMatch1", SqlDbType.Int).Value = Convert.ToInt32(elementMatch1);
                            updateCommand.Parameters.AddWithValue("@elementMatch2", SqlDbType.NVarChar).Value = elementMatch2;
                            updateCommand.Parameters.AddWithValue("@elementMatch3", SqlDbType.NVarChar).Value = elementMatch3;
                            updateCommand.Parameters.AddWithValue("@elementMatch4", SqlDbType.NVarChar).Value = elementMatch4;
                            updateCommand.Parameters.AddWithValue("@elementMatch5", SqlDbType.Int).Value = Convert.ToInt32(elementMatch5);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = "SET IDENTITY_INSERT Matchs ON; INSERT INTO Matchs (ID, Name, StartDate, MatchType, EventID) VALUES (@elementMatch1, @elementMatch2, @elementMatch3, @elementMatch4, @elementMatch5); SET IDENTITY_INSERT Matchs OFF;";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@elementMatch1", SqlDbType.Int).Value = Convert.ToInt32(elementMatch1);
                            command.Parameters.AddWithValue("@elementMatch2", SqlDbType.NVarChar).Value = elementMatch2;
                            command.Parameters.AddWithValue("@elementMatch3", SqlDbType.NVarChar).Value = elementMatch3;
                            command.Parameters.AddWithValue("@elementMatch4", SqlDbType.NVarChar).Value = elementMatch4;
                            command.Parameters.AddWithValue("@elementMatch5", SqlDbType.Int).Value = Convert.ToInt32(elementMatch5);
                            command.ExecuteNonQuery();
                        }
                    }
                }

            }
        }
        private async Task UpdateBetsAsync(XmlDocument xmlDoc, SqlConnection connection)
        {
            /// Bets Table
            XmlNodeList betNodes = xmlDoc.SelectNodes("/XmlSports/Sport/Event/Match");
            foreach (XmlNode matchNode in betNodes)
            {
                XmlNodeList betNode = matchNode.SelectNodes("Bet");
                foreach (XmlNode item in betNode)
                {
                    string element1 = item?.Attributes["ID"]?.Value;
                    string element2 = item?.Attributes["Name"]?.Value;
                    string element3 = item?.Attributes["IsLive"]?.Value;
                    string element4 = matchNode.Attributes["ID"]?.Value;

                    string existingRecordQueryBets = "SELECT COUNT(*) FROM Bets WHERE ID = @element1";
                    using (SqlCommand existingRecordCommand = new SqlCommand(existingRecordQueryBets, connection))
                    {
                        existingRecordCommand.Parameters.AddWithValue("@element1", SqlDbType.Int).Value = Convert.ToInt32(element1);
                        int existingRecordCount = (int)existingRecordCommand.ExecuteScalar();

                        if (existingRecordCount > 0)
                        {
                            // Update existing record
                            string updateQueryBets = "UPDATE Bets SET Name = @element2, IsLive = @element3, MatchID = @element4 WHERE ID = @element1";
                            using (SqlCommand updateCommand = new SqlCommand(updateQueryBets, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@element1", SqlDbType.Int).Value = Convert.ToInt32(element1);
                                updateCommand.Parameters.AddWithValue("@element2", SqlDbType.NVarChar).Value = element2;
                                updateCommand.Parameters.AddWithValue("@element3", SqlDbType.NVarChar).Value = element3;
                                updateCommand.Parameters.AddWithValue("@element4", SqlDbType.Int).Value = Convert.ToInt32(element4);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insertQuery = "SET IDENTITY_INSERT Bets ON; INSERT INTO Bets (ID, Name, IsLive, MatchID) VALUES (@element1, @element2, @element3, @element4); SET IDENTITY_INSERT Bets OFF;";
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@element1", SqlDbType.Int).Value = Convert.ToInt32(element1);
                                command.Parameters.AddWithValue("@element2", SqlDbType.NVarChar).Value = element2;
                                command.Parameters.AddWithValue("@element3", SqlDbType.NVarChar).Value = element3;
                                command.Parameters.AddWithValue("@element4", SqlDbType.Int).Value = Convert.ToInt32(element4);
                                command.ExecuteNonQuery();
                            }
                        }

                    }
                }
            }
        }
        private async Task UpdateOddsAsync(XmlDocument xmlDoc, SqlConnection connection)
        {
            // Odds Table
            XmlNodeList oddNodes = xmlDoc.SelectNodes("/XmlSports/Sport/Event/Match/Bet");
            foreach (XmlNode oddNode in oddNodes)
            {
                XmlNodeList oddNodess = oddNode.SelectNodes("Odd");
                foreach (XmlNode itemOdd in oddNodess)
                {
                    string elementOdd1 = itemOdd.Attributes["ID"].Value;
                    string elementOdd2 = itemOdd.Attributes["Name"].Value;
                    string elementOdd3 = itemOdd.Attributes["Value"].Value;
                    string elementOdd4 = itemOdd?.Attributes["SpecialBetValue"]?.Value; 
                    string elementOdd5 = oddNode.Attributes["ID"].Value;

                    string specialBetValue = null;
                    if (elementOdd4 == null)
                    {
                        specialBetValue = null;
                    }
                    else
                    {
                        specialBetValue = elementOdd4;
                    }


                    string existingRecordQueryOdds = "SELECT COUNT(*) FROM Odds WHERE ID = @elementOdd1";
                    using (SqlCommand existingRecordCommand = new SqlCommand(existingRecordQueryOdds, connection))
                    {
                        existingRecordCommand.Parameters.AddWithValue("@elementOdd1", SqlDbType.Int).Value = Convert.ToInt32(elementOdd1);
                        int existingRecordCount = (int)existingRecordCommand.ExecuteScalar();

                        if (existingRecordCount > 0)
                        {
                            // Update existing record
                            string updateQueryOdds = "UPDATE Odds SET Name = @elementOdd2, Value = @elementOdd3, SpecialBetValue = @elementOdd4, BetID = @elementOdd5 WHERE ID = @elementOdd1";
                            using (SqlCommand updateCommand = new SqlCommand(updateQueryOdds, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@elementOdd1", Convert.ToInt32(elementOdd1));
                                updateCommand.Parameters.AddWithValue("@elementOdd2", elementOdd2);
                                updateCommand.Parameters.AddWithValue("@elementOdd3", elementOdd3);
                                updateCommand.Parameters.AddWithValue("@elementOdd4", specialBetValue != null ? specialBetValue : (object)DBNull.Value);
                                updateCommand.Parameters.AddWithValue("@elementOdd5", Convert.ToInt32(elementOdd5));
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insertQuery = "SET IDENTITY_INSERT Odds ON; INSERT INTO Odds (ID, Name, Value, SpecialBetValue, BetID) VALUES (@elementOdd1, @elementOdd2, @elementOdd3, @elementOdd4, @elementOdd5); SET IDENTITY_INSERT Odds OFF;";
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@elementOdd1", Convert.ToInt32(elementOdd1));
                                command.Parameters.AddWithValue("@elementOdd2", elementOdd2);
                                command.Parameters.AddWithValue("@elementOdd3", elementOdd3);
                                command.Parameters.AddWithValue("@elementOdd4", specialBetValue != null ? specialBetValue : (object)DBNull.Value);
                                command.Parameters.AddWithValue("@elementOdd5", Convert.ToInt32(elementOdd5));
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        public IActionResult Betting24H()
        {
            var models = new ViewModels
            {
                EventData = _dataService.GetEventData(),
                MatchData = _dataService.GetMatchData(),
                BetData = _dataService.GetBetData(),
                OddData = _dataService.GetOddData()
            };
            return View(models);
        }

        public IActionResult ViewMatch(int id)
        {
            var matchDetails = _dataService.GetMatchById(id);

            if (matchDetails == null)
            {
                return NotFound();
            }

            return View(matchDetails);
        }

    }
}


