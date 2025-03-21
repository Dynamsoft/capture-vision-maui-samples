﻿using Dynamsoft.Core.Maui;
using Dynamsoft.CaptureVisionRouter.Maui;
using Dynamsoft.CameraEnhancer.Maui;
using Dynamsoft.Utility.Maui;
using Dynamsoft.LabelRecognizer.Maui;
using Dynamsoft.CodeParser.Maui;
using DynamsoftVIN;

namespace VINScanner;

public partial class CameraPage : ContentPage, ICapturedResultReceiver, ICompletionListener
{
    public static CameraEnhancer enhancer;
    CaptureVisionRouter router;

    public CameraPage()
    {
        InitializeComponent();
        enhancer = new CameraEnhancer();
        router = new CaptureVisionRouter();
        router.SetInput(enhancer);
        router.AddResultReceiver(this);
        var filter = new MultiFrameResultCrossFilter();
        filter.EnableResultCrossVerification(EnumCapturedResultItemType.CRIT_BARCODE, true);
        filter.EnableResultCrossVerification(EnumCapturedResultItemType.CRIT_TEXT_LINE, true);
        router.AddResultFilter(filter);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        if (this.Handler != null)
        {
            enhancer.SetCameraView(camera);
            var region = new DMRect(0.1f, 0.4f, 0.9f, 0.6f, true);
            enhancer.SetScanRegion(region);
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Permissions.RequestAsync<Permissions.Camera>();
        enhancer?.Open();
        router?.StartCapturing("ReadVIN", this);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        enhancer?.Close();
        router?.StopCapturing();
    }
    
    public void OnParsedResultsReceived(ParsedResult result)
    {
        if (result?.Items?.Count > 0)
        {
            ParsedResultItem parsedResultItem = result.Items[0];
            if (result.Items.Count > 1) {
                foreach (var item in result.Items)
                {
                    if (item.TaskName == "parse-vin-barcode") {
                        parsedResultItem = item;
                        break;
                    }
                }
            }
            var dictionary = ConvertToVINDictionary(parsedResultItem);
            if (dictionary != null) {
                router?.StopCapturing();
                enhancer?.ClearBuffer();
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new ResultPage(dictionary));
                });
            }
        }
    }

    public Dictionary<string, string>? ConvertToVINDictionary(ParsedResultItem item) 
    {
        if (item.ParsedFields.TryGetValue("vinString", out ParsedField? value) && value != null) {
            Dictionary<string, string> dic = [];
            string[] infoLists = ["vinString", "WMI", "region", "VDS", "checkDigit", "modelYear", "plantCode", "serialNumber"];
            foreach (var info in infoLists)
            {
                if(item.ParsedFields.TryGetValue(info, out ParsedField? field) && field != null) 
                {
                    if (item.ParsedFields[info].ValidationStatus == EnumValidationStatus.VS_FAILED) {
                        return null;
                    } else {
                        dic.Add(CapitalizeFirstLetter(info), item.ParsedFields[info].Value);
                    }
                }
            }
            return dic;
        } else {
            return null;
        }
    }

    public static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        return char.ToUpper(input[0]) + input.Substring(1);
    }


    public void OnFailure(int errorCode, string errorMessage)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            DisplayAlert("Error", errorMessage, "OK");
        });
    }
}
