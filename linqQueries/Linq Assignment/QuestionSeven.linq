<Query Kind="Statements">
  <Connection>
    <ID>62427a96-4259-46eb-ba28-71d6bc4b97b8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Schedule</Database>
  </Connection>
</Query>

/*
For the month of January 2020, list the total earnings per employee along with the number of shifts, the regular earnings, and overtime earnings.

Note 1: Remember that handling DateTime and TimeSpan calculations is best done in-memory; therefore, you should use a .ToList() 
in your linq's from clause so that the linq query is not converted to SQL.

Note 2: When doing your earnings calculations, remember that it's permissible to use method syntax inside of your linq query syntax.
*/

var result = Schedules
		   .Where (x => ((x.Day.Year == 2020) && (x.Day.Month == 1)))
		   .GroupBy (x => x.Employee)
		   .Select
		   (
		        payGroup => new  
				 {
				 Name = payGroup.Key.FirstName + " " + payGroup.Key.LastName,
				 RegularEarning = String.Format("{0:0.00}", payGroup.Where(rpay => !rpay.OverTime).Sum(s => s.HourlyWage * (s.Shift.EndTime.Hours - s.Shift.StartTime.Hours))),
				 OverTime =  String.Format("{0:0.00}", payGroup.All(rpay => !rpay.OverTime)? 0.00m :  payGroup.Where(rpay => rpay.OverTime).Sum(s => (s.HourlyWage * 1.5m)*  (s.Shift.EndTime.Hours - s.Shift.StartTime.Hours))),
				 NumberOfShifts = payGroup.Count()
				 }
		   );
  result.Dump();
   
   //.ToString(0:00)