namespace MicroBloggingPlatform.Domain.Tests;

public class PostTests
{
    [Fact]
    public void MustCreatePost()
    {
        // Arrange
        var user = new User("user-1", new PersonName("User", null, "One"), "user.one@yolo.com", "userone");
        user.UpdateProfilePhoto("userone-photo.jpg");
        user.UpdateBio("You only live once");

        // Act
        var postPhotos = new List<string> { "post-photo-1.jpg", "post-photo-2.jpg" };
        var post = new Post("post-1", "user-1", postPhotos);

        // Assert
        Assert.Equal("post-1", post.Id);
        Assert.Equal("user-1", post.UserId);
        Assert.Equal(postPhotos, post.PhotoStorageUrls);
        Assert.Empty(post.Likes);
        Assert.Empty(post.Comments);
        Assert.Empty(post.Views);
        Assert.Empty(post.Reports);
    }

    [Fact]
    public void MustLikeAPost()
    {
        // Arrange
        var user1 = new User("user-1", new PersonName("User", null, "One"), "user.one@yolo.com", "userone");
        user1.UpdateProfilePhoto("userone-photo.jpg");
        user1.UpdateBio("You only live once");
        
        var postPhotos = new List<string> { "post-photo-1.jpg", "post-photo-2.jpg" };
        var post = new Post("post-1", "user-1", postPhotos);

        // Act
        var like = new Like("like-1", "post-1", "user-2");
        post.AddLike(like);
        
        // Assert
        Assert.Single(post.Likes);
    }
}