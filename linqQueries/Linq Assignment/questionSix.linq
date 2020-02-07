<Query Kind="Program">
  <Connection>
    <ID>da94a474-8b96-4ece-a34b-98032898cf8f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Schedule</Database>
  </Connection>
</Query>

void Main()
{
	//List the names of all employees who did not work in November. Show the name in the format of "LastName, FirstName" and sort it by last name and then first name.
	var notWorkResults = Schedules
						.Where(x =>(x.Day.Month == 11))
						.Select
							(
								x => new
								{
									Name = x.Employee.LastName + ", " +  x.Employee.FirstName,
								}
							).Distinct();
	notWorkResults.Dump();
}


// Define other methods and classes here
