namespace ReviewsPortal.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public int ReviewID { get; set; }
        public string CommentText { get; set; }
        public Review Review { get; set; }
        public User User { get; set; }
    }
}
