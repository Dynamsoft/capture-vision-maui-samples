using Dynamsoft.License.Maui;
using System.Diagnostics;
namespace BarcodeReaderSimpleSample;

public partial class MainPage : ContentPage, ILicenseVerificationListener
{

	public MainPage()
	{
		InitializeComponent();
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


