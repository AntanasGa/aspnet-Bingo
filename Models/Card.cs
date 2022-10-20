namespace Bingo.Models;

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

    public int Check(Game game)
    {
        int result = 0;
        foreach (int[] row in this.table)
        {
            bool appendScore = true;
            foreach (int item in row)
            {
                if (!Array.Exists(game.numbers, el => el == item))
                {
                    appendScore = false;
                    break;
                }
            }
            if (appendScore)
            {
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
