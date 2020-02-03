<Query Kind="Expression">
  <Connection>
    <ID>7f7ec588-2437-411a-b092-3f1debc0e04c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Aggregates

//.Count(), .Sum(), .Min(), .Max(), .Average()
//aggregates work against collections (0, 1 or more record datasets)

//list all albums showing the album title, album artists,
//and the number of tracks for the album

//Artists - > Albums -> Tracks
//ICollection(Album) -> ICollections -> ...
// <- Artist <- Album (child to parent)

//Method Syntax
from x in Albums
select new
{
	title = x.Title, 
	artist = x.Artist.Name,
	trackcount = x.Tracks.Count()
}
 //Aggregate using method syntax
from x in Albums
select new
{
	title = x.Title, 
	artist = x.Artist.Name,
	trackcount =(from y in x.Tracks 
						select y).Count()
}
 //Aggregate using query syntax and navigational property for
 //artist BUT NOT for Tracks
 //the navigational property down to the Tracks from Albums
 //	is replaced with a where clause comparing FKey to PKey
from x in Albums
select new
{
	title = x.Title, 
	artist = x.Artist.Name,
	trackcount =(from y in Tracks
						where y.AlbumId == x.AlbumId
						select y).Count()
}

//List the artists and their number of albums
//ordered the list from the most albums by an artists to least albums 
from x in Artists
orderby x.Albums.Count() descending, x.Name ascending //REALLY NO NEED TO WRITE ASCENDING AS IT IS THE DEFAULT VALUE
select new 
{
	name = x.Name,
	albums = x.Albums.Count()
}

//find the maximum number of albums for all artists
//methd 
Artists.Select( x => x.Albums.Count()).Max()

//produce a list of albums which have tracks showing their
//title, artist name, number of tracks and total price of 
//all tracks for that album
from x in Albums
where x.Tracks.Count() > 0
select new 
{
	title = x.Title,
	name = x.Artist.Name,
	trackcount = x.Tracks.Count(),
	tracktotal = x.Tracks.Sum(tr => tr.UnitPrice)
}

from x in Albums
where x.Tracks.Count() > 0
select new 
{
	title = x.Title,
	name = x.Artist.Name,
	trackcount = (from y in x.Tracks
					select y).Count(),
	tracktotal = (from y in Tracks 
					where y.AlbumId == x.AlbumId
					select y.UnitPrice).Sum()
}

//List all the playlist which have  track showing 
//the playlist name, number of tracks, 
from x in PlaylistTracks

select new
{
	title = x.Playlist.Name,
	totalTrack = x.Track.Count(),
	costPlayList = x.Track.Sum(tr => tr.UnitPrice)
}


