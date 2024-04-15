public record User
{
    public string Id { get; private set; }
    
    public PersonName Name { get; private set; }
    
    public string Email { get; private set; }
    
    public string Username { get; private set; }
    
    public string Bio { get; private set; }
    
    public string ProfilePhotoUrl { get; private set; }
    
    public IReadOnlyList<User> Followings { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public DateTime UpdatedAt { get; private set; }
    
    public bool IsDeleted { get; private set; }
    
    public User(string id, PersonName name, string email, string username)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(username);
        
        Id = id;
        Name = name;
        Email = email;
        Username = username;
        Bio = string.Empty;
        ProfilePhotoUrl = string.Empty;
        Followings = new List<User>();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }
    
    public void UpdateBio(string bio)
    {
        ArgumentNullException.ThrowIfNull(bio);
        
        Bio = bio;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateProfilePhoto(string profilePhotoUrl)
    {
        ArgumentNullException.ThrowIfNull(profilePhotoUrl);
        
        ProfilePhotoUrl = profilePhotoUrl;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Follow(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (user.IsDeleted)
        {
            throw new InvalidOperationException("Deleted user cannot follow someone");
        }
        
        var existingFollowing = Followings.FirstOrDefault(followingUser => followingUser.Id == user.Id);
        if(existingFollowing != null)
        {
            return;
        }
        
        var updatedFollowings = Followings.ToList();
        updatedFollowings.Add(user);
        
        Followings = updatedFollowings;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Unfollow(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (user.IsDeleted)
        {
            throw new InvalidOperationException("Deleted user cannot unfollow someone");
        }
        
        var existingFollowing = Followings.FirstOrDefault(followingUser => followingUser.Id == user.Id);
        if(existingFollowing == null)
        {
            return;
        }
        
        var updatedFollowings = Followings.ToList();
        updatedFollowings.Remove(existingFollowing);
        
        Followings = updatedFollowings;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Restore()
    {
        IsDeleted = false;
        UpdatedAt = DateTime.UtcNow;
    }
}