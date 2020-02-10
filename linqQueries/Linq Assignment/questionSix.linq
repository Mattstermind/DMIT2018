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
	//List the names of all employees who did not work in November. Show the name in the format of "LastName, FirstName" and sort it by last name and then first name.
	//put the month of november in a query
	
		
	
		var notWorkResults = Employees
						.Where(x => (x.Schedules.Count() == 0) && x.Schedules(y => y.Day.Mpnth == 11) )
						.Select
							(
								x => new
								{
								Name = x.Schedules
											.Where(y => y.Day.Month == 11 && (x.Schedules.Count() == 0))
											.Select
											(
												y => new
												{
													Name = x.LastName + ", " +  x.FirstName,
												}
											)
								}
							);
	notWorkResults.Dump();
	

}


// Define other methods and classes here