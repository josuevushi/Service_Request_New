using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Dn.ServiceRequest.PieceJointes
{
    public class PieceJointeAppService :
        CrudAppService<
            PieceJointe,
            PieceJointeDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdatePieceJointeDto>,
        IPieceJointeAppService
    {
        private readonly IConfiguration _configuration;

        public PieceJointeAppService(
            IRepository<PieceJointe, Guid> repository, // ✅ Repository correct
            IConfiguration configuration
        ) : base(repository)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Sauvegarde un tableau de fichiers encodés en Base64 dans le répertoire configuré
        /// </summary>
        /// <param name="files">Liste des fichiers à sauvegarder</param>
         public async Task<string> PostNewFileAsync(FileDto data)
        {
          return data.FileName;

        }

        public async Task<string> BAsync()
        {
          /*  var path = _configuration.GetValue<string>("FileSettings:UploadPath");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);                
                var filePath = Path.Combine(path, file.FileName);
                var bytes = Convert.FromBase64String(file.Base64);
                File.WriteAllBytes(filePath, bytes);
            }*/

            return null;
        }

        public async Task<List<PieceJointeDto>> GetListByTicketId(Guid ticketId)
        {
            var queryable = await Repository.GetQueryableAsync();
            var items = await AsyncExecuter.ToListAsync(queryable.Where(p => p.Ticket_id == ticketId));
            return ObjectMapper.Map<List<PieceJointe>, List<PieceJointeDto>>(items);
        }

        public async Task<PieceJointeDto> GetAddPieceJointe(string ticketId, string nom, string path)
        {
            var pieceJointe = new PieceJointe
            {
                Ticket_id = Guid.Parse(ticketId),
                Nom = nom,
                Path = path
            };

            await Repository.InsertAsync(pieceJointe);

            return ObjectMapper.Map<PieceJointe, PieceJointeDto>(pieceJointe);
        }
    }
}

