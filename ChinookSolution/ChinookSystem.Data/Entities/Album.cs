using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Data.Entities
{
    [Table("Albums")]
    public class Album
    {
        private string _ReleaseLabel;

        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Album Title is required")]
        [StringLength(160, ErrorMessage = "Artist Name can be no longer than 160 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ArtistId is required")]
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "Release Year is required")]
        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "Artist Name can be no longer than 50 characters.")]
        public string ReleaseLabel
        {
            get
            {
                return _ReleaseLabel;
            }
            set
            {
                //_ReleaseLabel = string.IsNullOrEmpty(value) ? null : value;
                if (string.IsNullOrEmpty(value))
                {
                    _ReleaseLabel = null;
                }
                else
                {
                    _ReleaseLabel = value;
                }
            }
        }

        public virtual Artist Artist { get; set; }

    }
}
