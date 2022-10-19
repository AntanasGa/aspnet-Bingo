using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

public class Card
    {
        public int[][] table { get; }
        private const int ROW_COUNT = 3;
        private const int COL_COUNT = 5;

        public Card(int maxVal)
        {
            int[] availableNumbers = Enumerable.Range(1, maxVal).ToArray();
            int[][] table = new int[Card.ROW_COUNT][];
            for (int i = 0; i < Card.ROW_COUNT; i++)
            {
                table[i] = generateRow(ref availableNumbers);
            }
            this.table = table;
        }

        public int Check(Game game) {
            int result = 0;
            foreach (int[] row in this.table) {
                bool appendScore = true;
                foreach (int item in row) {
                    if (!Array.Exists(game.numbers, el => el == item)) {
                        appendScore = false;
                        break;
                    }
                }
                if (appendScore) {
                    result++;
                }
            }
            return result == Card.ROW_COUNT ? Game.SCORE_MAX : result * Game.SCORE_PER_ROW;
        }

        private int[] generateRow(ref int[] availableNumbers)
        {
            int[] result = new int[Card.COL_COUNT];
            Random rnd = new Random();
            for (int i = 0; i < Card.COL_COUNT; i++)
            {
                int index = rnd.Next(0, availableNumbers.Length - 1);
                result[i] = availableNumbers[index];
                availableNumbers = availableNumbers.Where((_, id) => id != index).ToArray();
            }
            Array.Sort(result);
            return result;
        }
    }

    public class Game
    {
        public int[] numbers { get; }
        public const int SCORE_PER_ROW = 100;
        public const int SCORE_MAX = 1500;

        public Game(int maxVal, int pulls)
        {
            int[] availableNumbers = Enumerable.Range(1, maxVal).ToArray();
            int[] result = new int[pulls];
            Random rnd = new Random();
            for (int i = 0; i < pulls; i++) {
                int index = rnd.Next(0, availableNumbers.Length - 1);
                result[i] = availableNumbers[index];
                availableNumbers = availableNumbers.Where((_, id) => id != index).ToArray();
            }
            Array.Sort(result);
            this.numbers = result;
        }
    }
