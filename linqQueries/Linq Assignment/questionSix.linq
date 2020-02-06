<Query Kind="Program">
  <Connection>
    <ID>7eb36c45-f45a-48bd-b8de-f61d5f6d7965</ID>
    <Server>.</Server>
    <Database>Schedule</Database>
  </Connection>
</Query>

void Main()
{
	//List the names of all employees who did not work in November. Show the name in the format of "LastName, FirstName" and sort it by last name and then first name.
	//we need to encapsulate the Month:November
	//do a schedule count() where count is equal == 0
	DateTime monthNovember = new DateTime(2020, 11, 1)
	int monthPlaceHolder = monthNovember.Month;
	var notWorkResults = Employees
						.Where(x =>(x.Schedules.Day.Month == monthPlaceHolder))
						.Select
							(
								x => new
								{
									Name = x.LastName + " " +  x.FirstName,
								}
							);
	notWorkResults.Dump();
}


// Define other methods and classes here
public DateTime[] November()
{
DateTime monthNovember = new DateTime(2020, 11, 1)
	for(x = 0; x < 30; x++)
	{
	
	}
}