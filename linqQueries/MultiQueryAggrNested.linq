<Query Kind="Program">
  <Connection>
    <ID>41bad139-d332-41fa-8f46-a6f48f14fabf</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

/* Nested Queries
	A query within a query
	The query is returned as an IEnumerable<T> or IQueryable<T>
	If you need to return your query as a List<T> then you must
	encapsulate your query and add .ToList9) on the sub query
		(from...).ToList()
	ToList() is also useful if you require your data in memory for
	some execution
	
	QUESTION - CREATE A LIST OF ALBUMS DISPLAYING THE TITLE, ARTIST.
			   DISPLAY ONLY ALBUMS WITH 15 OR MORE TRACKS.
			   DISPLAY THE SONGS ON THE ALBUM (NAME AND LENGTH)
*/

void Main()
{
	var results = from x in Albums
				where x.Tracks.Count() >= 15
				select new AlbumList
				{
					AlbumTitle = x.Title,
					ArtistName = x.Artist.Name,
					Song = (from y in x.Tracks
								select new Song
								{
									SongName = y.Name,
									SongLength = y.Milliseconds
								}).ToList()
				};
results.Dump();

/*
	Create a list of PlayList with more than 15 tracks. 
	Show the playlist name, count of tracks and the list of tracks
	For each track show the song name and Genre
*/

var test = from x in Playlists
where x.PlaylistTracks.Count() > 15
select new  Playlist
				{
					PlaylistTitle = x.Name,
					AlbumArtistName = x.PlaylistTracks.Count(),
					PlayTime = x.PlaylistTracks.Sum(plt => plt.Track.Milliseconds),
					TracksList = (from y in x.PlaylistTracks
								select new Track
								{
									PlayListName = y.Track.Name,
									PlayListLength = y.Track.Genre.Name
								}).ToList()
				};
test.Dump();
}


//Define other methods and classes here

//Class that contains the album title, album artist, list of tracks 
public class AlbumList// (DTO)
{
	public string AlbumTitle {get;set;}
	public string ArtistName {get;set;}
	public List<Song> Song {get;set;}
}

public class Song//POCO
{
	public string SongName {get;set;}
	public int SongLength {get;set;}
}

public class Playlist// (DTO)
{
	public string PlaylistTitle {get;set;}
	public int AlbumArtistName {get;set;}
	public int PlayTime {get;set;}
	public List<Track> TracksList {get;set;}
}

public class Track//POCO
{
	public string PlayListName {get;set;}
	public string PlayListLength {get;set;}
}
