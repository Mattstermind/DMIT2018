using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addition Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        #region Queries
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Album Album_FindByID(int albumid)
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.Find(albumid);
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_FindByArtist(int artistid)
        {
            using(var context = new ChinookContext())
            {
                //simple example of a record set lookup via the foreign key
                // on a DbSet<T> using Linq
                var results = from albumrow in context.Albums
                              where albumrow.AlbumId == artistid
                              select albumrow;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_FindByTitle(string albumtitle)
        {
            using (var context = new ChinookContext())
            {
                //simple example of a record set lookup via the foreign key
                // on a DbSet<T> using Linq
                var results = from albumrow in context.Albums
                              where albumrow.Title.Contains(albumtitle)
                              select albumrow;
                return results.ToList();
            }
        }
        #endregion

        #region DML

        #endregion

        #region Support
        #endregion
    }
}
