<Query Kind="Statements">
  <Connection>
    <ID>da94a474-8b96-4ece-a34b-98032898cf8f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Schedule</Database>
  </Connection>
</Query>

//List the names of all employees who did not work in November. Show the name in the format of "LastName, FirstName" and sort it by last name and then first name.
//put the month of november in a query

	

	var notWorkResults = Employees
					.Where(x => (x.Schedules.Count() == 0) && (x.Schedules.All( z => z.Day.Month == 11)))
					.OrderBy (x => x.LastName)
					.Select
						(
							x => new
							{
							Name = x.LastName + ", " +  x.FirstName
							}
						);
notWorkResults.Dump();