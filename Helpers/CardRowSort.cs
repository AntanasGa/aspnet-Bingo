namespace Bingo.Helpers;

public class CardRowSort : IComparer<int[]>
{
    public int Compare(int[]? x, int[]? y)
    {
        if (x == null || y == null) {
            return 0;
        }
        return x[0].CompareTo(y[0]);
    }
}
