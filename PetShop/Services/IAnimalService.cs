using Microsoft.AspNetCore.Mvc;
using PetShop.Models;

namespace PetShop.Services
{
    public interface IAnimalService
    {
        public List<Animal> GetAllAnimals();
        public List<Animal> GetTopComments();
        public Animal? GetAnimalDetailsById(int? Id);
        public List<Animal> GetAnimalsByCategoryId(int categoryId);
        public string GetCategoryNameById(int categoryId);
        public List<Category> GetCategories();
        public void AddAnimal(Animal animal);
        public Animal EditAnimalGet(int id);
        public void EditAnimalPost(Animal animal);
        public Animal DeleteAnimalGet(int id);
        public Task DeleteAnimalPost(int id);
        public void AddComment(string commentInput, int animalId);
        public void DeleteComment(int commentId);
    }
}
