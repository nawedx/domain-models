public class User{
	public string Id { get; }
	public string Name { get; }
	public List<Book> BorrowedBooks { get; private set; }
	public bool Active { get; private set; }

	public User(string id, string name){
		Id = id;
		Name = name;
		BorrowedBooks = new List<Book>();
		Active = true;
	}

	public void Deactivate()
	{
		if(!Active)
		{
			return;
		}
		if(BorrowedBooks.Any())
		{
			throw new InvalidOperationException("Can't deactivate a user with borrowed books");
		}

		Active = false;
	}

	public void Borrow(Book book){
		if(BorrowedBooks.Any(b => b.Id == book.Id)) 
		{
			Console.WriteLine($"Issuing an already borrowed book {book.Id} - {book.Title}");
			return;	
		}

		if (!book.Available || book.IsDeleted)
		{
			throw new InvalidOperationException("User can't borrow an unavailable book");
		}

		BorrowedBooks.Add(book);
	}

	public void Return(Book book){
		var borrowedBook = BorrowedBooks.FirstOrDefault(b => b.Id == book.Id);
		if(borrowedBook is null)
		{
			throw new InvalidOperationException($"Book was not borrowed by the user");
		}
		
		BorrowedBooks.Remove(borrowedBook);
	}
}
