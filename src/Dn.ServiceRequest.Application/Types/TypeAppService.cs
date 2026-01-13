using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dn.ServiceRequest.Familles;
using Dn.ServiceRequest.Groupes;
using Dn.ServiceRequest.GroupeTypes;
using Dn.ServiceRequest.GroupeUsers;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Dn.ServiceRequest.Types
{
    public class TypeAppService :
    CrudAppService<
        Type, 
        TypeDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateTypeDto>, 
    ITypeAppService 
    {
    private readonly    IRepository<Groupe, Guid> _repositoryGroupe;
    private readonly    IRepository<GroupeType, Guid> _repositoryGroupeType;
    private readonly IRepository<Famille, Guid> _repositoryFamille;
    private readonly   IRepository<GroupeUser, Guid> _repositoryGroupeUser;
    private readonly IRepository<IdentityUser, Guid> _userRepository;



        public TypeAppService(IRepository<IdentityUser, Guid> userRepository,IRepository<GroupeUser, Guid> repositoryGroupeUser,IRepository<Famille, Guid> repositoryFamille,IRepository<GroupeType, Guid> repositoryGroupeType,IRepository<Groupe, Guid> repositoryGroupe, IRepository<Type, Guid> repository) : base(repository)
        {
            _repositoryGroupe=repositoryGroupe;
            _repositoryGroupeType=repositoryGroupeType;
            _repositoryFamille=repositoryFamille;
            _repositoryGroupeUser=repositoryGroupeUser;
            _userRepository=userRepository;

        }
        
        public override async Task<PagedResultDto<TypeDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            if (input.MaxResultCount < 1000)
            {
                input.MaxResultCount = 1000;
            }
            return await base.GetListAsync(input);
        }
public async Task<dynamic> GetTypeDetails(string typeId)
{
    var groupes = await _repositoryGroupe.GetQueryableAsync();
    var groupeTypes = await _repositoryGroupeType.GetQueryableAsync();
    var types = await Repository.GetQueryableAsync();
    var familles = await _repositoryFamille.GetQueryableAsync();
    var grpUsers = await _repositoryGroupeUser.GetQueryableAsync();
    var users = await _userRepository.GetQueryableAsync();

    Guid typeGuid = Guid.Parse(typeId);

    var result =
        from type in types
        join grpTyp in groupeTypes on type.Id equals grpTyp.Type_id
        join grp in groupes on grpTyp.Groupe_id equals grp.Id
        join fmll in familles on type.Famille_id equals fmll.Id
        join grpUsr in grpUsers on grp.Id equals grpUsr.Groupe_id
        join usr in users on grpUsr.User_id equals usr.Id
        where grpUsr.Is_Receiver == true
             // && 
              where type.Id == typeGuid
        select new
        {
            Type=type.Nom,
            Groupe = grp.Nom,
            Categorie = fmll.Nom,
            Sla = type.Sla,
            User = usr.Email
        };

    return await AsyncExecuter.ToListAsync(result);
}

    }
}