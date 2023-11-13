namespace webnangcao.Tools;

public class FileTool
{
    private static readonly string ArtWorkFolderPath
        = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

    private static readonly string TrackFolderPath
        = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "musics");

    public static Stream ReadArtWork(string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return Stream.Null;
        return new FileStream(Path.Combine(ArtWorkFolderPath, fileName), FileMode.Open);
    }

    public static Stream ReadTrack(string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return Stream.Null;
        return new FileStream(Path.Combine(TrackFolderPath, fileName), FileMode.Open);
    }

    public static async Task<string> SaveArtwork(IFormFile file)
    {
        var fileName = file.FileName;
        var filePath = Path.Combine(ArtWorkFolderPath, fileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        return fileName;
    }

    public static async Task<string> SaveTrack(IFormFile file)
    {
        var fileName = file.FileName;
        var filePath = Path.Combine(ArtWorkFolderPath, fileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        return fileName;
    }
}