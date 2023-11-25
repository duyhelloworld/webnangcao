using System.Net;
using webnangcao.Exceptions;

namespace webnangcao.Tools;

public class FileTool
{
    public static string PlaylistArtWorkBaseUrl (string fileArtworkName)
    {
        var fileName = Path.GetFileNameWithoutExtension(fileArtworkName);
        return $"http://localhost:5271/playlist/artwork/{fileName}";
    }

    public static string TrackArtworkBaseUrl (string fileArtworkName)
    {
        var fileName = Path.GetFileNameWithoutExtension(fileArtworkName);
        return $"http://localhost:5271/track/artwork/{fileName}";
    }
    
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

    public static async Task SaveArtwork(IFormFile fileInput)
    {
        var fileInputName = fileInput.FileName;
        if (Path.GetExtension(fileInputName) != "jpg" || 
            Path.GetExtension(fileInputName) != "png")
        {
            throw new AppException(HttpStatusCode.UnsupportedMediaType,
                "Ảnh không đúng định dạng", "Vui lòng chọn ảnh khác");
        }
        var filePath = Path.Combine(ArtWorkFolderPath, fileInputName);
        if (File.Exists(filePath))
        {
            throw new AppException(HttpStatusCode.BadRequest,
                "Ảnh này đã tồn tại", "Vui lòng chọn ảnh khác");
        }
        using var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
        await fileInput.CopyToAsync(stream);
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