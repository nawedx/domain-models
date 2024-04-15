public record Comment
{
    public string Id { get; private set; }
    
    public string PostId { get; private set; }
    
    public string UserId { get; private set; }
    
    public string Content { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public Comment(string id, string postId, string userId, string content)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(postId);
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(content);
        
        Id = id;
        PostId = postId;
        UserId = userId;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }
}