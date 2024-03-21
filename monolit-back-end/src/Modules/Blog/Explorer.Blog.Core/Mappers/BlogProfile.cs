using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Enums;
using Explorer.Blog.Core.Domain;
using Vote = Explorer.Blog.Core.Domain.Vote;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {

        CreateMap<BlogDto, Domain.Blog>()
            .ForMember(dest => dest.Ratings, opt =>
                opt.MapFrom(src =>
                    src.Ratings.Select(x =>
                        new BlogRating(x.UserId, x.VotingDate, (Vote)x.Mark)
                        )))
            .ForMember(dest => dest.BlogComments, opt =>
                opt.MapFrom(src =>
                    src.BlogComments.Select(x =>
                        new BlogComment(x.UserId, x.BlogId, x.Comment, x.TimeCreated, x.TimeUpdated))))
            .ForMember(dest => dest.Reports, opt =>
                opt.MapFrom(src =>
                    src.Reports.Select(x =>
                        new Report(x.UserId, x.TimeCommentCreated, x.TimeReported, x.ReportAuthorId, (BlogEnums.ReportReason)x.ReportReason, x.IsReviewed, x.BlogId, x.Comment, x.IsAccepted))));

        CreateMap<Domain.Blog, BlogDto>()
            .ForMember(dest => dest.Ratings, opt =>
                opt.MapFrom(src =>
                    src.Ratings.Select(x =>
                        new BlogRatingDto
                        {
                            UserId = (int)x.UserId,
                            VotingDate = x.VotingDate,
                            Mark = (API.Dtos.Vote)x.Mark
                        }
                    )))
            .ForMember(dest => dest.BlogComments, opt =>
                opt.MapFrom(src =>
                    src.BlogComments.Select(x =>
                        new BlogCommentDto { UserId = (int)x.UserId, BlogId = x.BlogId, Comment = x.Comment, TimeCreated = x.TimeCreated, TimeUpdated = x.TimeUpdated })))
            .ForMember(dest => dest.Reports, opt =>
                opt.MapFrom(src =>
                    src.Reports.Select(x =>
                        new ReportDto
                        {
                            UserId = (int)x.UserId,
                            TimeCommentCreated = x.TimeCommentCreated,
                            TimeReported = x.TimeReported,
                            ReportAuthorId = (int)x.ReportAuthorId,
                            ReportReason = (BlogEnums.ReportReason)x.ReportReason,
                            IsReviewed = x.IsReviewed,
                            BlogId = x.BlogId,
                            Comment = x.Comment,
                            IsAccepted = x.IsAccepted
                        })));

    }
}