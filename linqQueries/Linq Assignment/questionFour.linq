<Query Kind="Program">
  <Connection>
    <ID>7959939c-8f4f-4533-8369-bacee83cefbf</ID>
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
	
	
	
	
	
	

	var weeklyScheduleResults = Shifts
								.Where (x => x.PlacementContract.Location.Name.Contains("Nait"))
								.GroupBy (x => x.DayOfWeek)
								.OrderBy (grpDay => grpDay.Key)
								.Select
								(
									grpDay => new
									{
										DayOfWeek =  (grpDay.Key == 1) ? "Mon" : (grpDay.Key == 2) ? "Tue" : (grpDay.Key == 3) ? "Wed" : (grpDay.Key == 4) ? "Thu" : "Fri",
										EmployeesNeeded = grpDay.Sum(z => z.NumberOfEmployees)
									}
								);
								
	weeklyScheduleResults.Dump();
	
}
// Define other methods and classes here