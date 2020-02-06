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
		From the shifts scheduled for NAIT's placement contracts, show the number of employees needed for each day (ordered by day-of-week). 
		Display the name of the day of week (Sunday, as the first day of the week, is number zero) and the number of employees needed.
	*/
	
//	DateTime startDate = StartOfWeek(DayOfWeek.Monday);
//	DateTime endDate = startDate.AddDays(6);

	var weeklyScheduleResults = 
	
								Shifts
								.Where ( x=> (x.DayOfWeek != 0 && x.DayOfWeek != 6))
								.OrderBy (x => x.DayOfWeek)
								.Select
								(
									x => new
									{
										DayOfWeek = (x.DayOfWeek == 1) ? "Mon" : (x.DayOfWeek == 2) ? "Tue" : (x.DayOfWeek == 3) ? "Wed" : (x.DayOfWeek == 4) ? "Thu" : "Fri", 
										NumberOfEmployees = x.Schedules.Count()
									}
								);
								
	weeklyScheduleResults.Dump();
	
}
// Define other methods and classes here
