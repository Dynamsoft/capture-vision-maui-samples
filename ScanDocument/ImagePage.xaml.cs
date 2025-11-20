using Dynamsoft.Core.Maui;
using Dynamsoft.CameraEnhancer.Maui;
using Dynamsoft.CaptureVisionRouter.Maui;
using Dynamsoft.DocumentNormalizer.Maui;
using Dynamsoft.Utility.Maui;
using Microsoft.Maui.Storage;
using System.IO;

namespace ScanDocument;

public partial class ImagePage : ContentPage
{
    private readonly ImageProcessor _processor = new();
    private readonly ImageData _originalImage;
    private ImageData _deskewedColorfulImage;
    private ImageData _showingImageData;
    private Quadrilateral _deskewSourceQuad;


    public ImagePage(ImageData originalImage, ImageData deskewedImage, Quadrilateral deskewSourceQuad)
    {
        InitializeComponent();
        _originalImage = originalImage;
        _deskewedColorfulImage = deskewedImage;
        _deskewSourceQuad = deskewSourceQuad;
        _showingImageData = _deskewedColorfulImage;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        Image.Source = _showingImageData.ToImageSource();
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditorPage(_originalImage, _deskewSourceQuad, (deskewedImageData, updatedQuad) =>
        {
            _deskewedColorfulImage = deskewedImageData;
            _deskewSourceQuad = updatedQuad;

            //Update UI
            _showingImageData = _deskewedColorfulImage;
            Image.Source = _showingImageData.ToImageSource();
        }));
    }

    private async void OnColourModeClicked(object sender, EventArgs e)
    {
        var selectedColorMode = await DisplayActionSheet("Select Colour Mode", "Cancel", null, "Colorful", "Grayscale", "Binary");
        if (string.IsNullOrEmpty(selectedColorMode) || selectedColorMode == "Cancel") return;
        _showingImageData = selectedColorMode switch
        {
            "Colorful" => _deskewedColorfulImage,
            "Grayscale" => _processor.ConvertToGray(_deskewedColorfulImage)!,
            "Binary" => _processor.ConvertToBinaryLocal(_deskewedColorfulImage, 0, 15, false)!,
            _ => _showingImageData
        };
        Image.Source = _showingImageData.ToImageSource();
    }
    
    private void OnExportClicked(object sender, EventArgs e)
    {
        var timestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var fileName = "dynamsoft_output_"+timestampMs+".png";
        var savedPath =
#if ANDROID
            Platform.CurrentActivity!.ExternalCacheDir!.AbsolutePath + fileName;
#elif IOS
            Path.Combine(FileSystem.AppDataDirectory, fileName);
#endif
        new DMImageIO().SaveToFile(_showingImageData, savedPath, true);
        DisplayAlert("Successfully saved", "Has been saved to " + savedPath, "OK");
    }
}