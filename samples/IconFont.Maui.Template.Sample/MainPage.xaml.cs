using IconFontTemplate.Sample.ViewModels;

namespace IconFontTemplate.Sample;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new IconsViewModel();
	}
}
