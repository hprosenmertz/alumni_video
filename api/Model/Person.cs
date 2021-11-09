using api.Data;
using api.Interfaces;

namespace api.Model
{
    public class Person
    {
        public int ID {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Major {get; set;}
        public string Minor {get; set;}
        public string PledgeClass {get; set;}
        public string GraduationSemester {get; set;}
        public string GradSchool {get; set;}
        public string GradSchoolName {get; set;}
        public string Employed {get; set;}
        public string Position {get; set;}
        public string Company {get; set;}
        public string City {get; set;}
        public string Email {get; set;}

        public IPersonDataHandler DataHandler {get; set;}   
            //use in order to do CRUD operations

        public Person(){
            DataHandler = new PersonDataHandler();
        }
    }
}