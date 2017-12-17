namespace ExeptionsSenderSmpt
{
    using Logging.ExceptionSender;

    public class ExceptionSenderSmtpOptions : ExceptionSenderOptions
    {
        /// <summary>
        /// Host your smtp server
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// Port of smtp server
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// Login of smtp server
        /// </summary>
        public string SmtpLogin { get; set; }

        /// <summary>
        /// Password of smtp server
        /// </summary>
        public string SmtpPassword { get; set; }

        /// <summary>
        /// Sender email ('from' in messages)
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Target (admin) email(s) ('to' in messages)
        /// </summary>
        public string[] To { get; set; }
    }
}
