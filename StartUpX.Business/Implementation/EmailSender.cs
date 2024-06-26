using Microsoft.Extensions.Options;
using StartUpX.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using StartUpX.Model;
using RestSharp;
using RestSharp.Authenticators;

namespace StartUpX.Business.Implementation
{
    public class EmailSender : IEmailSender
    {
        private ConfigurationModel _configuration;
        public EmailSender(IOptions<EmailSettings> emailSettings, IOptions<ConfigurationModel> hostName)
        {
            _emailSettings = emailSettings.Value;
            this._configuration = hostName.Value;

        }

        public EmailSettings _emailSettings { get; }
        public EmailSettings _emailSettings1 { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            //Execute(email, subject, message);
            SendEmail(email, subject, message);

            return Task.FromResult(0);
        }

        public async Task SendEmailAsync(string email, string subject, string message, Dictionary<string, MemoryStream> attachments)
        {
            //await Execute(email, subject, message, attachments);
            await SendEmail(email, subject, message, attachments);
        }
        public Task SendSmsAsync(string email, string userName, string otp)
        {
            using (var web = new System.Net.WebClient())
            {
                try
                {
                    //StartUpX's sms gateway
                    string url = "http://103.233.79.217/api/mt/SendSMS?user=MeshBA&password=Mahesh@123&senderid=MESHBA&channel=Trans&DCS=0&flashsms=0&number=" +
                    email +
                    "&text=Dear User ,  " + otp + " " +
                    "is the OTP for your login at StartUpX app. In case you have not requested this, please contact us. -" +
                    "MeshBA&route=8&peid=1701159146303386050&dlttemplateid=1707165114455193951";

                    //MeshBA's sms gateway
                    //string url = "http://103.233.79.217/api/mt/SendSMS?user=MeshBA&password=Mahesh@123&senderid=MESHBA&channel=Trans&DCS=0&flashsms=0&number=" +
                    //    phonenumber +
                    //    "&text= " +
                    //    "SMSBell-Rx: You have received a SMS, from device: StartUpX with Sub: The OTP for your login is " +
                    //    otp +
                    //    " MeshBA&route=8&peid=1701159146303386050&dlttemplateid=1707162090147685530";


                    string result = web.DownloadString(url);

                }
                catch (Exception ex)
                {
                    //Catch and show the exception if needed. Donot supress. :)  

                }
            }
            return Task.FromResult(0);
        }

      

        public async Task<IRestResponse> SendEmail(string email, string subject, string message)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(_emailSettings.BaseUri);
            client.Authenticator = new HttpBasicAuthenticator("api", _emailSettings.ApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", _emailSettings.Domain, ParameterType.UrlSegment);
            request.Resource = _emailSettings.RequestUri;
            request.AddParameter("from", _emailSettings.From);
            request.AddParameter("to", email);
            request.AddParameter("cc", _emailSettings.CcEmail);
            request.AddParameter("subject", subject);
            request.AddParameter("html", message);
            request.Method = Method.POST;
            return await client.ExecuteAsync(request);
        }

        public async Task<IRestResponse> SendEmail(string email, string subject, string message, Dictionary<string, MemoryStream> attachments)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(_emailSettings.BaseUri);
            client.Authenticator = new HttpBasicAuthenticator("api", _emailSettings.ApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", _emailSettings.Domain, ParameterType.UrlSegment);
            request.Resource = _emailSettings.RequestUri;
            request.AddParameter("from", _emailSettings.From);
            //request.AddParameter("to", "khot.vijayananda@gmail.com");
            request.AddParameter("to", email);
            request.AddParameter("cc", _emailSettings.CcEmail);
            request.AddParameter("subject", subject);
            request.AddParameter("html", message);
            foreach (var item in attachments)
            {
                System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                request.AddFile("attachment", item.Value.ToArray(), item.Key, ct.MediaType);
            }
            request.Method = Method.POST;
            var result = await client.ExecuteAsync(request);
            return result;
        }

        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email)
                                 ? _emailSettings.ToEmail
                                 : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "StartUpX")
                };
                mail.To.Add(new MailAddress(toEmail));
                try
                {
                   // mail.CC.Add(new MailAddress(_emailSettings.CcEmail));
                }
                catch { }
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }


            }
            catch (Exception ex)
            {
                //do something here
            }
        }

   
    }
}
