using Dynamsoft.Core.Maui;
using Dynamsoft.CameraEnhancer.Maui;
using Dynamsoft.CaptureVisionRouter.Maui;
using Dynamsoft.DocumentNormalizer.Maui;
namespace AutoNormalize;

public partial class ImagePage : ContentPage
{
	ImageData data;
	CaptureVisionRouter cvr;

	public ImagePage(ImageData data)
	{
		InitializeComponent();
		this.data = data;
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
		cvr.UpdateSettings(name, settings);
		var result = cvr.Capture(data, name);
		if (result?.Items?.Count > 0 && result.Items[0].Type == EnumCapturedResultItemType.CRIT_NORMALIZED_IMAGE)
		{
			NormalizedImageResultItem item = (NormalizedImageResultItem)result.Items[0];
            image.Source = item.ImageData.ToImageSource();
        }
	}
}
