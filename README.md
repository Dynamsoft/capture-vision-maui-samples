# Dynamsoft Capture Vision samples for MAUI edition

This repository contains multiple samples that demonstrate how to use the [Dynamsoft Capture Vision](https://www.dynamsoft.com/capture-vision/docs/core/introduction/) MAUI Edition.

## System Requirements

### .Net

- .NET 7.0 and above.

### Android

- Supported OS: **Android 5.0** (API Level 21) or higher.
- Supported ABI: **armeabi-v7a**, **arm64-v8a**, **x86** and **x86_64**.
- Development Environment: Visual Studio 2022.
- JDK: 1.8+

### iOS

- Supported OS: **iOS 11.0** or higher.
- Supported ABI: **arm64** and **x86_64**.
- Development Environment: Visual Studio 2022 for Mac and Xcode 14.3+.

## Samples

| Sample Name | Description |
| ----------- | ----------- |
| `BarcodeReaderSimpleSample` | This is a sample that illustrates the simplest way to recognize barcodes from video streaming with Dynamsoft Capture Vision MAUI SDK. |

## Install the Dependencies

### Online installation

In the **NuGet Package Manager>Manage Packages for Solution** of your project, search for **Dynamsoft.BarcodeReaderBundle.Maui**. Select it and click **install**.

### Offline installation

Navigate to **Tools -> Options -> NuGet Package Manager -> Package Sources**, click the **+** to add a new package source, then fill in the **Name** (e.g., local-nuget) and the absolute path to the NuGet package location. Click **OK** to confirm.

In the **NuGet Package Manager > Manage Packages for Solution** section of your project, switch the package source to `local-nuget`, search for **Dynamsoft.BarcodeReader.Maui**, **Dynamsoft.CaptureVisionRouter.Maui**, and **Dynamsoft.Core.Maui**, and click **install**.

### Build and Run

Select your device and run the project.

> Note: If you are runing Android on Visual Studio Windows, you have to mannually exclude iOS and Windows platforms. Otherwise, the Visual Studio will report type or namespace not found errors.

![Exclude iOS and Windows from targets](assets/maui-exclude.png)

## License

- A one-day trial license is available by default for every new device to try Dynamsoft SDKs.
- You can request a 30-day trial license via the [Request a Trial License](https://www.dynamsoft.com/customer/license/trialLicense?product=dbr&utm_source=github&package=maui) link. Offline trial license is also available by [contacting us](https://www.dynamsoft.com/company/contact/).

## Contact

https://www.dynamsoft.com/contact/
