using System.Runtime.Serialization;

namespace webnangcao.Entities.Enumerables;

public enum ERole
{
    [EnumMember(Value = "SuperAdmin")]
    SuperAdmin,

    [EnumMember(Value = "Admin")]
    Admin,

    [EnumMember(Value = "User")]
    User
}