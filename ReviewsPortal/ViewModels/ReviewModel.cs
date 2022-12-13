namespace ReviewsPortal.ViewModels
{
    public class ReviewModel
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string? Topic { get; set; }
        public string? Text { get; set;}
    }
}
