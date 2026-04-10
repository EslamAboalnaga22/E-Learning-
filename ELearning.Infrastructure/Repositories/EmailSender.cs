namespace ELearning.Infrastructure.Repositories
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailSender = "eslamnga220@gmail.com";
            var password = "rodm hryl mxaw marv";
            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress(emailSender);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = $"<html><body>{htmlMessage}</body></html>";
            mailMessage.IsBodyHtml = true;

            using SmtpClient _smpt = new();
            _smpt.EnableSsl = true;
            _smpt.UseDefaultCredentials = false;
            _smpt.Credentials = new NetworkCredential(emailSender, password);
            _smpt.Host = "smtp.gmail.com";
            _smpt.Port = 587;
            _smpt.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smpt.Send(mailMessage);
        }
    }
}
