using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addition Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        //this List<T> wil hold a series of error message strings
        //this List<T> will be used by MessageUserControl via the
        //  BusinessRuleException()
        private List<string> reasons = new List<string>();
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
            using (var context = new ChinookContext())
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
        //if you didnt want to pass back the newly created ID use void rather then datatype
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Album_Add(Album item)
        {
            using (var context = new ChinookContext())
            {
                //addition logic - treat this as validation
                if (CheckReleaseYear(item))
                {
                    item.ReleaseLabel = string.IsNullOrEmpty(item.ReleaseLabel) ?
                    null : item.ReleaseLabel;

                    context.Albums.Add(item); //Staging all our information is just sitting in memory
                    context.SaveChanges();    //actual commit to the database
                    return item.AlbumId;      //the instance now has the indentity pk value
                }
                else
                {
                    throw new BusinessRuleException("Validation error", reasons);
                }
            }

        }
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int Album_Update(Album item)
        {
            using (var context = new ChinookContext())
            {
                //addition logic - treat this as validation
                if (CheckReleaseYear(item))
                {
                    item.ReleaseLabel = string.IsNullOrEmpty(item.ReleaseLabel) ?
                    null : item.ReleaseLabel;

                    context.Entry(item).State = System.Data.Entity.EntityState.Modified; //Staging all our information is just sitting in memory
                    return context.SaveChanges();    //actual commit to the database

                }
                else
                {
                    throw new BusinessRuleException("Validation error", reasons);
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int Album_Delete(Album item)
        {
            return Album_Delete(item.AlbumId);
        }

            public int Album_Delete(int albumid)
        {
            using (var context = new ChinookContext())
            {
                var existing = context.Albums.Find(albumid);
                context.Albums.Remove(existing);
                return context.SaveChanges();
            }
        }



        #endregion
        #region Support
        private bool CheckReleaseYear(Album item)
        {
            bool isValid = true;
            int releaseyear;
            if (string.IsNullOrEmpty(item.ReleaseYear.ToString()))
            {
                isValid = false;
                reasons.Add("Release Year is Required.");
            }
            else if(!int.TryParse(item.ReleaseYear.ToString(), out releaseyear))
            {
                isValid = false;
                reasons.Add("Release Year must be a valid year number (YYYY).");
            }
            else if(releaseyear < 1950 || releaseyear > DateTime.Today.Year)
            {
                isValid = false;
                reasons.Add(string.Format("Release Year of {0} must be a valid between 1950 and the current year.", releaseyear));
            }
            return isValid;
        }
        #endregion
    }
}

