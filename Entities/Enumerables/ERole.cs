using System.Runtime.Serialization;

namespace webnangcao.Entities.Enumerables;

public enum ERole
{
    [EnumMember(Value = "SUPERADMIN")]
    SUPERADMIN,

    [EnumMember(Value = "ADMIN")]
    ADMIN,

    [EnumMember(Value = "USER")]
    USER
}