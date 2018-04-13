namespace Forum.Data.Models
{
    public class Reply
    {
        public Reply()
        {
        }

        public Reply(string content, Post post, User author)
        {
            this.Content = content;
            this.Post = post;
            this.Author = author;
        }

        public Reply(string content, int postId, int authorId)
        {
            this.Content = content;
            this.PostId = postId;
            this.AuthorId = authorId;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}
