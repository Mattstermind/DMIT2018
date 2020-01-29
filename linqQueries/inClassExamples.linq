<Query Kind="Program">
  <Connection>
    <ID>41bad139-d332-41fa-8f46-a6f48f14fabf</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	var queryResults =  from x in Albums
					orderby x.ReleaseYear descending, x.Title
					select new AlbumArtists
					{
					AlbumTitle = x.Title,
					Year = x.ReleaseYear,
					ArtistName = x.Artist.Name
					};


queryResults.Dump();
}

// Define other methods and classes here
public class AlbumArtists
{
	public string AlbumTitle {get;set;}
	public int Year {get;set;}
	public string ArtistName {get;set;}
}