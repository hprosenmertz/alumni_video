using System.Dynamic;
using System.Collections.Generic;
using api.Interfaces;
using api.Model;

namespace api.Data
{
    public class PersonDataHandler : IPersonDataHandler
    {
        private Database db;

        public PersonDataHandler(){
            db = new Database();
        }
        public void Delete(Person person)
        {
            string sql = "UPDATE person SET deleted='Y' WHERE id=@id";
            

            var values = GetValues(person);
            db.Open();
            db.Update(sql, values);
            db.Close();
        }

        public void Insert(Person person)
        {
            var values = GetValues(person);

            string sql = "INSERT INTO person (first_name, last_name, major, minor, pledge_class, graduating_semester, grad_school, grad_school_name, employed, position, company, city, email) ";
            sql+= "VALUES(@firstName, @lastName, @major, @minor, @pledgeClass, @graduationSemester, @gradSchool, @gradSchoolName, @employed, @position, @company, @city, @email)";

            db.Open();
            db.Insert(sql, values);
            db.Close();
        }

        public List<Person> Select()
        {
            db.Open(); //open connection to database
            string sql = "SELECT * from person where deleted = 'N'";

            List<ExpandoObject> results = db.Select(sql);   //creates list of ExpandoObjects

            List<Person> people = new List<Person>(); //people list
            
            foreach(dynamic item in results){   //use dynamic for ExpandoObjects
                Person temp = new Person(){
                      ID = item.id,
                       FirstName = item.first_name,
                       LastName = item.last_name,
                       Major = item.major,
                       Minor = item.minor,
                       PledgeClass = item.pledge_class,
                       GraduationSemester = item.graduation_semester,
                       GradSchool = item.grad_school,
                       GradSchoolName = item.university,
                       Employed = item.employed,
                       Position = item.position,
                       Company = item.company,
                       City = item.city,
                       Email = item.email};

                    people.Add(temp);
            }
            db.Close();
            return people;
        }

        public void Update(Person person)
        {
        
            string sql = "UPDATE person SET first_name = @firstName, last_name = @lastName, major = @major, ";
            sql+= "minor =@minor, pledge_class=@pledgeClass, graduation_semester=@graduationSemester, ";
            sql+= "grad_school=@gradSchool, grad_school_name=@gradSchoolName, employed=@employed, ";
            sql+= "position=@position, company=@company, city=@city, email=@email ";
            sql+= "WHERE id = @id";

            var values = GetValues(person);
            db.Open();
            db.Update(sql, values);
            db.Close();
        }

        public Dictionary<string, object> GetValues(Person person){
            var values = new Dictionary<string, object>(){
                {"@id", person.ID},
                {"@firstName", person.FirstName},
                {"@lastName", person.LastName},
                {"@major", person.Major},
                {"@minor", person.Minor},
                {"@pledgeClass", person.PledgeClass},
                {"@graduatingSemester", person.GraduationSemester},
                {"@gradSchool", person.GradSchool},
                {"@gradSchoolName", person.GradSchoolName},
                {"@employed", person.Employed},
                {"@position", person.Position},
                {"@company", person.Company},
                {"@city", person.City},
                {"@email", person.Email}
            };
            return values;
        }
    }
}