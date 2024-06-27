using Dynamsoft.License.Maui;
using System.Diagnostics;
namespace BarcodeReaderSimpleSample;

public partial class MainPage : ContentPage, ILicenseVerificationListener
{

	public MainPage()
	{
		InitializeComponent();

		// Initialize license for Dynamsoft Barcode Reader.
		// The license string here is a time-limited trial license. Note that network connection is required for this license to work.
		// You can also request a 30-day trial license via the Request a Trial License link: https://www.dynamsoft.com/customer/license/trialLicense?product=dbr&utm_source=github&package=maui
        LicenseManager.InitLicense("DLS2eyJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSJ9", this);
    }

    private async void OnCameraClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CameraPage());
    }
    
    public void OnLicenseVerified(bool isSuccess, string message)
    {
        if (!isSuccess)
        {
            Debug.WriteLine(message);
        }
    }
}


