using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public enum Vote { PLUS = 1, MINUS }
    public class BlogRatingDto
    {
        public int UserId { get; set; }
        public DateTime VotingDate { get; set; }
        public Vote Mark { get; set; }
        public string BlogId { get; set; }
    }
}
