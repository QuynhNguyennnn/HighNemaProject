using CinemaObject;
using DataAccess.respository;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminPage.Controllers
{
    public class MovieController : Controller
    {
        IMovieRepository movieRepository = null;
        public MovieController() => movieRepository = new MovieRepository();

        IGenresRepository genresRepository = new GenreRepository();
        // GET: MovieController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "1")
            {
                TempData["NeedLoginToBooking"] = "Please login to your account for booking ticket";
                return Redirect("/Home/Login");
            }
            var movieList = movieRepository.GetMovies();
            return View(movieList);
        }


        public ActionResult GetMovieByGenres(string genres)
        {
            var movieList = movieRepository.GetMovies();
            var searchMovieByGenres = movieList.Where(m => m.Genres.ToLower().Contains(genres.ToLower()));
            TempData["Genres"] = genres;
            return View(searchMovieByGenres);
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = movieRepository.GetMovieByID(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            ViewData["Genres"] = genresRepository.GetAllGenres();
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            Console.Write(movie.Name);
            Console.Write(movie.Description);
            
            try
            {
                if (ModelState.IsValid)
                {
                    movieRepository.InsertMovie(movie);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(movie);
            }
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = movieRepository.GetMovieByID(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Movie movie)
        {
            try
            {
                if (id != movie.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    movieRepository.UpdateMovie(movie);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = movieRepository.GetMovieByID(id.Value);
            Console.Write(movie);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                movieRepository.DeleteMovie(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
