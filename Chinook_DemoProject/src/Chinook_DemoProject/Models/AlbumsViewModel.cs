
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook_DemoProject.Models
{
    public class AlbumsViewModel
    {
        public ICollection<Album> Albums { get; set; }
        public SelectList AlbumList { get; set; }

        public int? SelectedAlbumId { get; set; }
    }
}
