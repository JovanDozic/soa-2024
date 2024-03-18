using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PreferenceDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Difficulty { get; set; }
        public string Transport { get; set; }
        public string Tags { get; set; }
    }
}
