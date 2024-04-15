public record View
{
    public string Id { get; private set; }
    
    public string PostId { get; private set; }
    
    public string UserId { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public View(string id, string postId, string userId)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(postId);
        ArgumentNullException.ThrowIfNull(userId);
        
        Id = id;
        PostId = postId;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
}