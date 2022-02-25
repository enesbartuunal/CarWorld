using MassTransit;
using ProductCatalogue.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProductCatalogue.EmailApi.Service
{
    public class EmailSendService : IConsumer<EmailModel>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public EmailSendService(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<EmailModel> context)
        {
            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(SenderMail, SenderMailPassword)
            };
            string subject = "Welcome!";
            string body = "Registration completed successfully";
            try
            {
                email.Send(SenderMail, context.Message.Adress, subject, body);
            }

            catch 
            {
                if (context.Message.TryCount <= 5)
                {
                    context.Message.TryCount += 1;
                    var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:failedsendingmail"));
                    await sendEndpoint.Send<EmailModel>(context.Message);
                }
               
            }

        }

        public const string SenderMail = "enesbartuunal2040@gmail.com";

        public const string SenderMailPassword = "ebu155015502040";


    }
}
