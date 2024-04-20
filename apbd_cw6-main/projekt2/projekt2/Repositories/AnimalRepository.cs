using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using projekt2.Models;
using projekt2.Models.DTOs;

namespace projekt2.Repositories;

public class AnimalRepository : IAnimalRepository
{

    public readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<Animal> GetAnimals()
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM Animal;";
        var reader = command.ExecuteReader();

        var animals = new List<Animal>();
        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");
        int descriptionOrdinal = reader.GetOrdinal("Description");
        int categoryOrdinal = reader.GetOrdinal("Category");
        int areaOrdinal = reader.GetOrdinal("Area");
        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(nameOrdinal),
                Description = reader.GetString(descriptionOrdinal),
                Category = reader.GetString(categoryOrdinal),
                Area = reader.GetString(areaOrdinal)
            });
        }

        return animals;
    }
    public IEnumerable<Animal> OrderBy(string orderBy = "name")
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        
        string orderByColumn = "name";
        orderBy = orderBy.ToLower();
        if (orderBy.Equals("name") || orderBy.Equals("description") || orderBy.Equals("category") || orderBy.Equals("area"))
        {
            orderByColumn = orderBy.ToLower();
        }
        
        command.CommandText = $"SELECT * FROM Animal ORDER BY {orderByColumn};";
        var reader = command.ExecuteReader();

        var animals = new List<Animal>();
        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");
        int descriptionOrdinal = reader.GetOrdinal("Description");
        int categoryOrdinal = reader.GetOrdinal("Category");
        int areaOrdinal = reader.GetOrdinal("Area");

        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(nameOrdinal),
                Description = reader.GetString(descriptionOrdinal),
                Category = reader.GetString(categoryOrdinal),
                Area = reader.GetString(areaOrdinal)
            });
        }
        return animals;  
    }

    public void AddAnimal(AddAnimal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal VALUES(@animalName, @animalDescription, @animalCategory, @animalArea)";
        command.Parameters.AddWithValue("@animalName", animal.Name);
        command.Parameters.AddWithValue("@animalDescription", animal.Description);
        command.Parameters.AddWithValue("@animalCategory", animal.Category);
        command.Parameters.AddWithValue("@animalArea", animal.Area);
        command.ExecuteNonQuery();
    }

    public void DeleteAnimal(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "DELETE FROM Animal WHERE IdAnimal=@id";
        command.Parameters.AddWithValue("@id", id);
        
        command.ExecuteNonQuery();
    }

    public Animal EditAnimal(int id,[FromBody]Animal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "UPDATE Animal SET Name=@name,Description=@description,Category=@category,Area=@area WHERE IdAnimal=@id";
        
        command.Parameters.AddWithValue("@name", animal.Name);
        command.Parameters.AddWithValue("@description", animal.Description);
        command.Parameters.AddWithValue("@category", animal.Category);
        command.Parameters.AddWithValue("@area", animal.Area);
    
        command.ExecuteNonQuery();
        
        Animal updatedAnimal = GetAnimalById(id);
        return updatedAnimal;
    }
    
    public Animal GetAnimalById(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM Animal WHERE IdAnimal = @id";
        command.Parameters.AddWithValue("@id", id);
        
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
            int nameOrdinal = reader.GetOrdinal("Name");
            int descriptionOrdinal = reader.GetOrdinal("Description");
            int categoryOrdinal = reader.GetOrdinal("Category");
            int areaOrdinal = reader.GetOrdinal("Area");
        
            return new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(nameOrdinal),
                Description = reader.GetString(descriptionOrdinal),
                Category = reader.GetString(categoryOrdinal),
                Area = reader.GetString(areaOrdinal)
            };
        }
        else {return null; }
    }
   
}