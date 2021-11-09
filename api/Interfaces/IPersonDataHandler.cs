using System.Collections.Generic;
using api.Model;

namespace api.Interfaces
{
    public interface IPersonDataHandler
    {
         //used to handle anything with Person data
         //CRUD operations (each will have a method)

          public List<Person> Select();

         public void Delete(Person person); //going to need id to delete, can pass in person and pull id from there
         public void Insert(Person person);
         public void Update(Person person);
    }
}