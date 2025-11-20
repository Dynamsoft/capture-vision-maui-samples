using Dynamsoft.Core.Maui;
using Dynamsoft.CameraEnhancer.Maui;
using Dynamsoft.Utility.Maui;

namespace ScanDocument;

public partial class EditorPage : ContentPage
{
    private ImageData _originalImage;
    private Quadrilateral _deskewSourceQuad;
    private readonly Action<ImageData, Quadrilateral> _returnResult;

    public EditorPage(ImageData originalImage, Quadrilateral deskewSourceQuad, Action<ImageData, Quadrilateral> returnResult)
	{
		InitializeComponent();
        _originalImage = originalImage;
        _deskewSourceQuad = deskewSourceQuad;
        _returnResult = returnResult;
	}

    protected override void OnHandlerChanged()
    {
        EditorView.OriginalImage = _originalImage;
        var drawingLayer = EditorView.GetDrawingLayer(EnumDrawingLayerId.DLI_DDN);
        var drawingItems = new List<DrawingItem> { new QuadDrawingItem(_deskewSourceQuad) };
        drawingLayer.DrawingItems = drawingItems;
    }

    private void OnBtnConfirmClicked(Object sender, EventArgs e)
    {
        var drawingLayer = EditorView.GetDrawingLayer(EnumDrawingLayerId.DLI_DDN);
        var drawingItem = EditorView.GetSelectedDrawingItem();
        if (drawingItem == null && drawingLayer.DrawingItems.Count > 0)
        {
            drawingItem = drawingLayer.DrawingItems[0];
        }
        var selectedQuad = (drawingItem as QuadDrawingItem)!.Quad;
        var processor = new ImageProcessor();
        try
        {
            var croppedImage = processor.CropAndDeskewImage(_originalImage, selectedQuad)!;
            _returnResult.Invoke(croppedImage, selectedQuad);
            Navigation.PopAsync();
        }
        catch (CoreException exception)
        {
            DisplayAlert("", "The selected area is close to a triangle, please change it to a quadrilateral.", "OK");
        }
    }
}