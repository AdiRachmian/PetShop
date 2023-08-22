using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Models;

namespace PetShop.Services
{
    public class AnimalService : IAnimalService
    {
        private ApplicationDbContext _context;

        public AnimalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Animal? GetAnimalDetailsById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return _context.Animal
            .Include(a => a.Comments)
            .Include(a => a.Category)
            .FirstOrDefault(a => a.AnimalId == id);
        }

        public List<Animal> GetAllAnimals()
        {
            return _context.Animal.Include(a => a.Category).ToList();
        }

        public List<Category> GetCategories()
        {
            return _context.Category.ToList();
        }

        public List<Animal> GetTopComments()
        {
            List<Animal> topAnimals = _context.Animal
                .Include(a => a.Comments)
                .OrderByDescending(a => a.Comments!.Count)
                .Take(2)
                .ToList();

            return topAnimals;
        }

        public List<Animal> GetAnimalsByCategoryId(int categoryId)
        {
            return _context.Animal.Where(a => a.CategoryId == categoryId).ToList();
        }

        public string GetCategoryNameById(int categoryId)
        {
			return _context.Category.Where(c => c.CategoryId == categoryId).First()?.Name!;
		}

        public void AddAnimal(Animal animal)
        {
            _context.Add(animal);
            _context.SaveChanges();
        }

        public Animal EditAnimalGet(int id)
        {
            var animal = _context.Animal.Find(id);

            return animal!;
        }

        public void EditAnimalPost(Animal animal)
        {
            _context.Animal.Attach(animal);
            _context.Entry(animal).State = EntityState.Modified;

            _context.Update(animal);
            _context.SaveChanges();
        }

        public Animal DeleteAnimalGet(int id)
        {
            var animal = _context.Animal.FirstOrDefault(m => m.AnimalId == id);

            return animal!;
        }

        public async Task DeleteAnimalPost(int id)
        {
            var animal = _context.Animal.FirstOrDefault(a => a.AnimalId == id);

            if (animal != null)
            {
                _context.Animal.Remove(animal);
                await _context.SaveChangesAsync();
            }
        }

        public void AddComment(string commentInput, int animalId)
        {
            Comment comment = new() { AnimalId = animalId, Content = commentInput };

            _context.Comment.Add(comment);
            _context.SaveChanges();
        }

        public void DeleteComment(int commentId)
        {
            var comment = _context.Comment.FirstOrDefault(c => c.CommentId == commentId);
            if (comment != null)
            {
                _context.Comment.Remove(comment);
                _context.SaveChanges();
            }
        }
    }
}