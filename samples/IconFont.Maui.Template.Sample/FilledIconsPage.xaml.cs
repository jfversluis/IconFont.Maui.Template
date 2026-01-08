using IconFontTemplate.Sample.ViewModels;

namespace IconFontTemplate.Sample;

public partial class FilledIconsPage : ContentPage
{
	public FilledIconsPage()
	{
		InitializeComponent();
		BindingContext = new IconsViewModel("FluentIconsFilled");
	}
}
