using Consumer;
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

            config.ReceiveEndpoint("email_queue", endpoint =>
            {
                var emailService = new EmailService();
                endpoint.Consumer(() => new MessageConsumer(emailService));
            });
        });

        await bus.StartAsync();
        Console.WriteLine("Consumer started. Press any key to exit.");
        Console.ReadKey();
        await bus.StopAsync();
    }
}