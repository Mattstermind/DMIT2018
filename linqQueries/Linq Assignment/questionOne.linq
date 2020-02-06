<Query Kind="Expression">
  <Connection>
    <ID>7eb36c45-f45a-48bd-b8de-f61d5f6d7965</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Schedule</Database>
  </Connection>
</Query>

//1. List all the skills for which we do not have any qualfied employees.
//method 
Skills
   .Where (x => (x.EmployeeSkills.Count () < 1))
   .Select (
      x => 
         new  
         {
             x.Description
         }
   )