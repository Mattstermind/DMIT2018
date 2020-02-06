<Query Kind="Expression">
  <Connection>
    <ID>7eb36c45-f45a-48bd-b8de-f61d5f6d7965</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Schedule</Database>
  </Connection>
</Query>

//1. List all the skills for which we do not have any qualfied employees.
from x in Skills
where x.EmployeeSkills.Count() < 1
select new
{
	name = x.Description,
}
//method 
Skills
   .Where (x => (x.EmployeeSkills.Count () < 1))
   .Select (
      x => 
         new  
         {
            name = x.Description
         }
   )
   
/* Question 2
Show all skills requiring a ticket and which employees have those skills. Include all the data as seen in the 
following image. Order the employees by years of experience (highest to lowest). Use the following text for the 
levels: 0 = Novice, 1 = Proficient, 2 = Expert. (Hint: Use nested ternary operators to handle the levels as text.)
*/
Skills
 .Where (x => (x.RequiresTicket == true))
	.OrderByDescending(x => x.Description)
	.Select(
		x =>
		new
		{
			skilldes = x.Description,
			name = x.EmployeeSkills.Employee.LastName + ", " + x.EmployeeSkills.Employee.FirstName,
			level = (x.EmployeeSkills.Level == 1) ? "Novice" :  (x.EmployeeSkills.Level == 2) ? "Proficient" : "Expert",
			yoe = x.EmployeeSkills.YearsOfExperience
		}
	)
// Class to define our search
public class Employee
{
	public string skilldes {get;set;}
	public string name {get;set;}
	public string level {get;set;}
	public string yoe {get;set;}
}


Artists
   .OrderByDescending (x => x.Albums.Count ())
   .ThenBy (x => x.Name)
   .Select (
      x => 
         new  
         {
            name = x.Name, 
            albums = x.Albums.Count ()
         }
   )

