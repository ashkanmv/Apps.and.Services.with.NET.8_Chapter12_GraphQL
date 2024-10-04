

using Microsoft.Extensions.DependencyInjection;
using Northwind.GraphQL.Client.Console;
using StrawberryShake;

ServiceCollection serviceCollection = new();

serviceCollection
    .AddNorthwindClient()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5121/graphql"));

IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
INorthwindClient client = serviceProvider.GetRequiredService<INorthwindClient>();

var result = await client.SeafoodProducts.ExecuteAsync();
result.EnsureNoErrors();
if (result.Data is null)
{
    Console.WriteLine("No data!");
    return;
}
foreach (var product in result.Data.ProductsInCategory)
{
    Console.WriteLine("{0}: {1}",
        product.ProductId, product.ProductName);
}

