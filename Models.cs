
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacebookPost
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime JoinDate { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Interaction> Interactions { get; set; }
    }

    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
        public ICollection<Interaction> Interactions { get; set; }

    }

    public enum InteractionType
    {
        COMMENT,
        REPLY,
        FORWARD,
        LIKE,
        WOW,
        LOVE,
        HAHA,
        SAD,
        ANGRY
    }

    public class Interaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InteractionId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User InteractionUser { get; set; }
        public InteractionType InteractionType { get; set; }
        public string Content { get; set; }
        public DateTime InteractionTime { get; set; }
    }

}
