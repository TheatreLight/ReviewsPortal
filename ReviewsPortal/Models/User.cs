namespace ReviewsPortal.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
