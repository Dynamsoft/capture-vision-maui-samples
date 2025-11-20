namespace ScanDocument;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CameraPage());
    }
}

