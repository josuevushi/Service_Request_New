namespace Dn.ServiceRequest.Permissions;

public static class ServiceRequestPermissions
{
    public const string GroupName = "ServiceRequest";

    public static class Tickets
    {
        public const string Default = GroupName + ".Tickets";
        public const string Traiter = Default + ".Traiter";
        public const string Start = Traiter + ".Start";
        public const string Pending = Traiter + ".Pending";
        public const string Close = Traiter + ".Close";
        public const string Transfert = Traiter + ".Transfert";
        public const string GlobalSearch = Default + ".GlobalSearch";
    }

    public static class Familles
    {
        public const string Default = GroupName + ".Familles";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Types
    {
        public const string Default = GroupName + ".Types";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Groupes
    {
        public const string Default = GroupName + ".Groupes";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class GroupeUsers
    {
        public const string Default = GroupName + ".GroupeUsers";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class GroupeTypes
    {
        public const string Default = GroupName + ".GroupeTypes";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Rapports
    {
        public const string Default = GroupName + ".Rapports";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string AssignRole = Default + ".AssignRole";
    }
}
