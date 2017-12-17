namespace ExeptionsSenderSmpt
{
    using System.IO;
    using System.Net;
    using Logging.ExceptionSender;
    using MailKit.Net.Smtp;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using RecurrentTasks;

    public class ExceptionSenderSmtpTask : ExceptionSenderTask
    {

        private ExceptionSenderSmtpOptions options;

        public ExceptionSenderSmtpTask(
            ILogger<ExceptionSenderSmtpTask> logger,
            IOptions<ExceptionSenderSmtpOptions> options,
            IHostingEnvironment hostingEnvironment)
            : base(logger, options.Value, hostingEnvironment)
        {
            this.options = options.Value;
        }

        protected override void Send(ITask currentTask, string text, FileInfo logFile)
        {
            SmtpClient client = new SmtpClient();
            client.Connect(this.options.SmtpHost, this.options.SmtpPort, true);
            client.Authenticate(new NetworkCredential(this.options.SmtpLogin, this.options.SmtpPassword));

            MimeMessage mailMessage = new MimeMessage();

            mailMessage.Sender = new MailboxAddress(this.options.From);

            mailMessage.Subject = text.Substring(0, text.IndexOf("\r\n"));

            foreach (var to in options.To)
            {
                mailMessage.To.Add(new MailboxAddress(to));
            }

            var bodyBuilder = new BodyBuilder();

            bodyBuilder.TextBody = text;

            if (logFile != null && logFile.Exists)
            {
                bodyBuilder.Attachments.Add(logFile.FullName);
            }

            mailMessage.Body = bodyBuilder.ToMessageBody();

            client.Send(mailMessage);
        }
    }
}
