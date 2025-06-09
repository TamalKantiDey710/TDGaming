
namespace TDGaming.Domain.Entities;

public class VideoGame
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public decimal Price { get; private set; }

    public VideoGame(string title, decimal price)
    {
        Title = title;
        Price = price;
    }

    public void Update(string title, decimal price)
    {
        Title = title;
        Price = price;
    }
}
