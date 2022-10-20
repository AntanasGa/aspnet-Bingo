using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bingo.Models;

namespace Bingo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public Game? game { get; private set; }

    public Card? card { get; private set; }
    public int score {get; private set; } = 0;

    public IndexModel(ILogger<IndexModel> logger)
    {
        const int bagItemCount = 60;
        const int drawnItemCount = 30;
        this.game = new Game(bagItemCount, drawnItemCount);
        this.card = new Card(bagItemCount);
        this.score = this.card.Check(this.game);
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
