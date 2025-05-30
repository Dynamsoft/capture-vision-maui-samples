# Dynamsoft Capture Vision samples for MAUI edition

This repository contains multiple samples that demonstrate how to use the [Dynamsoft Capture Vision](https://www.dynamsoft.com/capture-vision/docs/mobile/programming/maui/) MAUI Edition.

- [User Guide](https://www.dynamsoft.com/capture-vision/docs/mobile/programming/maui/user-guide/)
- [API Reference](https://www.dynamsoft.com/capture-vision/docs/mobile/programming/maui/api-reference/)

## System Requirements

### .Net

- .NET 8.0 and above.

### Android

- Supported OS: **Android 5.0** (API Level 21) or higher.
- Supported ABI: **armeabi-v7a**, **arm64-v8a**, **x86** and **x86_64**.
- Development Environment: Visual Studio 2022.
- JDK: 1.8+

### iOS

- Supported OS: **iOS 13.0** or higher.
- Supported ABI: **arm64** and **x86_64**.
- Development Environment: Visual Studio 2022 for Mac and Xcode 14.3+.

## Samples

| Sample Name | Description |
| ----------- | ----------- |
| `ScanDocument` | This sample demonstrates how to automatically detect and normalize documents in video streams while also adjusting their boundaries.  |
| `ScanVIN` | Scan the VIN code from a barcode or a text line and extract the vehicle information. |

## Installation

### Visual Studio for Mac

In the **NuGet Package Manager>Manage Packages for Solution** of your project, search for **Dynamsoft.CaptureVisionBundle.Maui**. Select it and click **install**.

### Visual Studio for Windows

You have to Add the library via the project file and do some additional steps to complete the installation.

1. Add the library in the project file:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        ...
        <ItemGroup>
            ...
            <PackageReference Include="Dynamsoft.CaptureVisionBundle.Maui" Version="3.0.3100" />
        </ItemGroup>
    </Project>
    ```

2. Open the **Package Manager Console** and run the following commands:

    ```bash
    dotnet build
    ```

> Note:
>
> - Windows system have a limitation of 260 characters in the path. If you don't use console to install the package, you will receive error "Could not find a part of the path 'C:\Users\admin\.nuget\packages\dynamsoft.imageprocessing.ios\2.2.300\lib\net7.0-ios16.1\Dynamsoft.ImageProcessing.iOS.resources\DynamsoftImageProcessing.xcframework\ios-arm64\dSYMs\DynamsoftImageProcessing.framework.dSYM\Contents\Resources\DWARF\DynamsoftImageProcessing'"
> - The library only support Android & iOS platform. Be sure that you remove the other platforms like Windows, maccatalyst, etc.

## License

- You can request a 30-day trial license via the [Request a Trial License](https://www.dynamsoft.com/customer/license/trialLicense?product=dcv&utm_source=samples&package=mobile) link.

## Contact Us

For any questions or feedback, you can either [contact us](https://www.dynamsoft.com/company/contact/) or [submit an issue](https://github.com/Dynamsoft/capture-vision-maui-samples/issues/new).
