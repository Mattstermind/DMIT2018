<Query Kind="Program">
  <Connection>
    <ID>7959939c-8f4f-4533-8369-bacee83cefbf</ID>
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
	
	var startDate = new DateTime(2019, 12, 22);
	var endDate = new DateTime(2019, 12, 28);
//x.DayOfWeek != 0 && x.DayOfWeek != 6
	var weeklyScheduleResults = Shifts
								.Where ( x=> (x.Schedules.Any(z => z.Day > startDate && z.Day < endDate)))
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