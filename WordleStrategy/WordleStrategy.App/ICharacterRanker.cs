namespace WordleStrategy.App;

internal interface ICharacterRanker
{
    Task<Dictionary<char, int>> RankAsync();
}

internal class CharacterRanker : ICharacterRanker
{
    private readonly IWordStreamer _wordStreamer;

    public CharacterRanker(IWordStreamer wordStreamer)
    {
        _wordStreamer = wordStreamer;
    }

    public async Task<Dictionary<char, int>> RankAsync()
    {
        var ret = InitialiseDictionary();

        await foreach (var word in _wordStreamer.StreamWordsAsync())
        {
            foreach (var letter in word)
            {
                ret[letter]++;
            }
        }

        return ret;
    }

    private static Dictionary<char, int> InitialiseDictionary()
    {
        var ret = new Dictionary<char, int>();

        const string Letters = "abcdefghijklmnopqrstuvwxyz";

        foreach (var letter in Letters)
        {
            ret.Add(letter, 0);
        }

        return ret;
    }
}