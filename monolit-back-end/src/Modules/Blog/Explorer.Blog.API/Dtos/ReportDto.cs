using Explorer.Blog.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class ReportDto
    {
        public int UserId { get; set; }
        public DateTime TimeCommentCreated { get; set; }
        public DateTime TimeReported { get; set; }
        public int ReportAuthorId { get; set; }
        public BlogEnums.ReportReason ReportReason { get; set; }
        public bool IsReviewed { get; set; } = false;
        public int BlogId { get; set; }
        public string Comment { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
