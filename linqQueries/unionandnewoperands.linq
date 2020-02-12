<Query Kind="Statements">
  <Connection>
    <ID>41bad139-d332-41fa-8f46-a6f48f14fabf</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Distinct Data

//List of countries in which we have customer.
var results1 = (from x in Customers
orderby x.Country
select x.Country).Distinct();
results1.Dump();

//.All() and .Any()

//.Any() method iterates through the entire collection to see
//	if any of the items match the specified condition
//	returns no data just a true or false
// an instance of the collection that receives a true on the condition is selected for processing

Genres.OrderBy(x => x.Name).Dump();


//Show Genre that have tracks which are not on any playlist
var results2 = from x in Genres
				where x.Tracks.Any(trck => trck.PlaylistTracks.Count() == 0)
				orderby x.Name
				select x.Name;
results2.Dump();

//.All() method iterates through the entire collection to see if all of the items match the specific condition
//returns no data just a true or false
//an instance of the collection that receives a true on the condition 
//		is selected for processing

//Show Genres that have all their tracks appearing at least once on a playlist

var populargenres = from x in Genres
					where x.Tracks.All(trck => trck.PlaylistTracks.Count() > 0)
					orderby x.Name
					select new
					{
						x.Name,
						thetracks = from y in x.Tracks
									where y.PlaylistTracks.Count() > 0
									select new 
										{
											song = y.Name,
											count = y.PlaylistTracks.Count()
										}
					};
populargenres.Dump();

/*
	Sometimes you have two collections that need to be compare usually you are looking for
	items that are the same (in both collections) OR you are looking for items that are different
	In either case you are comparing one collection to a second collection
*/
//list a
var almeida = (from x in PlaylistTracks
			   where x.Playlist.UserName.Contains("Almeida")
			   orderby x.Track.Name
			   select new 
			   {
			   	genre = x.Track.Genre.Name,
				id = x.TrackId,
				song = x.Track.Name
			   }).Distinct();
//almeida.Dump();
//list b
var brooks = (from x in PlaylistTracks
			   where x.Playlist.UserName.Contains("BrooksM")
			   orderby x.Track.Name
			   select new 
			   {
			   	genre = x.Track.Genre.Name,
				id = x.TrackId,
				song = x.Track.Name
			   }).Distinct();
//brooks.Dump();

//when you are comparing two collections, you need to determine which collections will
//be collection1 and which be collection2

//Show all the same tracks that both users listen to
var likes = almeida
.Where(a => brooks.Any(b => b.id == a.id))
.OrderBy(a => a.genre)
.Select(a => a)
.Dump();

//Show all the different tracks that both users listen to
var dislikes = almeida
.Where(a => !brooks.Any(b => b.id == a.id))
.OrderBy(a => a.genre)
.Select(a => a)
.Dump();

//Show the tracks that michellle has but not roberto
var MichellOnly = almeida
.Where(b => !brooks.Any(a => a.id == b.id))
.OrderBy(a => a.genre)
.Select(a => a)
.Dump();

//to concatentate two results from multiple queries 
//	you can use the .Union()
//This operates in the same fashion as the sql union command
//rules are quite similar between the two .Union() and union command
// number of columns same
//column datatype same
//ordering done on the last query 

//Create a list of Albums showing their title

//query1 will report albums that have tracks
//query will report albums without tracks (there is no tracks to sum or average)

//(query1).Union(query2).OrderBy(first sort).ThenBy(nth sort)
//Count is an integer 
//Sum() is a decimal (UnitPrice)
//Average() is q returned as a double (Milliseconds is an integer)

var unionresult = (from x in Albums
				   where x.Tracks.Count() > 0
				   select new
				   {
				 		title = x.Title,
						trackcount = x.Tracks.Count(),
						trackcost = x.Tracks.Sum( y => y.UnitPrice),
						avglength = x.Tracks.Average(y => y.Milliseconds)/1000.0
				   }).Union(
				   	from x in Albums
				    where x.Tracks.Count() == 0
				    select new
				    {
				 		title = x.Title,
						trackcount = 0,
						trackcost = 0.00m,
						avglength = 0.00
				    })
					.OrderBy (y => y.trackcount)
					.ThenBy(y => y.title).Dump();
					
					
unionresult.Dump();