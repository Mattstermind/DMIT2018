<Query Kind="Program">
  <Connection>
    <ID>7eb36c45-f45a-48bd-b8de-f61d5f6d7965</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Schedule</Database>
  </Connection>
</Query>

void Main()
{
	/*
	Show all skills requiring a ticket and which employees have those skills. Include all the data as seen in the following image. 
	Order the employees by years of experience (highest to lowest). Use the following text for the levels: 0 = Novice, 1 = Proficient, 2 = Expert.
	(Hint: Use nested ternary operators to handle the levels as text.)
	*/
var skillResult =   Skills
					   .Where (x => (x.RequiresTicket == true))
					   .Select (
					      x => 
					         new  
					         {
					            Description = x.Description, 
					            Employees = x.EmployeeSkills.OrderByDescending (y => y.YearsOfExperience)
					               .Select (
					                  y => 
					                     new EmployeesList()
					                     {
					                        Name = ((y.Employee.FirstName + " ") + y.Employee.LastName), 
					                        Level = (y.Level == 0) ? "Novice" : (y.Level == 1) ? "Proficient" : "Expert", 
					                        YearExperience = y.YearsOfExperience
					                     }
					               )
					         }
					   );		   
skillResult.Dump();
}
// Define other methods and classes here
public class EmployeesList
{
	public string Name {get; set;}
	public string Level {get; set;}
	public int? YearExperience {get; set;}
}