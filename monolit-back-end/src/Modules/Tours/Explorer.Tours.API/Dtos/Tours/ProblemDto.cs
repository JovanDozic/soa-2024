using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.Tours
{
    public class ProblemDto
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public bool Priority { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public long TourId { get; set; }
        public int TouristId { get; set; }
        public string AuthorsSolution { get; set; }
        public bool IsSolved { get; set; }
        public string UnsolvedProblemComment { get; set; }
        public DateTime Deadline { get; set; }
    }
}