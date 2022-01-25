using Microsoft.Extensions.Options;

namespace WordleStrategy.App;

internal interface IWordStreamer
{
    IAsyncEnumerable<string?> StreamWordsAsync();
}

internal class WordStreamerFromFile : IWordStreamer
{
    private readonly string _fileName;

    public WordStreamerFromFile(IOptions<FilePathOptions> options)
    {
        _fileName = options?.Value.FileName ?? throw new ArgumentNullException(nameof(options));
    }

    public async IAsyncEnumerable<string?> StreamWordsAsync()
    {
        using var fs = File.OpenRead(_fileName);
        using var sr = new StreamReader(fs);
        while (!sr.EndOfStream)
        {
            yield return await sr.ReadLineAsync();
        }

        sr.Close();
        fs.Close();
    }
}