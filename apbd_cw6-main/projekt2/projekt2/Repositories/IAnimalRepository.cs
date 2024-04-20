using Microsoft.AspNetCore.Mvc;
using projekt2.Models;
using projekt2.Models.DTOs;

namespace projekt2.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals();
    public Animal GetAnimalById(int id);
    IEnumerable<Animal> OrderBy(string sortcategory);
    void AddAnimal(AddAnimal animal);
    void DeleteAnimal(int id);
    Animal EditAnimal(int id,[FromBody]Animal animal);
    

}