using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.API.Enums;

namespace Explorer.Blog.Core.Converters
{
    public static class BlogReportConverter
    {
        public static ReportDto ToDto(this ReportDto report)
        {
            if (report == null)
            {
                return null;
            }

            return new ReportDto
            {
                UserId = (int)report.UserId,
                TimeCommentCreated = report.TimeCommentCreated,
                TimeReported = report.TimeReported,
                ReportAuthorId = (int)report.ReportAuthorId,
                ReportReason = (BlogEnums.ReportReason)report.ReportReason,
                IsReviewed = report.IsReviewed,
                BlogId = report.BlogId,
                Comment = report.Comment,
                IsAccepted = report.IsAccepted
            };
        }
        public static Report ToDomain(this ReportDto reportDto)
        {
            return reportDto == null ? null :
                new Report(reportDto.UserId, reportDto.TimeCommentCreated, reportDto.TimeReported, reportDto.ReportAuthorId, (BlogEnums.ReportReason)reportDto.ReportReason, reportDto.IsReviewed, reportDto.BlogId, reportDto.Comment, reportDto.IsAccepted);
        }
    }
    
}
