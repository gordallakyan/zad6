using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using projekt2.Models;
using projekt2.Models.DTOs;
using projekt2.Repositories;

namespace projekt2.Controllers;
[ApiController]
[Route("aniapi/animals")]
public class AnimalController : Controller
{
 private readonly IAnimalRepository _animalRepository;
 public AnimalController(IAnimalRepository animalRepository)
 {
  _animalRepository = animalRepository;
 }
 
 [HttpGet("getall")]
 public IActionResult GetAnimals(){
  var animals = _animalRepository.GetAnimals();
  return Ok(animals);
 }
 [HttpGet("orderby")]
 public IActionResult OrderBy(string ordercategory){
  var animals = _animalRepository.OrderBy(ordercategory);
  return Ok(animals);
 }
 [HttpGet("{id}")]
 public IActionResult GetAnimalById(int id){
  var animals = _animalRepository.GetAnimalById(id);
  return Ok(animals);
 }
 [HttpPost]
 public IActionResult AddAnimal(AddAnimal animal){
_animalRepository.AddAnimal(animal);
  return Created("", null);
 }
 [HttpDelete]
 public IActionResult DeleteAnimal(int id) {
  _animalRepository.DeleteAnimal(id);
  return  NoContent();
 }
 [HttpPut]
 public IActionResult EditAnimal(int id,[FromBody]Animal animal){
  var editedAnimal = _animalRepository.EditAnimal(id,animal);
  return Ok(editedAnimal);
 }
 
 
 
 
 
 
 
}