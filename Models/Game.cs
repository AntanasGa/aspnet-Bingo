namespace Bingo.Models;

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
        for (int i = 0; i < pulls; i++)
        {
            int index = rnd.Next(0, availableNumbers.Length - 1);
            result[i] = availableNumbers[index];
            availableNumbers = availableNumbers.Where((_, id) => id != index).ToArray();
        }
        Array.Sort(result);
        this.numbers = result;
    }
}
