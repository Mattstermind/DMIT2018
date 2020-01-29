<Query Kind="Statements">
  <Connection>
    <ID>41bad139-d332-41fa-8f46-a6f48f14fabf</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

var queryResults =  from x in Albums
					orderby x.ReleaseYear descending, x.Title
					select new
					{
					AlbumTitle = x.Title,
					Year = x.ReleaseYear,
					ArtistName = x.Artist.Name
					};


queryResults.Dump();
var methodResults = Albums
				   .OrderByDescending (x => x.ReleaseYear)
				   .ThenBy (x => x.Title)
				   .Select (
				      x => 
				         new  
				         {
				            AlbumTitle = x.Title, 
				            Year = x.ReleaseYear, 
				            ArtistName = x.Artist.Name
				         }
				   );
				   

methodResults.Dump();