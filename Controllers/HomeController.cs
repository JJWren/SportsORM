using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;
using Microsoft.EntityFrameworkCore;


namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.WomensLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Women"))
                .ToList();

            ViewBag.HockeyLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Hockey"))
                .ToList();

            ViewBag.NotFootballLeagues = _context.Leagues
                .Where(l => !l.Sport.Contains("Football"))
                .ToList();

            ViewBag.ConferenceLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Conference"))
                .ToList();

            ViewBag.AtlanticLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Atlantic"))
                .ToList();

            ViewBag.DallasTeams = _context.Teams
                .Where(t => t.Location.Contains("Dallas"))
                .ToList();

            ViewBag.RaptorsTeams = _context.Teams
                .Where(t => t.TeamName.Contains("Raptors"))
                .ToList();
            
            ViewBag.CityTeams = _context.Teams
                .Where(t => t.Location.Contains("City"))
                .ToList();

            ViewBag.StartsWithTTeams = _context.Teams
                .Where(t => t.TeamName.Contains("T"))
                .ToList();

            ViewBag.OrderedByLocationTeams = _context.Teams
                .OrderBy(t => t.Location)
                .ThenBy(t => t.TeamName)
                .ToList();

            ViewBag.ReverseOrderedByTeamNameTeams = _context.Teams
                .OrderByDescending(t => t.TeamName)
                .ThenByDescending(t => t.Location)
                .ToList();

            ViewBag.LastNameCooperPlayers = _context.Players
                .Where(p => p.LastName.Contains("Cooper"))
                .OrderBy(p => p.FirstName)
                .ToList();

            ViewBag.FirstNameJoshuaPlayers = _context.Players
                .Where(p => p.FirstName.Contains("Joshua"))
                .OrderBy(p => p.LastName)
                .ToList();

            ViewBag.LNameCooperFNameNotJoshuaPlayers = _context.Players
                .Where(p => !p.FirstName.Contains("Joshua") && p.LastName.Contains("Cooper"))
                .OrderBy(p => p.FirstName)
                .ToList();

            ViewBag.AlexanderAndWyattPlayers = _context.Players
                .Where(p => p.FirstName.Contains("Alexander") || p.FirstName.Contains("Wyatt"))
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToList();
            
            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            ViewBag.AtlanticSoccerConf = _context.Teams
                .Where(team => team.CurrLeague.Name.Contains("Atlantic") && team.CurrLeague.Sport.Contains("Soccer"))
                .ToList();

            ViewBag.BostonPenguinsCurrentRoster = _context.Players
                .Where(p => p.CurrentTeam.TeamName.Contains("Penguins") && p.CurrentTeam.Location.Contains("Boston"))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList();

            ViewBag.IntlCollegiateBaseballConfRoster = _context.Players
                .Where(p => p.CurrentTeam.CurrLeague.Name.Contains("International Collegiate Baseball Conference"))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList();

            ViewBag.AmericanConfAmFootball_Lopez = _context.Players
                .Where(p => p.CurrentTeam.CurrLeague.Name.Contains("American Conference of Amateur Football") && p.LastName.Contains("Lopez"))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList();

            ViewBag.AllFootballRoster = _context.Players
                .Where(p => p.CurrentTeam.CurrLeague.Sport == "Football")
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList();

            ViewBag.TeamsWithASophia = _context.Players
                .Where(p => p.FirstName == "Sophia" || p.LastName == "Sophia")
                .Select(p => p.CurrentTeam)
                .Distinct()
                .OrderBy(team => team.TeamName)
                .ThenBy(team => team.Location)
                .ToList();
            
            #region [This helps for a comparison using Razor]
            ViewBag.SophiaRoster = _context.Players
                .Where(p => p.FirstName == "Sophia" || p.LastName == "Sophia")
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList();
            #endregion

            ViewBag.LeaguesWithSophia = _context.Players
                .Where(p => p.FirstName == "Sophia" || p.LastName == "Sophia")
                .Select(p => p.CurrentTeam.CurrLeague)
                .Distinct()
                .OrderBy(l => l.Sport)
                .ThenBy(l => l.Name)
                .ToList();

            ViewBag.FloresRoster_NonRoughRider = _context.Players
                .Where(p => p.LastName.Contains("Flores") && p.CurrentTeam.Location + p.CurrentTeam.TeamName != "Washington Roughriders")
                .Include(p => p.CurrentTeam)
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList();

            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            ViewBag.SamuelEvansTeams = _context.Teams
                .Include(team => team.AllPlayers)
                    .ThenInclude(playerteam => playerteam.PlayerOnTeam)
                .Where(team => team.AllPlayers.Any(player => player.PlayerOnTeam.FirstName == "Samuel" && player.PlayerOnTeam.LastName == "Evans"))
                .Include(team => team.CurrLeague)
                .ToList();

            ViewBag.ManitobaTigerPlayers = _context.Players
                .Include(player => player.AllTeams)
                    .ThenInclude(playerteam => playerteam.PlayerOnTeam)
                .Where(player => player.AllTeams.Any(player => player.TeamOfPlayer.Location == "Manitoba" && player.TeamOfPlayer.TeamName == "Tiger-Cats"))
                .OrderBy(player => player.LastName)
                .ThenBy(player => player.FirstName)
                .ToList();

            ViewBag.FormerWVikings = _context.Players
                .Include(p => p.AllTeams)
                    .ThenInclude(pt => pt.PlayerOnTeam)
                .Include(p => p.CurrentTeam)
                .Where(p => p.AllTeams.Any(p => p.TeamOfPlayer.Location == "Wichita" && p.TeamOfPlayer.TeamName == "Vikings") && p.CurrentTeam.TeamName != "Vikings")
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList();

            ViewBag.JacobGreyTeamsB4Colts = _context.Teams
                .Include(team => team.AllPlayers)
                    .ThenInclude(pt => pt.PlayerOnTeam)
                .Include(team => team.CurrentPlayers)
                .Where(team => team.AllPlayers.Any(pt => pt.PlayerOnTeam.FirstName == "Jacob" && pt.PlayerOnTeam.LastName == "Gray") && (team.Location + team.TeamName != "Oregon Colts"))
                .Include(team => team.CurrLeague)
                .OrderBy(team => team.Location)
                .ThenBy(team => team.TeamName)
                .ToList();

            ViewBag.JoshuasInAFABaseball = _context.Players
                .Where(p => p.FirstName == "Joshua")
                .Include(p => p.AllTeams)
                    .ThenInclude(pt => pt.TeamOfPlayer)
                        .ThenInclude(team => team.CurrLeague)
                .Where(p => p.AllTeams.Any(team => team.TeamOfPlayer.CurrLeague.Name == "Atlantic Federation of Amateur Baseball Players"))
                .ToList();
            
            #region [Proof that there were no Joshua players in the Atlantic Federation of Amateur Baseball]
            
            var AFABaseballPlayers = _context.Players
                .Where(p => p.FirstName == "Joshua")
                .Include(p => p.AllTeams)
                    .ThenInclude(pt => pt.TeamOfPlayer)
                        .ThenInclude(team => team.CurrLeague)
                .ToList();
            
            foreach (var p in AFABaseballPlayers)
            {
                Console.WriteLine($"{p.FirstName} {p.LastName}:");
                foreach (var t in p.AllTeams)
                {
                    Console.WriteLine($"\t{t.TeamOfPlayer.Location} {t.TeamOfPlayer.TeamName}, {t.TeamOfPlayer.CurrLeague.Name}");
                }
            }
            #endregion

            ViewBag.AnyTeamWith12Plus = _context.Teams
                .Include(t => t.AllPlayers)
                .Where(t => t.AllPlayers.Count > 11)
                .OrderBy(t => t.Location)
                .ThenBy(t => t.TeamName)
                .ToList();

            ViewBag.PlayersSortedNumTeamsPlayedFor = _context.Players
                .Include(p => p.AllTeams)
                .OrderByDescending(p => p.AllTeams.Count)
                .ThenBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList();

            return View();
        }
    }
}