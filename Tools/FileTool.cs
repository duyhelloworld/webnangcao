using System.Net;
using webnangcao.Exceptions;

namespace webnangcao.Tools;

public class FileTool
{
    private static readonly string ArtWorkFolderPath
        = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "artworks");

    private static readonly string TrackFolderPath
        = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "musics");

    private static readonly string AvatarFolderPath
        = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatars");

    public static readonly string DefaultAvatar = "default-avatar.jpg";
    public static readonly string DefaultArtWork = "default-artwork.jpg";
    private static Stream ReadFile(string? fileName, string folder, string? defaultFile)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                // Nếu server có ảnh default
                if (defaultFile != null && Path.Exists(Path.Combine(folder, defaultFile)))
                    return new FileStream(
                        Path.Combine(folder, defaultFile), FileMode.Open);
                return Stream.Null;
            }
            if (fileName.Contains(' '))
            {
                fileName = fileName.Replace(" ", "_");
            }
            return new FileStream(
                Path.Combine(folder, fileName), FileMode.Open);
        }
        catch (FileNotFoundException)
        {
            throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy file yêu cầu");
        }
    }

    private static async Task SaveImage(IFormFile? fileInput, string folder)
    {
        if (fileInput == null)
        {
            return;
        }
        var fileInputName = fileInput.FileName;
        if (Path.GetExtension(fileInputName) == ".jpg" ||
            Path.GetExtension(fileInputName) == ".png")
        {
            if (fileInputName.Contains(' '))
            {
                fileInputName = fileInputName.Replace(" ", "_");
            }
            var filePath = Path.Combine(folder, fileInputName);
            if (File.Exists(filePath))
            {
                throw new AppException(HttpStatusCode.BadRequest,
                    "Ảnh này đã tồn tại. Vui lòng chọn ảnh khác");
            }
            using var stream = new FileStream(filePath, FileMode.Create);
            await fileInput.CopyToAsync(stream);
            return;
        }
        throw new AppException(HttpStatusCode.UnsupportedMediaType,
            "Ảnh không đúng định dạng. Vui lòng chọn ảnh đuôi .png hoặc .jpg");
    }

    public static Stream ReadAvatar(string? fileName)
    {
        return ReadFile(fileName, AvatarFolderPath, DefaultAvatar);
    }

    public static Stream ReadArtwork(string? fileName)
    {
        return ReadFile(fileName, ArtWorkFolderPath, DefaultArtWork);
    }

    public static Stream ReadTrack(string? fileName)
    {
        return ReadFile(fileName, TrackFolderPath, null);
    }


    public static async Task SaveTrack(IFormFile file)
    {
        if (!file.FileName.EndsWith(".mp3"))
        {
            throw new AppException(HttpStatusCode.UnsupportedMediaType,
                "File không đúng định dạng. Vui lòng chọn file đuôi .mp3");
        }
        using var stream = new FileStream(
            Path.Combine(TrackFolderPath, file.FileName), FileMode.Create);
        await file.CopyToAsync(stream);
    }

    public static async Task SaveAvatar(IFormFile? file)
    {
        await SaveImage(file, AvatarFolderPath);
    }

    public static async Task SaveArtwork(IFormFile? file)
    {
        await SaveImage(file, ArtWorkFolderPath);
    }

    public static async Task DeleteFile(string? fileName, string folder)
    {
        if (string.IsNullOrWhiteSpace(fileName) || fileName == DefaultAvatar || fileName == DefaultArtWork)
        {
            // Không xóa ảnh mặc định
            return;
        }

        try
        {
            var filePath = Path.Combine(folder, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                await Task.CompletedTask;
            }
            else
            {
                throw new AppException(HttpStatusCode.NotFound,
                    "Không tìm thấy file để xóa");
            }
        }
        catch (Exception ex)
        {
            // Xử lý các trường hợp xóa thất bại
            System.Console.WriteLine(ex);
            throw new AppException(HttpStatusCode.InternalServerError,
                "Lỗi xóa file");
        }
    }

    public static async Task DeleteAvatar(string? fileName)
    {
        await DeleteFile(fileName, AvatarFolderPath);
    }

    public static async Task DeleteArtwork(string? fileName)
    {
        await DeleteFile(fileName, ArtWorkFolderPath);
    }

}
