using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;

namespace FacebookPost
{
    public class Mutation
    {
        public async Task<Interaction> Interaction([Service] FacebookContext facebookContext, 
            int postId, int userId, InteractionType interactionType, string content, DateTime dateTime)
        {
            var id = facebookContext.Interactions.Any() ? facebookContext.Interactions.Max(i => i.InteractionId) :0;
            var user = facebookContext.Users.Find(userId);
            var post = facebookContext.Posts.Find(postId);
            var interaction = new Interaction
            {
                InteractionId = id + 1,
                PostId = postId,
                UserId = userId,
                InteractionType = interactionType,
                Content = content,
                InteractionTime = dateTime
            };

            if (user.Interactions is null) 
            {
                user.Interactions = new List<Interaction>();
            }
            if (post.Interactions is null)
            {
                post.Interactions = new List<Interaction>();
            }
            user.Interactions.Add(interaction);
            post.Interactions.Add(interaction);

            facebookContext.Users.Update(user);
            facebookContext.Posts.Update(post);
            facebookContext.Interactions.Add(interaction);

            await facebookContext.SaveChangesAsync();

            return interaction;
        }

        public async Task<Post> Post([Service] FacebookContext facebookContext,
            int authorId, string content, DateTime dateTime)
        {
            var id = facebookContext.Posts.Any() ? facebookContext.Posts.Max(i => i.PostId) : 0;
            var author = facebookContext.Users.Find(authorId);
            var post = new Post
            {
                PostId = id + 1,
                AuthorId = authorId,
                Author = author,
                Content = content,
                PostTime = dateTime,
                Interactions = null
            };
            facebookContext.Posts.Add(post);

            await facebookContext.SaveChangesAsync();

            return post;
        }

        public async Task<User> User([Service] FacebookContext facebookContext,
            string firstName, string lastName, DateTime joinDate)
        {
            var id = facebookContext.Users.Any() ? facebookContext.Users.Max(i => i.UserId) : 0;
            var user = new User
            {
                UserId = id + 1,
                FirstName = firstName,
                LastName = lastName,
                JoinDate = joinDate,
                Posts = null,
                Interactions = null
            };
            facebookContext.Users.Add(user);

            await facebookContext.SaveChangesAsync();

            return user;
        }
    }
}
