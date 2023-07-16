using AdminPage.Models;
using CinemaObject;
using DataAccess.respository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HighCinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IAccountRepository accountRespository = null;
        IMovieRepository movieRespository = null;

        public HomeController(ILogger<HomeController> logger)
        {
            accountRespository = new AccountRepository();
            movieRespository = new MovieRepository();

            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.movieList = movieRespository.GetMovies();
            var movieList = movieRespository.GetMovies();
            int count = 0;
            var moList = new List<Movie>();
            foreach (var i in movieList)
            {
                if (count == 6)
                {
                    break;
                }

                moList.Add(i);
                count++;
            }
            viewModel.mostPopularMovies = moList;

            return View(viewModel);
        }


        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search()
        {
            var movieList = movieRespository.GetMovies();
            string searchMovie = Request.Form["Search"] + "";
            Console.WriteLine(searchMovie);

            var searchMovieList = new List<Movie>();

            foreach (var movie in movieList)
            {
                if (movie.Name.ToLower().Contains(searchMovie.ToLower()))
                {
                    searchMovieList.Add(movie);
                }
            }

            if (searchMovieList.Count == 0)
            {
                TempData["NotFoundMovie"] = "Sorry, HighCinema has not this movie!";
                return Redirect("/Home/Index");
            }

            return View(searchMovieList);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = accountRespository.GetAccounts().Where(a => a.UserName.Equals(account.UserName) && a.Password.Equals(account.Password) && a.Role == 1).FirstOrDefault();
                    if (obj != null)
                    {
                        // SessionAuthentication["Email"] = member.Email;
                        HttpContext.Session.SetString("Username", (obj.UserName).ToString());
                        HttpContext.Session.SetString("Password", (obj.Password).ToString());
                        HttpContext.Session.SetString("Role", (obj.Role).ToString());

                        Console.WriteLine(HttpContext.Session.GetString("Username"));
                        Console.WriteLine(HttpContext.Session.GetString("Password"));
                        Console.WriteLine(HttpContext.Session.GetString("Role"));
                    }

                    if ((HttpContext.Session.GetString("Username") != null)
                        && (HttpContext.Session.GetString("Password") != null))
                    {
                        TempData["Login"] = "Login Successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Login"] = "Login fail!, please enter correct account and password!";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
            return View();
        }


        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    accountRespository.InsertAccount(account);
                }
                TempData["Register"] = "Register Successfully!";
                return Redirect("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Console.WriteLine(ex.Message);
                return View();
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
