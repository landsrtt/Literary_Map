using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Literary_Map.Models
{
    public class FavoriteBooks

    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
