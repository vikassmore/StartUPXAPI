using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IEmailSender
    {
        Task Execute(string email, string subject, string message);
        //Task Execute(string email, string subject, string message);
        //Task SendEmailAsync(string email, string subject, string message);

        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailAsync(string email, string subject, string message, Dictionary<string, MemoryStream> attachments);
        //Task SendSmsAsync(string phonenumber, string subject, string message);
        //Task SendSmsAsync(string phonenumber, string subject, string message, string referenceId);
    }
}
