namespace Explorer.Blog.API.Enums
{
    public class BlogEnums
    {
        public enum BlogStatus
        {
            Draft = 1,
            Published,
            Closed,
            Active, 
            Famous
        }

        public enum ReportReason
        {
            Spam = 1,
            HateSpeech,
            FalseInfo,
            Bullying,
            Violence
        }
    }
}
