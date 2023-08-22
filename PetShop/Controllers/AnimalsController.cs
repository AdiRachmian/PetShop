using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PetShop.Data;
using PetShop.Models;
using PetShop.Services;

namespace PetShop.Controllers
{
    public class AnimalsController : Controller
    {
        private IAnimalService _animalService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AnimalsController(IAnimalService animalService, IWebHostEnvironment hostEnvironment)
        {
            _animalService = animalService;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Animals
        [ActionName("Catalog")]
        public IActionResult DisplayByCategory(int? id)
        {
            List<Animal> AnimalList;

            if(id == null)
            {
                AnimalList = _animalService.GetAllAnimals();
                ViewBag.CategoryName = "Choose Category";
            }

            else
            {
                AnimalList = _animalService.GetAnimalsByCategoryId((int)id);
                ViewBag.CategoryName = _animalService.GetCategoryNameById((int)id);
            }

            ViewBag.Categories = _animalService.GetCategories();

            return View("Catalog", AnimalList);
        }

        public string GetCategoryName(int id)
        {
            string catName = _animalService.GetCategoryNameById(id);
            ViewBag.CategoryById = _animalService.GetCategoryNameById(id);

            return catName;
        }

        // GET: Animals/Details/id
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _animalService.GetAnimalDetailsById(id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/AddAnimal
        [HttpGet]
        [Authorize]
        public IActionResult AddAnimal()
        {
            var categories = _animalService.GetCategories();
            ViewData["CategoryList"] = new SelectList(categories, "CategoryId", "Name");

            return View();
        }

        // POST: Animals/AddAnimal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAnimal([Bind("Id,Name,Age,ImageFile,Description,CategoryId")] Animal animal)
        {
            if (animal.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + animal.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await animal.ImageFile.CopyToAsync(fileStream);
                }

                animal.PictureName = uniqueFileName;
            }

            _animalService.AddAnimal(animal);

            return RedirectToAction("Catalog");
        }

        //// GET: Animals/Edit/id
        [Authorize]
        public IActionResult Edit(int id)
        {
            var animal = _animalService.EditAnimalGet(id);
            
            if (animal == null)
            {
                return NotFound();
            }

            List<Category> categories = _animalService.GetCategories();

            SelectList categoryList = new SelectList(categories, "CategoryId", "Name", animal.CategoryId);

            ViewBag.CategoryList = categoryList;

            return View(animal);
        }

        //POST: Animals/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId, Name, Age, ImageFile, Description, CategoryId")] Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }


            Animal? existingAnimal = _animalService.GetAnimalDetailsById(animal.AnimalId);

            // TODO Extract to function,  void MapAnimal(Animal existingAnimal, Animal newAnimal);
            if (existingAnimal == null)
            {
                return NotFound();
            }

            existingAnimal.Name = animal.Name;
            existingAnimal.Description = animal.Description;
            existingAnimal.Age = animal.Age;
            existingAnimal.CategoryId = animal.CategoryId;
            existingAnimal.Category = animal.Category;

            if (animal.ImageFile != null && animal.ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + animal.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await animal.ImageFile.CopyToAsync(fileStream);
                }

                existingAnimal.PictureName = uniqueFileName;
            }

            _animalService.EditAnimalPost(existingAnimal);

            return RedirectToAction("Details", new { id = existingAnimal.AnimalId });
        }

        // GET: Animals/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var animal = _animalService.DeleteAnimalGet((int)id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/id
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = _animalService.GetAnimalDetailsById(id);

            if (animal == null)
            {
                return NotFound();
            }

            foreach (var comment in animal.Comments.ToList())
            {
                _animalService.DeleteComment(comment.CommentId);
            }

            await _animalService.DeleteAnimalPost(id);

            return RedirectToAction("Catalog");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(int animalId, string comment)
        {
            _animalService.AddComment(comment, animalId);
            return RedirectToAction("Details", new { id = animalId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteComment(Comment comment)
        {
            _animalService.DeleteComment(comment.CommentId);

            return RedirectToAction("Details", new { id = comment.AnimalId });
        }
    }
}
