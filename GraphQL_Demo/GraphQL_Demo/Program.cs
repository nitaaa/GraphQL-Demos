var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer().AddQueryType<Query>();

var app = builder.Build();
app.UseRouting().UseEndpoints(endpoints => endpoints.MapGraphQL());

app.MapGet("/", () => "Hello World!");

app.Run();


public record Book(string Title, Author Author);
public record Author(string Name);

public class Query
{
    readonly List<Book?> books = new()
    {
        new Book ("A Court of Thorns and Roses", new Author("Sarah J Maas")),
        new Book ("Little Women", new Author("Louisa May Alcott")),
        new Book ("Bad Luck and Trouble", new Author("Lee Child"))
    };

    public List<Book> GetBooks() => books;
    public Book? GetBook(string title) => books.FirstOrDefault( x => x?.Title == title);
    public Author? GetAuthor(string name) => books.Where(x => x?.Author.Name == name).FirstOrDefault()?.Author;

}