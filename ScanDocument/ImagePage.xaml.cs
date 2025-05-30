using Dynamsoft.Core.Maui;
using Dynamsoft.CameraEnhancer.Maui;
using Dynamsoft.CaptureVisionRouter.Maui;
using Dynamsoft.DocumentNormalizer.Maui;

namespace ScanDocument;

public partial class ImagePage : ContentPage
{
	ImageData data;
    Quadrilateral quadrilateral;
	CaptureVisionRouter cvr;

	public ImagePage(ImageData data, Quadrilateral quadrilateral)
	{
		InitializeComponent();
		this.data = data;
        this.quadrilateral = quadrilateral;
		this.cvr = new CaptureVisionRouter();
	}

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
		normalize(EnumImageColourMode.ICM_Colour);
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            if (button.Text == "gray")
            {
                normalize(EnumImageColourMode.ICM_GRAYSCALE);
            }
            else if (button.Text == "color")
            {
                normalize(EnumImageColourMode.ICM_Colour);
            }
            else if (button.Text == "binary")
            {
                normalize(EnumImageColourMode.ICM_BINARY);
            }
        }
    }

    private void normalize(EnumImageColourMode type)
	{
		var name = EnumPresetTemplate.PT_NORMALIZE_DOCUMENT;
		var settings = cvr.GetSimplifiedSettings(name);
		settings.DocumentSettings.ColourMode = type;
        settings.Roi = quadrilateral;
        settings.RoiMeasuredInPercentage = false;
		cvr.UpdateSettings(name, settings);
		var result = cvr.Capture(data, name);
		if (result?.Items?.Length > 0)
		{
			foreach (var item in result.Items)
			{
				if (item.Type == EnumCapturedResultItemType.EnhancedImage) 
				{
					EnhancedImageResultItem enhancedImage = (EnhancedImageResultItem)item;
					image.Source = enhancedImage.ImageData?.ToImageSource();
                    return;
				}
			}
        }
	}
}