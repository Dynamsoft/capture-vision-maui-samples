﻿using Dynamsoft.Core.Maui;
using Dynamsoft.DocumentNormalizer.Maui;
using Dynamsoft.CameraEnhancer.Maui;
namespace EditAndNormalize;

public partial class EditorPage : ContentPage
{
    ImageData data;
    List<DetectedQuadResultItem> items;

    public EditorPage(ImageData data, List<DetectedQuadResultItem> items)
	{
		InitializeComponent();
        this.data = data;
        this.items = items;
	}

    protected override void OnHandlerChanged()
    {
        setUp();
    }

    void setUp()
    {
        editorView.OriginalImage = data;
        var layer = editorView.GetDrawingLayer(EnumDrawingLayerId.DLI_DDN);
        IList<DrawingItem> drawingItems = new List<DrawingItem>();
        foreach (DetectedQuadResultItem item in this.items)
        {
            drawingItems.Add(new QuadDrawingItem(item.Location));
        }
        layer.DrawingItems = drawingItems;
    }

    void OnNormalizeBtnClicked(System.Object sender, System.EventArgs e)
    {
        var item = editorView.GetSelectedDrawingItem();
        if (item == null)
        {
            item = editorView.GetDrawingLayer(EnumDrawingLayerId.DLI_DDN).DrawingItems[0];
        }
        if (item?.MediaType == EnumDrawingItemMediaType.DIMT_QUADRILATERAL)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new ImagePage(data, ((QuadDrawingItem)item).Quad));
            });
        }
    }
}
