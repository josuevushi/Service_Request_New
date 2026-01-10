using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.Domain.Repositories;

namespace Dn.ServiceRequest.Tickets
{
    public class TicketEmailNotificationArgs
    {
        public Guid TicketId { get; set; }
        public string TypeNotification { get; set; } // "Création" ou "Clôture"
    }

    public class TicketEmailNotificationJob : AsyncBackgroundJob<TicketEmailNotificationArgs>, ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly IRepository<Ticket, Guid> _ticketRepository;
        private readonly IRepository<IdentityUser, Guid> _userRepository;
        private readonly IConfiguration _configuration;

        public TicketEmailNotificationJob(
            IEmailSender emailSender,
            IRepository<Ticket, Guid> ticketRepository,
            IRepository<IdentityUser, Guid> userRepository,
            IConfiguration configuration)
        {
            _emailSender = emailSender;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public override async Task ExecuteAsync(TicketEmailNotificationArgs args)
        {
            var ticket = await _ticketRepository.FindAsync(args.TicketId);
            if (ticket == null) return;

            var creator = await _userRepository.FindAsync(ticket.CreatorId.GetValueOrDefault());
            if (creator == null || string.IsNullOrWhiteSpace(creator.Email)) return;

            string subject = "";
            string title = "";
            string message = "";
            string primaryColor = "#a12b2a";

            if (args.TypeNotification == "Création")
            {
                subject = $"[Nouveau Ticket] {ticket.Numero} - {ticket.Object}";
                title = "Nouveau Ticket Créé";
                message = $@"
                    <p>Bonjour <strong>{creator.UserName}</strong>,</p>
                    <p>Votre ticket a été créé avec succès.</p>
                    <div style='background-color: #f8f9fa; border-left: 4px solid {primaryColor}; padding: 15px; margin: 20px 0;'>
                        <p style='margin: 0;'><strong>Numéro :</strong> {ticket.Numero}</p>
                        <p style='margin: 5px 0;'><strong>Objet :</strong> {ticket.Object}</p>
                        <p style='margin: 5px 0;'><strong>Date Estimée :</strong> {ticket.EstimateDate:dd/MM/yyyy HH:mm}</p>
                    </div>
                    <p>Vous recevrez une notification dès qu'une mise à jour sera disponible.</p>";
            }
            else if (args.TypeNotification == "Clôture")
            {
                subject = $"[Ticket Clôturé] {ticket.Numero} - {ticket.Object}";
                title = "Ticket Clôturé";
                message = $@"
                    <p>Bonjour <strong>{creator.UserName}</strong>,</p>
                    <p>Nous vous informons que votre ticket a été clôturé.</p>
                    <div style='background-color: #f8f9fa; border-left: 4px solid {primaryColor}; padding: 15px; margin: 20px 0;'>
                        <p style='margin: 0;'><strong>Numéro :</strong> {ticket.Numero}</p>
                        <p style='margin: 5px 0;'><strong>Objet :</strong> {ticket.Object}</p>
                        <p style='margin: 5px 0;'><strong>Date de Clôture :</strong> {ticket.ClosureDate:dd/MM/yyyy HH:mm}</p>
                    </div>
                    <p>Merci de votre confiance.</p>";
            }

            string body = $@"
                <div style='font-family: ""Public Sans"", -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif; max-width: 600px; margin: auto; border: 1px solid #e1e4e8; border-radius: 8px; overflow: hidden;'>
                    <div style='background-color: {primaryColor}; color: white; padding: 20px; text-align: center;'>
                        <h2 style='margin: 0;'>{title}</h2>
                    </div>
                    <div style='padding: 30px; color: #444; line-height: 1.6;'>
                        {message}
                        <div style='text-align: center; margin-top: 30px;'>
                            <a href='{_configuration["OpenIddict:Applications:ServiceRequest_Web:RootUrl"]}/tickets/details?id={ticket.Id}' 
                               style='background-color: {primaryColor}; color: white; padding: 12px 25px; text-decoration: none; border-radius: 5px; font-weight: bold;'>
                               Voir le Ticket
                            </a>
                        </div>
                    </div>
                    <div style='background-color: #f8f9fa; color: #888; padding: 15px; text-align: center; font-size: 12px;'>
                        Ceci est un message automatique, merci de ne pas y répondre.<br>
                        &copy; {DateTime.Now.Year} Gestion des Requêtes de Service
                    </div>
                </div>";

            await _emailSender.SendAsync(creator.Email, subject, body, isBodyHtml: true);
        }
    }
}
