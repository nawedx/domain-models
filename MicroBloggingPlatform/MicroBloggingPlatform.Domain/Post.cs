public class Post
{
    public string Id { get; private set; }
    
    public string UserId { get; private set; }

    public IReadOnlyList<string> PhotoStorageUrls { get; private set; }
    
    public string Caption { get; private set; }
    
    public IReadOnlyList<Like> Likes { get; private set; }
    
    public IReadOnlyList<Comment> Comments { get; private set; }
    
    public IReadOnlyList<View> Views { get; private set; }
    
    public IReadOnlyList<Report> Reports { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public DateTime UpdatedAt { get; private set; }

    public Post(string id, string userId, IReadOnlyList<string> photoStorageUrls, string caption)
    {
        Id = id;
        UserId = userId;
        PhotoStorageUrls = photoStorageUrls.Any() ? photoStorageUrls : throw new ArgumentException("At least one photo is required");
        Caption = caption;
        Likes = new List<Like>();
        Comments = new List<Comment>();
        Views = new List<View>();
        Reports = new List<Report>();
        CreatedAt = DateTime.UtcNow;
    }
    
    public void UpdatePhotos(IReadOnlyList<string> photoStorageUrls)
    {
        ArgumentNullException.ThrowIfNull(photoStorageUrls);
        if (photoStorageUrls.Count == 0)
        {
            throw new ArgumentException("At least one photo is required");
        }
        
        PhotoStorageUrls = photoStorageUrls;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateCaption(string caption)
    {
        ArgumentNullException.ThrowIfNull(caption);
        
        Caption = caption;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void AddLike(Like like)
    {
        ArgumentNullException.ThrowIfNull(like);
        
        var existingLike = Likes.FirstOrDefault(l => l.UserId == like.UserId);
        if (existingLike != null)
        {
            return;
        }
        
        var updatedLikes = Likes.ToList();
        updatedLikes.Add(like);
        
        Likes = updatedLikes;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void RemoveLike(string userId)
    {
        ArgumentNullException.ThrowIfNull(userId);
        
        var existingLike = Likes.FirstOrDefault(l => l.UserId == userId);
        if (existingLike == null)
        {
            return;
        }
        
        var updatedLikes = Likes.ToList();
        updatedLikes.Remove(existingLike);
        
        Likes = updatedLikes;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void AddComment(Comment comment)
    {
        ArgumentNullException.ThrowIfNull(comment);
        
        var newComments = Comments.ToList();
        newComments.Add(comment);
        
        Comments = newComments;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void AddView(View view)
    {
        ArgumentNullException.ThrowIfNull(view);
        
        if(view.UserId == UserId)
        {
            return;
        }
        
        var newViews = Views.ToList();
        newViews.Add(view);
        
        Views = newViews;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void RemoveComment(string commentId)
    {
        ArgumentNullException.ThrowIfNull(commentId);
        
        var existingComment = Comments.FirstOrDefault(c => c.Id == commentId);
        if (existingComment == null)
        {
            return;
        }
        
        var newComments = Comments.ToList();
        newComments.Remove(existingComment);
        
        Comments = newComments;
        UpdatedAt = DateTime.UtcNow;
    }
}