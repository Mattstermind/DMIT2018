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
	List all employees with multiple skills; ignore employees with only one skill. 
	Show the name of the employee and the list of their skillsets; for each skill, show the name of the skill, the level of competance and the years of experience. 
	Use the following text for the levels: 0 = Novice, 1 = Proficient, 2 = Expert.
	*/
	
	var employeeResult = Employees
							.Where (x => (x.EmployeeSkills.Count() > 1))
							.Select
							(
								x => new
								{
									Name = x.FirstName + " " + x.LastName,
									Skills = x.EmployeeSkills
											.Select
											(
												y => new skillList
												{
													Description = y.Skill.Description,
													Level = (y.Level == 0) ? "Novice" : (y.Level == 1) ? "Proficient" : "Expert",
													YearsExperience = y.YearsOfExperience
												}
											)									
								}
							);
	
employeeResult.Dump();
}
// Define other methods and classes here
public class skillList
{
	public string Description {get; set;}
	public string Level {get; set;}
	public int? YearsExperience {get; set;}
}
