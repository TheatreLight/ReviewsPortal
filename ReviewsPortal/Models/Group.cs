﻿namespace ReviewsPortal.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
