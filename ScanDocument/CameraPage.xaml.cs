using Dynamsoft.Core.Maui;
using Dynamsoft.CameraEnhancer.Maui;
using Dynamsoft.CaptureVisionRouter.Maui;
using Dynamsoft.License.Maui;
using Dynamsoft.DocumentNormalizer.Maui;

namespace ScanDocument;

public partial class CameraPage : ContentPage, ICapturedResultReceiver, ICompletionListener, ILicenseVerificationListener
{
	CameraEnhancer enhancer;
    CaptureVisionRouter router;
    bool isClicked;
	public CameraPage()
	{
		InitializeComponent();
		// Initialize the license.
        // The license string here is a trial license. Note that network connection is required for this license to work.
        // You can request an extension via the following link: https://www.dynamsoft.com/customer/license/trialLicense?product=ddn&utm_source=samples&package=mobile
        LicenseManager.InitLicense("DLS2eyJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSJ9", this);
		enhancer = new CameraEnhancer();
        router = new CaptureVisionRouter();
        router.SetInput(enhancer);
        router.AddResultReceiver(this);
	}

	protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        if (this.Handler != null)
        {
            enhancer.SetCameraView(camera);
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Permissions.RequestAsync<Permissions.Camera>();
        enhancer?.Open();
        router?.StartCapturing(EnumPresetTemplate.PT_DETECT_DOCUMENT_BOUNDARIES, this);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        enhancer?.Close();
        router?.StopCapturing();
    }

    public void OnProcessedDocumentResultReceived(ProcessedDocumentResult result)
    {
        if (isClicked)
        {
            isClicked = false;
            if (result?.DetectedQuadResultItems?.Length > 0)
            {
                var data = router.GetIntermediateResultManager().GetOriginalImage(result.OriginalImageHashId);
                if (data != null)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PushAsync(new EditorPage(data, result.DetectedQuadResultItems));
                    });
                }
            }
        }
    }

    void OnCaptureBtnClicked(System.Object sender, System.EventArgs e)
    {
        isClicked = true;
    }
	
	public void OnLicenseVerified(bool isSuccess, string message)
    {
        if (!isSuccess)
        {
			Console.WriteLine("License initialization failed: " + message);
        }
    }

    public void OnFailure(int errorCode, string errorMessage)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            DisplayAlert("Error", errorMessage, "OK");
        });
    }
}