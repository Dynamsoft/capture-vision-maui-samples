using Dynamsoft.Core.Maui;
using Dynamsoft.CameraEnhancer.Maui;
using Dynamsoft.CaptureVisionRouter.Maui;
using Dynamsoft.License.Maui;
using Dynamsoft.DocumentNormalizer.Maui;
using Dynamsoft.Utility.Maui;

namespace ScanDocument;

public partial class CameraPage : ContentPage, ICapturedResultReceiver, ICompletionListener,
    ILicenseVerificationListener
{
    private readonly CameraEnhancer _camera;
    private readonly CaptureVisionRouter _router;
    private bool _isClicked;

    public CameraPage()
    {
        InitializeComponent();
        // Initialize the license.
        // The license string here is a trial license. Note that network connection is required for this license to work.
        // You can request an extension via the following link: https://www.dynamsoft.com/customer/license/trialLicense?product=ddn&utm_source=samples&package=mobile
        LicenseManager.InitLicense("DLS2eyJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSJ9", this);
        _camera = new CameraEnhancer();
        _router = new CaptureVisionRouter();
        _router.SetInput(_camera);
        _router.AddResultReceiver(this);
        var filter = new MultiFrameResultCrossFilter();
        filter.EnableResultCrossVerification(EnumCapturedResultItemType.DeskewedImage, true);
        _router.AddResultFilter(filter);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        if (this.Handler != null)
        {
            _camera.SetCameraView(CameraView);
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Permissions.RequestAsync<Permissions.Camera>();
        _camera.Open();
        _router.StartCapturing(EnumPresetTemplate.PT_DETECT_AND_NORMALIZE_DOCUMENT, this);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _camera.Close();
        _router.StopCapturing();
    }

    public void OnProcessedDocumentResultReceived(ProcessedDocumentResult result)
    {
        if (!(result?.DeskewedImageResultItems?.Length > 0)) return;
        if (result.DeskewedImageResultItems[0].CrossVerificationStatus == EnumCrossVerificationStatus.Passed || _isClicked)
        {
            _isClicked = false;
            var originalImage = _router.GetIntermediateResultManager().GetOriginalImage(result.OriginalImageHashId);
            var deskewedImage = result.DeskewedImageResultItems[0].ImageData;
            var sourceDeskewedQuad = result.DeskewedImageResultItems[0].SourceDeskewQuad;
            _router.StopCapturing();
            if (originalImage != null && deskewedImage != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                    Navigation.PushAsync(new ImagePage(originalImage, deskewedImage, sourceDeskewedQuad))
                );
            }
        }
    }

    private void OnCaptureBtnClicked(Object sender, EventArgs e)
    {
        _isClicked = true;
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
        MainThread.BeginInvokeOnMainThread(() => { DisplayAlert("Error", errorMessage, "OK"); });
    }
}