namespace ReviewsPortal.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public string ReviewTopic { get; set;}
        public string ReviewText { get; set;}
        public List<Comment>? Comments { get; set;}
        public User User { get; set; }
        public Group Group { get; set; }
    }
}
