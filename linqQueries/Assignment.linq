<Query Kind="Expression">
  <Connection>
    <ID>7742a1a8-cfbf-493f-b9fd-4bc9a806013a</ID>
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
	x.Description
}

Skills
   .Where (x => (x.EmployeeSkills.Count () < 1))
   .Select (
      x => 
         new  
         {
             x.Description
         }
   )
   
/* Question 2
Show all skills requiring a ticket and which employees have those skills. Include all the data as seen in the following image. Order the employees by years of experience 
(highest to lowest). Use the following text for the levels: 0 = Novice, 1 = Proficient, 2 = Expert. (Hint: Use nested ternary operators to handle the levels as text.)
*/
from x in Skills
where x.RequiresTicket == true
select new 
{
 Description = x.Description,
 Employee = (from y in Employees 
			select y.FirstName + y.LastName,
				   (y.EmployeeSkills.Level == 0) ? "Novice" : (y.EmployeeSkills.Level == 1) ?  "Proficient" : "Expert",
				   y.EmployeeSkills.YearsOfExperience)
}

