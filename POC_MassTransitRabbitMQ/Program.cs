using MassTransit;

class Program
{
    static async Task Main(string[] args)
    {
        var bus = Bus.Factory.CreateUsingRabbitMq(config =>
        {
            config.Host("localhost", "/", host =>
            {
                host.Username("guest");
                host.Password("guest");
            });
        });

        await bus.StartAsync();

        try
        {
            Console.WriteLine("Enter a message (or 'exit' to quit):");

            string message;
            while ((message = Console.ReadLine()) != "exit")
            {
                await bus.Publish<IMessage>(new
                {
                    Text = message
                });

                Console.WriteLine("Message published. Enter another message (or 'exit' to quit):");
            }
        }
        finally
        {
            await bus.StopAsync();
        }
    }
}

public interface IMessage
{
    string Text { get; }
}
