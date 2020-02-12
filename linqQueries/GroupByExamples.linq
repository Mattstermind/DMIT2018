<Query Kind="Expression">
  <Connection>
    <ID>41bad139-d332-41fa-8f46-a6f48f14fabf</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

/*
Grouping

Basically, grouping is the technique of placing a large pile of data
	into small piles of data depending on criteria 
	
	Navigation properties allow for natural grouping of parent to child
		collections
			ex, tablerow.childnavproperty.Count() counts all the child
			records associated with the parent instance
			
	ALERT!! WHAT IF THERE IS NO NAVIGATIONAL PROPERTY FOR THE GROUPING OF THE 
			DATA COLLECTION !!
			
	here you can use the group clause to create a set of smaller collections based
		on the desired criteria
		
	It is important to remember that once the small groups are create, ALL reporting MUST
	use the small groups as the collection reference
*/

//report albums by ReleaseYear
from x in Albums
group x by x.ReleaseYear into yearGroups
select yearGroups

//parts of the group
// key component
// instance component 

//the key component is created by the "by" criteria
//the "by" criteria can be a 
	//A) A single attribute
	//B) A class
	//C) A list of attributes
	
//Where and orderby clauses can be executed against the group key component or group field
//you can filter(where) or order before grouping
//orderby before grouping is basically useless

//report albums by ReleaseYear showing the year and number of
//	albums for that year. Order by the year with the most
// 	albums, then by the year.

from x in Albums
group x by x.ReleaseYear into yearGroup
orderby yearGroup.Count() descending, yearGroup.Key ascending
select new
{
	year = yearGroup.Key,
	albumcount = yearGroup.Count(),
}

//report albums by ReleaseYear showing the year and number of
//	albums for that year. Order by the year with the most
// 	albums, then by the year. Report the album title, artist name,
//  and number of album tracks

from x in Albums
group x by x.ReleaseYear into yearGroup
orderby yearGroup.Count() descending, yearGroup.Key ascending
select new
{
	year = yearGroup.Key,
	albumcount = yearGroup.Count(),
	albumandartist = from gr in yearGroup
					 select new
					 {
						name = gr.Title,
						artistName = gr.Artist.Name,
						TrackCount = gr.Tracks.Count(trk => trk.AlbumId == gr.AlbumId )
					 }
}


//report albums by ReleaseYear showing the year and number of
//	albums for that year. Order by the year with the most
// 	albums, then by the year. Report the album title, artist name,
//  and number of album tracks. Report Only albums of the 70s. 80s and 90s

from x in Albums
where x.ReleaseYear > 1969 && x.ReleaseYear < 2000
group x by x.ReleaseYear into yearGroup
orderby yearGroup.Count() descending, yearGroup.Key ascending
select new
{
	year = yearGroup.Key,
	albumcount = yearGroup.Count(),
	albumandartist = from gr in yearGroup
					 select new
					 {
						name = gr.Title,
						artistName = gr.Artist.Name,
						TrackCount = gr.Tracks.Count(trk => trk.AlbumId == gr.AlbumId )
					 }
}

//Note commenting in LinqPad
// comment ctrl + k + c
// uncomment is ctrl + k + u
//Grouping can be done on entity attributes deteremined using a navigational property 
//List tracks for albums produced after 2010 by Genre name. Count tracks for the Name.
from trk in Tracks
where trk.Album.ReleaseYear > 2010
group trk by trk.Genre.Name into gTemp
//select gTemp
select new 
{
	genre = gTemp.Key,
	numberOf = gTemp.Count()
}

//same report but using the entity as the group criteria 
//when you have multiple attributes in a group key
// you MUST reference the attribute
from trk in Tracks
where trk.Album.ReleaseYear > 2010
group trk by trk.Genre into gTemp
orderby gTemp.Key.Name 
//select gTemp
select new 
{
	genre = gTemp.Key.Name,
	numberOf = gTemp.Count()
}

//Using group techniques. Create a list of customers by employee support individual 
//showing the employee lastname, firstname, thenumber of customers
//for this employee, and a customer list for the employee by state, city and customer name (Lname, Fname)
from x in Customers
group x by x.SupportRepIdEmployee into empGrp
select new
{
	employee = empGrp.Key.LastName + ", " + empGrp.Key.FirstName + "(" + empGrp.Key.Phone + ")",
	customercount = empGrp.Count(),
	customerlist = from clst in empGrp
					orderby clst.State, clst.City, clst.LastName
					 select new
					 {
						state = clst.State,
						city = clst.City,
						custname = clst.LastName +", "+ clst.FirstName
					 }
}

//grouping on multiple attributes NOT a defined class
from c in Customers
group c by new {c.Country, c.State} into gRed
select gRed