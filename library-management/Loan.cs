public class Loan
{
    public string Id { get; private set; }

    public Book Book { get; private set; }

    public User User { get; private set; }

    public DateOnly IssueDate { get; private set; }
        
    public int LoanDuration { get; private set; }

    public DateOnly DueDate { get; private set; }

    public Loan(string id, Book book, User user, DateOnly issueDate, int loanDuration)
    {
        Id = id;
        Book = book;
        User = user;
        IssueDate = issueDate;
        LoanDuration = loanDuration;
        DueDate = issueDate.AddDays(loanDuration);
    }
}