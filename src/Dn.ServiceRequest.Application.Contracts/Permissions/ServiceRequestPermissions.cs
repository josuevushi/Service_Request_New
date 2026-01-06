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
    }
}
