public static class LibraryDataSeederExtension
{
    private static readonly List<string> UserNames = new() { "User One", "User Two", "User Three" };

    private static readonly List<BookInfo> BookDetails = new()
    {
        new BookInfo("ISBN-1", "Book One"),
        new BookInfo("ISBN-2", "Book Two"),
        new BookInfo("ISBN-3", "Book Three")
    };

    private record BookInfo(string Isbn, string Title);
    
    public static void SeedData(this Library library)
    {
        foreach (var userName in UserNames)
        {
            library.RegisterUser(userName);
        }

        foreach (var book in BookDetails)
        {
            library.AddBook(book.Isbn, book.Title);
        }
    }
}