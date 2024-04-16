namespace MicroBloggingPlatform.Domain.Tests;

public class UserTests
{
    [Fact]
    public void MustCreateCompleteUser()
    {
        // Arrange and Act
        var user = new User("user-1", new PersonName("User", null, "One"), "user.one@yolo.com", "userone");
        user.UpdateProfilePhoto("userone-photo.jpg");
        user.UpdateBio("You only live once");
        
        // Assert
        Assert.Equal("user-1", user.Id);
        Assert.Equal("User", user.Name.FirstName);
        Assert.Null(user.Name.MiddleName);
        Assert.Equal("One", user.Name.LastName);
        Assert.Equal("user.one@yolo.com", user.Email);
        Assert.Equal("userone", user.Username);
        Assert.Equal("You only live once", user.Bio);
        Assert.Equal("userone-photo.jpg", user.ProfilePhotoUrl);
    }

    [Fact]
    public void MustFollowSomeone()
    {
        // Arrange
        var user1 = new User("user-1", new PersonName("User", null, "One"), "user.one@yolo.com", "userone");
        user1.UpdateProfilePhoto("userone-photo.jpg");
        user1.UpdateBio("You only live once");
        
        var user2 = new User("user-2", new PersonName("User", null, "Two"), "user.two@yolo.com", "usertwo");
        user2.UpdateProfilePhoto("usertwo-photo.jpg");
        user2.UpdateBio("You only live twice");
        
        // Act
        user1.Follow(user2);
        
        // Assert
        Assert.Single(user1.Followings);
        Assert.Equal("user-2", user1.Followings.First().Id);
    }

    [Fact]
    public void MustRemoveFollowing()
    {
        // Arrange
        var user1 = new User("user-1", new PersonName("User", null, "One"), "user.one@yolo.com", "userone");
        user1.UpdateProfilePhoto("userone-photo.jpg");
        user1.UpdateBio("You only live once");
        
        var user2 = new User("user-2", new PersonName("User", null, "Two"), "user.two@yolo.com", "usertwo");
        user2.UpdateProfilePhoto("usertwo-photo.jpg");
        user2.UpdateBio("You only live twice");
        
        user1.Follow(user2);

        // Act
        user1.Unfollow(user2);
        
        // Assert
        Assert.Empty(user1.Followings);
    }
}