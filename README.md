# Dynamsoft Capture Vision samples for MAUI edition

This repository contains multiple samples that demonstrate how to use the [Dynamsoft Capture Vision](https://www.dynamsoft.com/capture-vision/docs/core/introduction/) MAUI Edition.

- [User Guide](https://www.dynamsoft.com/barcode-reader/docs/mobile/programming/maui/user-guide.html)
- [API Reference](https://www.dynamsoft.com/barcode-reader/docs/mobile/programming/maui/api-reference/)

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

In the **NuGet Package Manager>Manage Packages for Solution** of your project, search for **Dynamsoft.BarcodeReaderBundle.Maui**. Select it and click **install**.

### Build and Run

Select your device and run the project.

> Note: If you are running Android on Visual Studio Windows, you have to mannually exclude iOS and Windows platforms. Otherwise, the Visual Studio will report type or namespace not found errors.

![Exclude iOS and Windows from targets](assets/maui-exclude.png)

## License

- You can request a 30-day trial license via the [Request a Trial License](https://www.dynamsoft.com/customer/license/trialLicense?product=cvs&utm_source=samples&package=maui) link.

## Contact Us

For any questions or feedback, you can either [contact us](https://www.dynamsoft.com/company/contact/) or [submit an issue](https://github.com/Dynamsoft/capture-vision-maui-samples/issues/new).
