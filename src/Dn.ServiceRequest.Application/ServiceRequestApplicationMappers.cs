using Dn.ServiceRequest.Familles;
using Dn.ServiceRequest.Groupes;
using Dn.ServiceRequest.Types;
using Dn.ServiceRequest.Tickets;
using Dn.ServiceRequest.PieceJointes;
using Dn.ServiceRequest.Comments;
using Dn.ServiceRequest.GroupeUsers;

using Dn.ServiceRequest.GroupeTypes;



using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace Dn.ServiceRequest;

[Mapper]
public partial class ServiceRequestApplicationMappers
{
    /* You can configure your Mapperly mapping configuration here.
     * Alternatively, you can split your mapping configurations
     * into multiple mapper classes for a better organization. */
}

    [Mapper]
    public partial class FamilleToFamilleDtoMapper : MapperBase<Famille, FamilleDto>
    {
        public override partial FamilleDto Map(Famille source);
        public override partial void Map(Famille source, FamilleDto destination);
    }
    [Mapper]
    public partial class CreateUpdateFamilleDtoToFamilleMapper : MapperBase<CreateUpdateFamilleDto, Famille>
    {
        public override partial Famille Map(CreateUpdateFamilleDto source);

        public override partial void Map(CreateUpdateFamilleDto source, Famille destination);
    }


    [Mapper]
    public partial class GroupeToGroupeDtoMapper : MapperBase<Groupe, GroupeDto>
    {
        public override partial GroupeDto Map(Groupe source);
        public override partial void Map(Groupe source, GroupeDto destination);
    }
    [Mapper]
    public partial class CreateUpdateGroupeDtoToGroupeMapper : MapperBase<CreateUpdateGroupeDto, Groupe>
    {
        public override partial Groupe Map(CreateUpdateGroupeDto source);

        public override partial void Map(CreateUpdateGroupeDto source, Groupe destination);
    }

    [Mapper]
    public partial class TypeToTypeDtoMapper : MapperBase<Type, TypeDto>
    {
        public override partial TypeDto Map(Type source);
        public override partial void Map(Type source, TypeDto destination);
    }
    [Mapper]
    public partial class CreateUpdateTypeDtoToTypeMapper : MapperBase<CreateUpdateTypeDto, Type>
    {
        public override partial Type Map(CreateUpdateTypeDto source);

        public override partial void Map(CreateUpdateTypeDto source, Type destination);
    }

    [Mapper]
    public partial class TicketToTicketDtoMapper : MapperBase<Ticket, TicketDto>
    {
        public override partial TicketDto Map(Ticket source);
        public override partial void Map(Ticket source, TicketDto destination);
    }
    [Mapper]
    public partial class CreateUpdateTicketDtoToTicketMapper : MapperBase<CreateUpdateTicketDto, Ticket>
    {
        public override partial Ticket Map(CreateUpdateTicketDto source);

        public override partial void Map(CreateUpdateTicketDto source, Ticket destination);
    }

    //
    [Mapper]
    public partial class PieceJointeToPieceJointeDtoMapper : MapperBase<PieceJointe, PieceJointeDto>
    {
        public override partial PieceJointeDto Map(PieceJointe source);
        public override partial void Map(PieceJointe source, PieceJointeDto destination);
    }
    //
    [Mapper]
    public partial class CreateUpdatePieceJointeDtoToPieceJointeMapper : MapperBase<CreateUpdatePieceJointeDto, PieceJointe>
    {
        public override partial PieceJointe Map(CreateUpdatePieceJointeDto source);

        public override partial void Map(CreateUpdatePieceJointeDto source, PieceJointe destination);
    }

    //
      [Mapper]
    public partial class CommentToCommentDtoMapper : MapperBase<Comment, CommentDto>
    {
        public override partial CommentDto Map(Comment source);
        public override partial void Map(Comment source, CommentDto destination);
    }
    //
    [Mapper]
    public partial class CreateUpdateCommentDtoToCommentMapper : MapperBase<CreateUpdateCommentDto, Comment>
    {
        public override partial Comment Map(CreateUpdateCommentDto source);

        public override partial void Map(CreateUpdateCommentDto source, Comment destination);
    }
    
    //
    [Mapper]
    public partial class GroupeUserToGroupeUserDtoMapper : MapperBase<GroupeUser, GroupeUserDto>
    {
        public override partial GroupeUserDto Map(GroupeUser source);
        public override partial void Map(GroupeUser source, GroupeUserDto destination);
    }
    //
    [Mapper]
    public partial class CreateUpdateGroupeUserDtoToGroupeUserMapper : MapperBase<CreateUpdateGroupeUserDto, GroupeUser>
    {
        public override partial GroupeUser Map(CreateUpdateGroupeUserDto source);

        public override partial void Map(CreateUpdateGroupeUserDto source, GroupeUser destination);
    }
  
    //
    [Mapper]
    public partial class GroupeTypeToGroupeTypeDtoMapper : MapperBase<GroupeType, GroupeTypeDto>
    {
        public override partial GroupeTypeDto Map(GroupeType source);
        public override partial void Map(GroupeType source, GroupeTypeDto destination);
    }
    //
    [Mapper]
    public partial class CreateUpdateGroupeTypeDtoToGroupeTypeMapper : MapperBase<CreateUpdateGroupeTypeDto, GroupeType>
    {
        public override partial GroupeType Map(CreateUpdateGroupeTypeDto source);

        public override partial void Map(CreateUpdateGroupeTypeDto source, GroupeType destination);
    }