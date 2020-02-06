<Query Kind="Statements">
  <Connection>
    <ID>7eb36c45-f45a-48bd-b8de-f61d5f6d7965</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Schedule</Database>
  </Connection>
</Query>

//List all the employees with the most years of experience
//var resultSum = EmployeeSkills.Max( x => x.YearsOfExperience);

int ?mostExperience = (from y in Employees
                           select y.EmployeeSkills.Sum( x => x.YearsOfExperience)).Max();
						   
mostExperience.Dump();
var mostExperienceResults = Employees
							.Where(x => (x.EmployeeSkills.YearsOfExperience ==  mostExperience))
							.Select
							(
								x => new
								{
									Name = x.FirstName + " " + x.LastName,
									YOE = x.EmployeeSkills.Sum(y => y.YearsOfExperience)	
								}
							);
mostExperienceResults.Dump();