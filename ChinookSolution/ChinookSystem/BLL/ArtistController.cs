﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Addition Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    public class ArtistController
    {
        public List<Artist> Artist_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Artists.ToList();
            }
        }

        public Artist Artist_FindByID(int artistid)
        {
            using (var context = new ChinookContext())
            {
                return context.Artists.Find(artistid);
            }
        }
    }
}
