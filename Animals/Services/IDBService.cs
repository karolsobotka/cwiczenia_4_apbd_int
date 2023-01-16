using Animals.Model;
using System.Collections.Generic;

namespace Animals
{
    public interface IDBService
    {
        List<Animal> GetAnimals(string orderBy);
        void CreateAnimal(Animal animal);
        void ChangeAnimal(int idAnimal, Animal animal);
        void DeleteAnimal(int idAnimal);
    }
}