namespace WordleStrategy.App;

internal class Runner
{
    private readonly IWordRanker _wordRanker;

    public Runner(IWordRanker wordRanker)
    {
        _wordRanker = wordRanker;
    }

    internal async Task RunAsync()
    {
        var ret = await _wordRanker.RankAsync();

        foreach (var word in ret.OrderByDescending(x => x.Value).Take(10))
        {
            Console.WriteLine($"{word.Key} = {word.Value}");
        }
    }
}