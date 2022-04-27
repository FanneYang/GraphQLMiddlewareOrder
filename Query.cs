using System.Linq;
using HotChocolate;
using HotChocolate.Types;

namespace FacebookPost
{
    public class Query
    {
        /// <summary>
        /// Gets all posts.
        /// </summary>
        [UsePaging]
        [CustomMiddleware(Domain = "Post")]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Post> GetPosts([Service] FacebookContext facebookContext) => facebookContext.Posts;


        /// <summary>
        /// Gets all users.
        /// </summary>
        [UsePaging]
        [CustomMiddleware(Domain = "Post")]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> GetUsers([Service] FacebookContext facebookContext) => facebookContext.Users;
    }
}
