using MassTransit;
using ProductCatalogue.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogue.Api.Helper
{
    public class EmailService : IConsumer<EmailModel>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public EmailService(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<EmailModel> context)
        {
           
            if (context.Message.TryCount < 6)
            {
                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:sendsignupmessage"));
                await sendEndpoint.Send<EmailModel>(context.Message);
            }
            else
            {
                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:usernotconfirmed"));
                await sendEndpoint.Send<EmailModel>(context.Message);
            }
        }

        public async Task SendEmailCommand(EmailModel emailAdress)
        {
            if (emailAdress.TryCount != 7)
                emailAdress.TryCount = 1;
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:sendsignupmessage"));
            await sendEndpoint.Send<EmailModel>(emailAdress);
        }
    }
}
