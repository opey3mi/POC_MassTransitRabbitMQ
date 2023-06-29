using Consumer;
using MassTransit;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class MessageConsumerTests
    {
        [Test]
        public async Task Consume_ValidMessage_SendsEmail()
        {
            // Arrange
            var emailServiceMock = new Mock<EmailService>();
            var contextMock = new Mock<ConsumeContext<IMessage>>();
            var message = new Mock<IMessage>();
            message.Setup(m => m.Text).Returns("Test message");

            contextMock.Setup(c => c.Message).Returns(message.Object);

            var consumer = new MessageConsumer(emailServiceMock.Object);

            await consumer.Consume(contextMock.Object);

            emailServiceMock.Verify(
                es => es.SendEmail("eemaill@email.com", "Subject", "Test message"),
                Times.Once);
        }
    }
}