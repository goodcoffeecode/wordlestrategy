namespace WordleStrategy.App;

internal interface IWordRanker
{
    Task<Dictionary<string, int>> RankAsync();
}

internal class NoDuplicateLettersWordRanker : IWordRanker
{
    private readonly ICharacterRanker _characterRanker;
    private readonly IWordStreamer _wordStreamer;

    public NoDuplicateLettersWordRanker(ICharacterRanker characterRanker, IWordStreamer wordStreamer)
    {
        _characterRanker = characterRanker;
        _wordStreamer = wordStreamer;
    }

    public async Task<Dictionary<string, int>> RankAsync()
    {
        var letters = await _characterRanker.RankAsync();

        var ret = new Dictionary<string, int>();

        await foreach (var word in _wordStreamer.StreamWordsAsync())
        {
            if (ContainsNoDuplicateLetters(word))
            {
                ret.Add(word, RankWord(word, letters));
            }
        }

        return ret;
    }

    private static bool ContainsNoDuplicateLetters(string word)
    {
        for (var i = 0; i < word.Length - 1; i++)
        {
            if (word.Count(l => l == word[i]) > 1)
            {
                return false;
            }
        }

        return true;
    }

    private static int RankWord(string word, Dictionary<char, int> letters)
    {
        var score = 0;

        foreach (var letter in word)
        {
            score += letters[letter];
        }

        return score;
    }
}