namespace TsundokuBibliotek.Repository;

public partial class ImageRepository
{
    public static async Task<Graphics.IImage> RetrieveImage()
    {
        try
        {
            var imagePath = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Vælg billede der illustrerer bogen" });

            if (imagePath?.FullPath is null)
                return null;

            Graphics.IImage image;

            using (Stream stream = File.OpenRead(imagePath.FullPath))
            {
                image = PlatformImage.FromStream(stream);
            }
            return image;
        }
        catch (Exception ex)
        {
            Debugger.Log(1, "Retrieve image error", ex.Message);
            return null;
        }
    }

    public static async Task<string> SaveImage(Graphics.IImage image, string imageName)
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
