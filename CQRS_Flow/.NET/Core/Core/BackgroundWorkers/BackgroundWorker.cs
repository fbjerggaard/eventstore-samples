namespace Core.BackgroundWorkers;

public class BackgroundWorker: IHostedService, IDisposable
{
    private readonly ILogger<BackgroundWorker> logger;
    private readonly Func<CancellationToken, Task> perform;
    private CancellationTokenSource? cts;
    private Task? executingTask;

    public BackgroundWorker(
        ILogger<BackgroundWorker> logger,
        Func<CancellationToken, Task> perform
    )
    {
        this.logger = logger;
        this.perform = perform;
    }

    public void Dispose()
    {
        cts?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Create a linked token so we can trigger cancellation outside of this token's cancellation
        cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        executingTask = Task.Run(() => perform(cts.Token), cancellationToken);

        return executingTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        // Stop called without start
        if (executingTask == null)
            return;

        // Signal cancellation to the executing method
        cts?.Cancel();

        // Wait until the issue completes or the stop token triggers
        await Task.WhenAny(executingTask, Task.Delay(-1, cancellationToken));

        // Throw if cancellation triggered
        cancellationToken.ThrowIfCancellationRequested();

        logger.LogInformation("Background worker stopped");
    }
}
