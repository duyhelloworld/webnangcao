namespace webnangcao.Entities.Enumerables;

public enum EMaxValue : int
{
    TrackNameLength = 40,
    DirectoryLength = 200,
    CategoryNameLength = 30,
    PlaylistNameLength = 35,
    NumberOfTags = 15,
    AddressLength = 140,
}

public static class EMaxValueExtension {
    public static int ToInt(this EMaxValue eMaxValue)
    {
        return Convert.ToInt32(eMaxValue);
    }
}