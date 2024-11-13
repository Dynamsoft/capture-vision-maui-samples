using Dynamsoft.Core.Maui;
using Dynamsoft.CaptureVisionRouter.Maui;
using Dynamsoft.DocumentNormalizer.Maui;
using Dynamsoft.CameraEnhancer.Maui;
using Dynamsoft.Utility.Maui;

namespace EditAndNormalize;

public partial class CameraPage : ContentPage, ICapturedResultReceiver, ICompletionListener
{
    public static CameraEnhancer enhancer;
    CaptureVisionRouter router;
    bool isClicked;

    public CameraPage()
    {
        InitializeComponent();
        enhancer = new CameraEnhancer();
        router = new CaptureVisionRouter();
        router.SetInput(enhancer);
        router.AddResultReceiver(this);
        //var filter = new MultiFrameResultCrossFilter();
        //filter.EnableResultCrossVerification(EnumCapturedResultItemType.CRIT_NORMALIZED_IMAGE, true);
        //router.AddResultFilter(filter);
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

    public void OnDetectedQuadsReceived(DetectedQuadsResult result)
    {
        if (isClicked)
        {
            isClicked = false;
            if (result?.Items?.Count > 0)
            {
                var data = router.GetIntermediateResultManager().GetOriginalImage(result.OriginalImageHashId);
                if (data != null)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PushAsync(new EditorPage(data, result.Items));
                    });
                }
            }
        }
    }

    public void OnFailure(int errorCode, string errorMessage)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            DisplayAlert("Error", errorMessage, "OK");
        });
    }

    void OnCaptureBtnClicked(System.Object sender, System.EventArgs e)
    {
        isClicked = true;
    }
}
