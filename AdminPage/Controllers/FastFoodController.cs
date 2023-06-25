using CinemaObject;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.respository;

namespace AdminPage.Controllers
{
    public class FastFoodController : Controller
    {
        IFastFoodRepository fastFoodRepository = null;
        public FastFoodController() => fastFoodRepository = new FastFoodRepository();
        // GET: FastFoodController
        public ActionResult Index()
        {
            var fastFoodlist = fastFoodRepository.GetFastFoods();
            return View(fastFoodlist);
        }

        // GET: FastFoodController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fastFood = fastFoodRepository.GetFastFoodByID(id.Value);
            if (fastFood == null)
            {
                return NotFound();
            }
            return View(fastFood);
        }

        // GET: FastFoodController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FastFoodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FastFood fastFood)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fastFoodRepository.InsertFastFood(fastFood);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(fastFood);
            }
        }

        // GET: FastFoodController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fastFood = fastFoodRepository.GetFastFoodByID(id.Value);
            if (fastFood == null)
            {
                return NotFound();
            }
            return View(fastFood);
        }

        // POST: FastFoodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FastFood fastFood)
        {
            try
            {
                if (id != fastFood.Id)
                    return NotFound();
                if (ModelState.IsValid)
                    fastFoodRepository.UpdateFastFood(fastFood);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: FastFoodController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fastFood = fastFoodRepository.GetFastFoodByID(id.Value);
            if (fastFood == null)
                return NotFound();
            return View(fastFood);
        }

        // POST: FastFoodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                fastFoodRepository.DeleteFastFood(id);
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
