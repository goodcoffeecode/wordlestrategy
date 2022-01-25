using Microsoft.Extensions.DependencyInjection;
using WordleStrategy.App;

Console.WriteLine("These are the Top 10 Best starting words for Wordle");

var runner = ServiceProviderSingleton.Instance.GetService<Runner>();

await runner.RunAsync();