namespace LibraryManagementTests;

public class LibraryTests
{
    private readonly BookInfo _book;
    private readonly UserInfo _user;

    private record BookInfo(string Id, string Isbn, string Title);

    private record UserInfo(string Id, string Name);

    public LibraryTests()
    {
        _book = new BookInfo("library-book-1", "ISBN-100", "Programming for Dummies");
        _user = new UserInfo("library-user-1", "User One");
    }
    
    [Fact]
    public void MustCreateLibraryManager()
    {
        var library = new Library();
        Assert.Empty(library.Books);
        Assert.Empty(library.Users);
    }

    [Fact]
    public void MustGenerateValidBookId()
    {
        var library = new Library();
        
        Assert.Equal("library-book-1", library.GenerateBookId());

        library.SeedData();
        
        Assert.Equal("library-book-4", library.GenerateBookId());
    }
    
    [Fact]
    public void MustGenerateValidUserId()
    {
        var library = new Library();
        var bookId = library.GenerateUserId();
        Assert.Equal("library-user-1", bookId);
    }

    [Fact]
    public void MustAddBook()
    {
        var library = new Library();
        
        library.AddBook(_book.Isbn, _book.Title);
        
        Assert.Single(library.Books);
        Assert.Equal(_book.Id, library.Books.First().Id);
        Assert.Equal(_book.Isbn, library.Books.First().ISBN);
        Assert.Equal(_book.Title, library.Books.First().Title);
        Assert.True(library.Books.First().Available);
        Assert.False(library.Books.First().IsDeleted);
    }

    [Fact]
    public void MustWriteOffBook()
    {
        var library = new Library();
        var addedBook = library.AddBook(_book.Isbn, _book.Title);
        
        library.WriteOffBook(addedBook.Id);
        
        Assert.False(addedBook.Available);
        Assert.True(addedBook.IsDeleted);
    }

    [Fact]
    public void MustRegisterUser()
    {
        var library = new Library();
        var userAdded = library.RegisterUser(_user.Name);

        Assert.Single(library.Users);
        Assert.Equal(_user.Id, userAdded.Id);
        Assert.Equal(_user.Name, userAdded.Name);
        Assert.Empty(userAdded.BorrowedBooks);
        Assert.True(userAdded.Active);
    }

    [Fact]
    public void MustRemoveUser()
    {
        var library = new Library();
        library.RegisterUser(_user.Name);
        
        library.RemoveUser(_user.Id);
        
        Assert.False(library.Users.First().Active);
    }

    [Fact]
    public void MustIssueBooks()
    {
        var library = new Library();
        library.SeedData();
        var user1 = library.Users.First();
        var twoBooks = library.Books.Take(2).ToList();
        
        library.IssueBooks(user1.Id, twoBooks.Select(b => b.Id));

        Assert.Equal(2, user1.BorrowedBooks.Count);
        Assert.False(twoBooks.First().Available);
        Assert.False(twoBooks.Last().Available);
        Assert.True(library.Books.Last().Available);
    }

    [Fact]
    public void ThrowsWhenIssuingUnavailableOrWrittenOffBook()
    {
        var library = new Library();
        library.SeedData();
        var user1 = library.Users.First();
        var twoBooks = library.Books.Take(2).ToList();
        library.IssueBooks(user1.Id, twoBooks.Select(b => b.Id));

        //Throws when issuing already issued books
        var user3 = library.Users.Last();
        
        Assert.Throws<InvalidOperationException>(() =>
        {
            library.IssueBooks(user3.Id, twoBooks.Select(b => b.Id));
        });
        
        //Throws when book is being issued to un-registered User
        Assert.Throws<InvalidOperationException>(() =>
        {
            library.IssueBooks("random-user-Id", twoBooks.Select(b => b.Id));
        });
        
        //Throw when written-off book is issued
        library.Books.Last().WriteOff();
        
        Assert.Throws<InvalidOperationException>(() =>
        {
            library.IssueBooks(user3.Id, new [] { library.Books.Last().Id });
        });
        
        //Throws when book is being issued to deactivated User
        user3.Deactivate();
        
        Assert.Throws<InvalidOperationException>(() =>
        {
            library.IssueBooks(user3.Id, new [] { library.Books.Last().Id });
        });
    }
}