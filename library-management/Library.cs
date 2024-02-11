public class Library
{
    public List<Book> Books { get; private set; }

    public List<User> Users { get; private set; }

    public Library()
    {
        Books = new List<Book>();
        Users = new List<User>();
    }

    public string GenerateBookId()
    {
        var booksCount = Books.Count;
        return $"library-book-{booksCount + 1}";
    }

    public string GenerateUserId()
    {
        var usersCount = Users.Count;
        return $"library-user-{usersCount + 1}";
    }

    // Add Books
    public Book AddBook(string isbn, string title)
    {
        var id = GenerateBookId();
        var book = new Book(id, isbn, title);
        Books.Add(book);
        return book;
    }

    // Write-off Books
    public void WriteOffBook(string id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if(book == null)
        {
            Console.WriteLine("The book to be written-off is not present in inventory");
            return;
        }

        book.WriteOff();
    }
	
    // Add User
    public User RegisterUser(string name)
    {
        var id = GenerateUserId();
        var user = new User(id, name);
        Users.Add(user);
        return user;
    }

    // Remove User
    public void RemoveUser(string id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if(user == null)
        {
            Console.WriteLine("Cannot remove a user that isn't registered in the system");
            return;
        }
		
        user.Deactivate();
    }
	
    // Issue Books
    public void IssueBooks(string userId, IEnumerable<string> bookIds)
    {
        var user = Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            throw new InvalidOperationException($"Trying to issue books to user {userId} not found in the system");
        }

        if (!user.Active)
        {
            throw new InvalidOperationException($"Trying to issue books to de-activated user {userId}");
        }
        
		foreach(var bookId in bookIds)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                Console.WriteLine($"Trying to issue a book {bookId} not found in system");
                continue;
            }
            
            // TODO: These two lines are part of single transaction
            user.Borrow(book);
            book.Borrow();
        }
    }
	
    // Return Books
    public void ReturnBooks(string userId, IEnumerable<string> bookIds)
    {
        var user = Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            throw new InvalidOperationException($"User {userId} not found in the system");
        } 
        
        foreach(var bookId in bookIds)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                Console.WriteLine($"Trying to return a book {bookId} not found in system");
                continue;
            }
            
            // TODO: These two lines are part of single transaction
            user.Return(book);
            book.Return();
        }
    }
}