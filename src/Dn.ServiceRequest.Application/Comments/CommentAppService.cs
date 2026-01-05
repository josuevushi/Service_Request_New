using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dn.ServiceRequest.Tickets;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Dn.ServiceRequest.Comments
{
    public class CommentAppService :
    CrudAppService<
        Comment, 
        CommentDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateCommentDto>, 
    ICommentAppService 
    {
        
        private readonly IRepository<Ticket, Guid> _ticketRepository;
        private readonly IRepository<IdentityUser, Guid> _userRepository;

        public CommentAppService( IRepository<IdentityUser, Guid> userRepository,IRepository<Comment, Guid> repository,IRepository<Ticket, Guid> ticketRepository) : base(repository)
        {
            _ticketRepository=ticketRepository;
            _userRepository=userRepository;
        }

        public async Task<List<dynamic>> GetCommentJoin(string TicketId)
        {
            var tickets = await _ticketRepository.GetQueryableAsync();
            var users = await _userRepository.GetQueryableAsync();
            var commentsQuery = await Repository.GetQueryableAsync();

            var comments = (from com in commentsQuery
                            join ticks in tickets on com.Ticket_Id equals ticks.Id
                            join usr in users on com.CreatorId equals usr.Id
                            where com.Ticket_Id == Guid.Parse(TicketId)
                            select new
                            {
                                Date = com.CreationTime,
                                Commentaire = com.Text,
                                User = usr.UserName
                            }).ToList<dynamic>(); // <-- transformer en List<dynamic>

            return comments;
         }
          public async Task<Comment> GetAddCommentJoin(CommentAddDto monComment)
        {
           Comment com = new Comment();
           com.Ticket_Id= Guid.Parse(monComment.TicketId);
           com.Text=monComment.Text;
            
            return await Repository.InsertAsync(com);
         }
    }
}