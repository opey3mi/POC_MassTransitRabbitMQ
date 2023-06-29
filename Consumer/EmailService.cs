using System.Net.Mail;
using System.Net;

namespace Consumer
{
    public class EmailService
    {
        public async Task SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress("ope@email.com", "Sender Name");
                var toAddress = new MailAddress(recipientEmail);
                const string fromPassword = "your-email-password";
                const string smtpHost = "smtp.example.com";
                const int smtpPort = 587;

                using (var smtp = new SmtpClient
                {
                    Host = smtpHost,
                    Port = smtpPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                })
                {
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        await smtp.SendMailAsync(message);
                    }
                }

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }
}
