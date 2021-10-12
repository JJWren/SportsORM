using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;


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
                .ToList();

            ViewBag.ReverseOrderedByTeamNameTeams = _context.Teams
                .OrderByDescending(t => t.TeamName)
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
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}