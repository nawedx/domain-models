public class Book
{
    public string Id { get; }
    public string ISBN { get; }
    public string Title { get; }
    public bool Available { get; private set; }
    public bool IsDeleted { get; private set; }

    public Book(string id, string isbn, string title)
    {
        Id = id;
        ISBN = isbn;
        Title = title;
        Available = true;
    }

    public void WriteOff()
    {
        if (IsDeleted)
        {
            Console.WriteLine("Book already written-off");
            return;
        }
        
        if (!Available)
        {
            throw new InvalidOperationException($"Can't write-off an un-available book");
        }

        Available = false;
        IsDeleted = true;
    }

    public void Borrow()
    {
        if (!Available)
        {
            throw new InvalidOperationException($"Can't borrow an unavailable book");
        }

        if (IsDeleted)
        {
            throw new InvalidOperationException($"Can't borrow a written-off book");
        }

        Available = false;
    }

    public void Return()
    {
        if (Available)
        {
            throw new InvalidOperationException($"Can't return an available book");
        }

        if (IsDeleted)
        {
            throw new InvalidOperationException("Can't return a written-off book");
        }

        Available = true;
    }
}