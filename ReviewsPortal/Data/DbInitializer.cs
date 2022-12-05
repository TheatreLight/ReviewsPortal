using ReviewsPortal.Models;

namespace ReviewsPortal.Data
{
    public class DbInitializer
    {
        public static void Initialize(ReviewsPortalContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;
            }
            var users = new List<User>
            {
                new User { UserName = "Fedor", UserEmail = "fedor@mail.ru", Password = "111" },
                new User { UserName = "Igor", UserEmail = "igor@mail.ru", Password = "222" },
                new User { UserName = "Anna", UserEmail = "anna@mail.ru", Password = "333" }
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();

            var groups = new List<Group>
            {
                new Group {Name = "Movies"},
                new Group {Name = "Games"},
                new Group {Name = "Books"},
                new Group {Name = "Music"},
                new Group {Name = "Theatre"}
            };
            foreach (var group in groups)
            {
                context.Add(group);
            }
            context.SaveChanges();
            
            var reviews = new List<Review>
            {
                new Review {UserID = 1, GroupID = 1, ReviewTopic = "Avatar", ReviewText = "This is a good movie."},
                new Review {UserID = 2, GroupID = 2, ReviewTopic = "Ori", ReviewText = "I love this game!"},
                new Review {UserID = 3, GroupID = 3, ReviewTopic = "Clean Code", ReviewText = "Th very useful book for me."}
            };
            foreach (var review in reviews)
            {
                context.Add(review);
            }
            context.SaveChanges();

            var comments = new List<Comment>
            {
                new Comment {UserID = 1, ReviewID = 2, CommentText = "Me too."},
                new Comment {UserID = 3, ReviewID = 1, CommentText = "I agree."},
                new Comment {UserID = 2, ReviewID = 3, CommentText = "What a book"}
            };
            foreach (var comment in comments)
            {
                context.Add(comment);
            }
            context.SaveChanges();
        }
    }
}
