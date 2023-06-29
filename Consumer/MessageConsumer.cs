using MassTransit;

namespace Consumer
{
    public class MessageConsumer : IConsumer<IMessage>
    {
        private readonly EmailService _emailService;
        public MessageConsumer(EmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Consume(ConsumeContext<IMessage> context)
        {
            await Console.Out.WriteLineAsync($"Received message: {context.Message.Text}");
            await _emailService.SendEmail("ope@email.com", "Subject", context.Message.Text);
        }
    }

    public interface IMessage
    {
        string Text { get; }
    }
}
