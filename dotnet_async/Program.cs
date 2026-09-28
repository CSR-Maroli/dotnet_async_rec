
Task<string> taskContent = Task.Run(() => File.ReadAllTextAsync("voos.txt"));
async Task ReadFileAsync(CancellationToken cancellationToken)
{
    try
    {
        await Task.Delay(new Random().Next(300, 8000));
        Console.WriteLine($"Conteudo: \n{taskContent.Result}");
        cancellationToken.ThrowIfCancellationRequested();
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine($"Operation canceled: {ex.Message}");
    }
    catch (AggregateException ex)
    {
        Console.WriteLine($"Error occurred: {ex.InnerException.Message}");

    }

}

async Task DisplayReportAsync(CancellationToken cancellationToken)
{
    try
    {
        Console.WriteLine("Executing report for passage buy!");
        await Task.Delay(new Random().Next(300, 8000));
        cancellationToken.ThrowIfCancellationRequested();
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine($"Operation canceled: {ex.Message}");
       
    }
    
}
CancellationTokenSource cancellationToken = new CancellationTokenSource();

//Task task1 = Task.Run(() => ReadFile());
//Task task2 = Task.Run(() => DisplayReport());

//await ReadFileAsync();
//await DisplayReportAsync();

Task tarefa = Task.WhenAll(ReadFileAsync(cancellationToken.Token), DisplayReportAsync(cancellationToken.Token));

await Task.Delay(2000).ContinueWith(t => cancellationToken.Cancel());

Console.WriteLine("Others operations");
Console.ReadKey();