public record User
{
    public string Id { get; private set; }
    
    public PersonName Name { get; private set; }
    
    public string Email { get; private set; }
    
    public string Username { get; private set; }
    
    public string Bio { get; private set; }
    
    public string ProfilePhotoUrl { get; private set; }
    
    public IReadOnlyList<User> Followers { get; private set; }
    
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
        
        var existingFollower = Followers.FirstOrDefault(follower => follower.Id == user.Id);
        if(existingFollower != null)
        {
            return;
        }
        
        var updatedFollowers = Followers.ToList();
        updatedFollowers.Add(user);
        
        Followers = updatedFollowers;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Unfollow(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (user.IsDeleted)
        {
            throw new InvalidOperationException("Deleted user cannot unfollow someone");
        }
        
        var existingFollower = Followers.FirstOrDefault(follower => follower.Id == user.Id);
        if(existingFollower == null)
        {
            return;
        }
        
        var updatedFollowers = Followers.ToList();
        updatedFollowers.Remove(existingFollower);
        
        Followers = updatedFollowers;
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