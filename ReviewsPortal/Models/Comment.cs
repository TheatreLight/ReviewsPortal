namespace ReviewsPortal.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int? UserID { get; set; }
        public int? ReviewID { get; set; }
        public string CommentText { get; set; }
        public virtual Review Review { get; set; }
        public virtual User User { get; set; }
    }
}
