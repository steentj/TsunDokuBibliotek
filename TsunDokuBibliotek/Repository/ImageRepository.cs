namespace TsundokuBibliotek.Repository;

public partial class ImageRepository
{
    public static async Task<Microsoft.Maui.Graphics.IImage> RetrieveImage()
    {
        try
        {
            var imagePath = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Vælg billede der illustrerer bogen" });

            if (imagePath?.FullPath is null)
                return null;

            Microsoft.Maui.Graphics.IImage image;

            using (Stream stream = File.OpenRead(imagePath.FullPath))
            {
#if WINDOWS
                image = Microsoft.Maui.Graphics.IImage;
#else
                image = Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(stream);
#endif
            }
            return image;
        }
        catch (Exception ex)
        {
            Debugger.Log(1, "Retrieve image error", ex.Message);
            return null;
        }
    }

    public static async Task<string> SaveImage(Microsoft.Maui.Graphics.IImage image, string imageName)
    {
        if (image is null)
            return null;
        
        try
        {
            var imagePath = $"{imageName}.png";
            using var outputStream = File.Open(Path.Combine(FileSystem.AppDataDirectory, imagePath), FileMode.OpenOrCreate);
            await image.SaveAsync(outputStream, ImageFormat.Png);

            return imagePath;
        }
        catch (Exception ex)
        {
            Debugger.Log(1, "Save image error", ex.Message);
            return null;
        }
    }
}
