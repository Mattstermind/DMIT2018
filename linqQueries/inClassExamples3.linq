<Query Kind="Expression">
  <Connection>
    <ID>41bad139-d332-41fa-8f46-a6f48f14fabf</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//in C# Expression, you must highlight the desired query
//	to excute IF you have more than one query int he file
//Query Syntax
from x in Albums
select x


//Method Syntax
Albums
   .Select (x => x)
   


from x in Albums
where x.ArtistId == 51
select x

Albums.Where(x => x.ArtistId == 51).Select(x => x)

Albums.Where(x => x.Artist.Name.Contains("a")).Select(x => x)

//list of albums and their artists
//show the album title, release year and artist name

from x in Albums
orderby x.ReleaseYear descending, x.Title
select new
{
AlbumTitle = x.Title,
Year = x.ReleaseYear,
ArtistName = x.Artist.Name
}

