using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Dn.ServiceRequest.Types;
using Type = Dn.ServiceRequest.Types.Type;
using Volo.Abp;
using System.Linq.Dynamic.Core;
using Dn.ServiceRequest.PieceJointes;
using Dn.ServiceRequest.Familles;
using Volo.Abp.Identity;
using Dn.ServiceRequest.Groupes;
using Dn.ServiceRequest.GroupeTypes;
using Dn.ServiceRequest.GroupeUsers;
using Volo.Abp.Authorization;
using Volo.Abp.Timing;
using System.IO;

namespace Dn.ServiceRequest.Tickets
{
    public class TicketAppService :
        CrudAppService<
            Ticket,
            TicketDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateTicketDto>,
        ITicketAppService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Type, Guid> _typeRepository;
        private readonly IRepository<Famille, Guid> _familleRepository;
        private readonly IRepository<IdentityUser, Guid> _userRepository;
        private readonly IRepository<PieceJointe, Guid> _pieceJointeRepository;
        //
        private readonly IRepository<Groupe, Guid> _repositoryGroupe;
        private readonly IRepository<GroupeType, Guid> _repositoryGroupeType;
        private readonly IRepository<GroupeUser, Guid> _repositoryGroupeUser;

        private readonly IClock _clock;

        public TicketAppService(
            IRepository<Ticket, Guid> repository,
            IConfiguration configuration,
            IRepository<Type, Guid> typeRepository,
            IRepository<PieceJointe, Guid> pieceJointeRepository,
            IRepository<Famille, Guid> familleRepository,
            IRepository<IdentityUser, Guid> userRepository,
            IRepository<Groupe, Guid> repositoryGroupe,
            IRepository<GroupeType, Guid> repositoryGroupeType,
            IRepository<GroupeUser, Guid> repositoryGroupeUser,
            IClock clock
            )
            : base(repository)
        {
            _configuration = configuration;
            _typeRepository = typeRepository;
            _pieceJointeRepository = pieceJointeRepository;
            _familleRepository = familleRepository;
            _userRepository = userRepository;
            _repositoryGroupe = repositoryGroupe;
            _repositoryGroupeType = repositoryGroupeType;
            _repositoryGroupeUser = repositoryGroupeUser;
            _clock = clock;
        }
        public async Task<Ticket> SetDemarrerTicket(string ticketId)
        {
            if (!Guid.TryParse(ticketId, out var ticketGuid))
                throw new BusinessException("Id du ticket invalide");

            var ticket = await Repository.FindAsync(ticketGuid);
            if (ticket == null)
                throw new BusinessException("Ticket introuvable");

            ticket.Status = Status.WorkInProgress; // assuming 0 => Open, 1 => InProgress
            await Repository.UpdateAsync(ticket, autoSave: true);
            return ticket;
        }
        public async Task<UnTicketsDto> GetUnTicketAsync(string ticketId)
        {
            // -------------------------------
            // Validation
            // -------------------------------
            if (!Guid.TryParse(ticketId, out var ticketGuid))
                throw new BusinessException("Id du ticket invalide");

            if (!CurrentUser.Id.HasValue)
                throw new AbpAuthorizationException();

            // -------------------------------
            // Récupération du ticket (entité)
            // -------------------------------
            var monTicket = await Repository.GetAsync(ticketGuid);
            if (monTicket == null)
                throw new BusinessException("Ticket introuvable");

            // -------------------------------
            // Query EF Core (DATABASE ONLY)
            // -------------------------------
            var ticket = await AsyncExecuter.FirstOrDefaultAsync(
                from tck in await Repository.GetQueryableAsync()
                join tpe in await _typeRepository.GetQueryableAsync()
                    on tck.Type_id equals tpe.Id
                join fam in await _familleRepository.GetQueryableAsync()
                    on tpe.Famille_id equals fam.Id
                join usr in await _userRepository.GetQueryableAsync()
                    on tck.CreatorId equals usr.Id
                where tck.Id == ticketGuid
                where usr.Id == CurrentUser.Id
                select new UnTicketsDto
                {
                    Id = tck.Id,
                    Type = tpe.Nom,
                    Famille = fam.Nom,
                    Numero = tck.Numero,
                    Status = tck.Status.ToString(),
                    Objet = tck.Object,
                    Description = tck.Description,
                    CreationTime = tck.CreationTime,
                    EstimateDate = tck.EstimateDate,
                    CreatedBy = usr.UserName
                }
            );

            if (ticket == null)
                return null;

            // -------------------------------
            // Logique métier (C# uniquement)
            // -------------------------------

            // Assigné à
            if (monTicket.AssignedTo != Guid.Empty)
            {
                var assignTo = await _userRepository.FindAsync(monTicket.AssignedTo);
                ticket.AssignTo = assignTo?.UserName;

                if (assignTo != null)
                {
                    var groupe = (
                        from grp in await _repositoryGroupe.GetQueryableAsync()
                        join grpUser in await _repositoryGroupeUser.GetQueryableAsync()
                            on grp.Id equals grpUser.Groupe_id
                        where grpUser.User_id == assignTo.Id
                        select grp
                    ).FirstOrDefault();

                    ticket.Groupe = groupe?.Nom;
                }
            }
            // Pourcentage
            ticket.Pourcentage = ticket.ClosureDate == null
                ? 0
                : PourcentageEntreDeuxDates(
                    ticket.CreationTime,
                    ticket.ClosureDate
                );

            return ticket;
        }
        public async Task<List<IdentityUser>> GetUserTicketAsync(string ticketId)
        {
            /*
            // -------------------------------
            // Validation
            // -------------------------------
            if (!Guid.TryParse(ticketId, out var ticketGuid))
                throw new BusinessException("Id du ticket invalide");

            if (!CurrentUser.Id.HasValue)
                throw new AbpAuthorizationException();

            // -------------------------------
            // Récupération du ticket (entité)
            // -------------------------------
            var monTicket = await Repository.GetAsync(ticketGuid);
            if (monTicket == null)
                throw new BusinessException("Ticket introuvable");

            // -------------------------------
            // Query EF Core (DATABASE ONLY)
            // -------------------------------
            var ticket = await AsyncExecuter.FirstOrDefaultAsync(
                from tck in await Repository.GetQueryableAsync()
                join grpUser in await _repositoryGroupeUser.GetQueryableAsync()
                    on tck.AssignedTo equals grpUser.User_id
                join grp in await _repositoryGroupe.GetQueryableAsync()
                    on grp. equals fam.Id
                join usr in await _userRepository.GetQueryableAsync()
                    on tck.CreatorId equals usr.Id
                where tck.Id == ticketGuid
                where usr.Id == CurrentUser.Id
                select new UnTicketsDto
                {
                    Id = tck.Id,
                    Type = tpe.Nom,
                    Famille = fam.Nom,
                    Numero = tck.Numero,
                    Status = tck.Status.ToString(),
                    Objet = tck.Object,
                    Description = tck.Description,
                    CreationTime = tck.CreationTime,
                    ClosureDate = tck.ClosureDate,
                    CreatedBy = usr.UserName
                }
            );

            if (ticket == null)
                return null;

            // -------------------------------
            // Logique métier (C# uniquement)
            // -------------------------------

          // Assigné à
        if (monTicket.AssignedTo != Guid.Empty)
        {
            var assignTo = await _userRepository.FindAsync(monTicket.AssignedTo);
            ticket.AssignTo = assignTo?.UserName;

            if (assignTo != null)
            {
                var groupe = (
                    from grp in await _repositoryGroupe.GetQueryableAsync()
                    join grpUser in await _repositoryGroupeUser.GetQueryableAsync()
                        on grp.Id equals grpUser.Groupe_id
                    where grpUser.User_id == assignTo.Id
                    select grp
                ).FirstOrDefault();

                ticket.Groupe = groupe?.Nom;
            }
        }
            // Pourcentage
            ticket.Pourcentage = ticket.ClosureDate == null
                ? 0
                : PourcentageEntreDeuxDates(
                    ticket.CreationTime,
                    ticket.ClosureDate
                );
        */
            return null;
        }

        public async Task<List<MesTicketsDto>> GetMesTickets()
        {
            var types = await _typeRepository.GetQueryableAsync();
            var familles = await _familleRepository.GetQueryableAsync();
            var tcks = await Repository.GetQueryableAsync();
            var users = await _userRepository.GetQueryableAsync();

            var query =
            from tck in tcks
            join type in types on tck.Type_id equals type.Id
            join famille in familles on type.Famille_id equals famille.Id
            join usr in users on tck.CreatorId equals usr.Id
            where usr.Id == CurrentUser.Id
            select new
            {
                tck.Id,
                type.Nom,
                Famille = famille.Nom,
                tck.Numero,
                tck.Status,
                Objet = tck.Object,
                tck.CreationTime,
                tck.EstimateDate,
                tck.PendingDate,
                tck.ClosureDate,
                User = usr.UserName
            };

            var data = await AsyncExecuter.ToListAsync(query);

            return data.Select(x =>
            {
                DateTime dateReference = x.Status switch
                {
                    Status.Open => _clock.Now,
                    Status.WorkInProgress => _clock.Now,
                    Status.Pending => x.PendingDate,
                    Status.Close => x.ClosureDate,
                    _ => _clock.Now
                };

                return new MesTicketsDto
                {
                    Id = x.Id,
                    Type = x.Nom,
                    Famille = x.Famille,
                    Numero = x.Numero,
                    Status = x.Status.ToString(),
                    Objet = x.Objet,
                    CreationDate = x.CreationTime,
                    EstimateDate = x.EstimateDate,
                    User = x.User,
                    Pourcentage = PourcentageEntreDeuxDates(
                        x.CreationTime,
                        x.EstimateDate,
                        dateReference
                    )
                };
            }).ToList();
        }
        // -------------------------------
        // POURCENTAGE ENTRE DEUX DATES
        // -------------------------------
        public double PourcentageEntreDeuxDates(
            DateTime dateDebut,
            DateTime? dateFin,
            DateTime? dateCourante = null)
        {
            var courante = dateCourante ?? _clock.Now;
            var fin = dateFin ?? courante;

            if (courante <= dateDebut)
                return 0.00;

            var total = (fin - dateDebut).TotalMilliseconds;
            if (total <= 0)
                return 100.00;

            var ecoule = (courante - dateDebut).TotalMilliseconds;

            var pourcentage = (ecoule / total) * 100;

            return Math.Round(
                Math.Clamp(pourcentage, 0, 100),
                2
            );
        }


        // -------------------------------
        // LECTURE DES CONGÉS DEPUIS CONFIG
        // -------------------------------
        private HashSet<DateTime> GetJoursConges()
        {
            var joursConges = _configuration
                .GetSection("BusinessRules:JoursConges")
                .Get<string[]>() ?? Array.Empty<string>();

            return joursConges
                .Select(d => DateTime.Parse(d).Date)
                .ToHashSet();
        }

        // -------------------------------
        // DATE ESTIMÉE EN SAUTANT CONGÉS
        // -------------------------------
        public DateTime DateEstimeeAvecConges(DateTime dateDebut, double dureeMs)
        {
            var conges = GetJoursConges();
            var current = dateDebut;
            var msRestants = dureeMs;

            while (msRestants > 0)
            {
                // Si weekend ou congé, saute au prochain jour ouvré à minuit
                if (current.DayOfWeek == DayOfWeek.Saturday ||
                    current.DayOfWeek == DayOfWeek.Sunday ||
                    conges.Contains(current.Date))
                {
                    current = current.Date.AddDays(1);
                    continue;
                }

                // Combien de millisecondes restent dans la journée en cours
                var finJour = current.Date.AddDays(1);
                var msDisponible = (finJour - current).TotalMilliseconds;

                // On prend soit tout le temps restant, soit jusqu'à la fin du jour
                var msAPrendre = Math.Min(msRestants, msDisponible);

                // Avance le temps
                current = current.AddMilliseconds(msAPrendre);

                // Décrémente le temps restant
                msRestants -= msAPrendre;
            }

            return current;
        }

        // -------------------------------
        // AJOUT D’UN NOUVEAU TICKET
        // -------------------------------
        public async Task<Ticket> GetNewTicket(NewTicketDto data)
        {
            var maintenant = _clock.Now;
            var typeGuid = Guid.Parse(data.IdenType);
            var type = await _typeRepository.FindAsync(typeGuid);
            if (type == null)
                throw new BusinessException("Type de ticket invalide");

            Ticket tck = new Ticket
            {
                Object = data.Objet,
                Description = data.Description,
                Json_form = data.JsonFrom,
                Type_id = typeGuid,
                EstimateDate = DateEstimeeAvecConges(maintenant, type.Sla),
                Status = Status.Open,
                Numero = await GenerateTicketNumberAsync()
            };
            tck = await Repository.InsertAsync(tck, autoSave: true);
            //
            if (data.Fichiers == null || data.Fichiers.Count == 0)
            {
                Console.WriteLine("La liste est vide ou nulle.");

            }
            else
            {
                foreach (string fichier in data.Fichiers)
                {
                    Console.WriteLine(fichier);
                    PieceJointe pj = new PieceJointe();
                    pj.Ticket_id = tck.Id;
                    pj.Nom = Path.GetFileName(fichier);
                    pj.Path = fichier;
                    await _pieceJointeRepository.InsertAsync(pj);

                }
            }




            return tck;

        }

        public async Task<Ticket> AddNewTicket(string objet, string description, string json_from, string IdenType)
        {


            var maintenant = _clock.Now;
            var typeGuid = Guid.Parse(IdenType);
            var type = await _typeRepository.FindAsync(typeGuid);
            if (type == null)
                throw new BusinessException("Type de ticket invalide");


            Ticket tck = new Ticket
            {
                Object = objet,
                Description = description,
                Json_form = json_from,
                Type_id = typeGuid,
                EstimateDate = DateEstimeeAvecConges(maintenant, type.Sla),
                Status = Status.Open,
                Numero = await GenerateTicketNumberAsync()
            };
            tck = await Repository.InsertAsync(tck, autoSave: true);
            //

            return tck;
        }
        private async Task<string> GenerateTicketNumberAsync()
        {
            var now = _clock.Now;
            var prefix = $"TCK-{now:MMyy}-";

            var tcks = await Repository.GetQueryableAsync();

            // Passe en liste pour faire FirstOrDefault
            var lastTicketNumber = tcks
                .Where(t => t.Numero.StartsWith(prefix))
                .OrderByDescending(t => t.Numero)
                .Select(t => t.Numero)
                .FirstOrDefault(); // LINQ classique

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastTicketNumber))
            {
                var lastPart = lastTicketNumber.Split('-').Last();
                if (int.TryParse(lastPart, out var n))
                    nextNumber = n + 1;
            }

            return $"{prefix}{nextNumber:D4}";
        }


        public async Task<List<IdentityUserDto>> GetUsersInGroups()
        {
            var usersQuery = await _userRepository.GetQueryableAsync();
            var groupUsersQuery = await _repositoryGroupeUser.GetQueryableAsync();

            var query = from u in usersQuery
                        join gu in groupUsersQuery on u.Id equals gu.User_id
                        select u;

            var users = await AsyncExecuter.ToListAsync(query.Distinct());

            return ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(users);
        }
        public async Task AssignTicketToUser(Guid ticketId, Guid userId)
        {
            var ticket = await Repository.GetAsync(ticketId);
            if (ticket == null)
            {
                throw new BusinessException("Ticket not found");
            }

            ticket.AssignedTo = userId;
            await Repository.UpdateAsync(ticket, autoSave: true);
        }
    }
}
