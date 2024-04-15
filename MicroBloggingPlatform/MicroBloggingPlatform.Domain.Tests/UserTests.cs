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
    public void MustAddFollowing()
    {
        // Arrange
        var user = new User("user-1", new PersonName("User", null, "One"), "user.one@yolo.com", "userone");
        user.UpdateProfilePhoto("userone-photo.jpg");
        user.UpdateBio("You only live once");
        
        var followingUser = new User("following-1", new PersonName("Following", null, "One"), "following.one@yolo.com", 
            "followingone");
        followingUser.UpdateProfilePhoto("followingone-photo.jpg");
        followingUser.UpdateBio("You live to get followed");
        
        // Act
        user.Follow(followingUser);
        
        // Assert
        Assert.Single(user.Followings);
        Assert.Equal("following-1", user.Followings.First().Id);
    }

    [Fact]
    public void MustRemoveFollowing()
    {
        // Arrange
        var user = new User("user-1", new PersonName("User", null, "One"), "user.one@yolo.com", "userone");
        user.UpdateProfilePhoto("userone-photo.jpg");
        user.UpdateBio("You only live once");
        
        var followingUser = new User("following-1", new PersonName("Following", null, "One"), "following.one@yolo.com", 
            "followingone");
        followingUser.UpdateProfilePhoto("followingone-photo.jpg");
        followingUser.UpdateBio("You live to get followed");
        
        user.Follow(followingUser);

        // Act
        user.Unfollow(followingUser);
        
        // Assert
        Assert.Empty(user.Followings);
    }
}