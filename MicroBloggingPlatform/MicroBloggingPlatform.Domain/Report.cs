public record Report
{
    public string Id { get; private set; }
    
    public string PostId { get; private set; }
    
    public string UserId { get; private set; }
    
    public string Reason { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public Report(string id, string postId, string userId, string reason)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(postId);
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(reason);
        
        Id = id;
        PostId = postId;
        UserId = userId;
        Reason = reason;
        CreatedAt = DateTime.UtcNow;
    }
}