using IconFontTemplate.Sample.ViewModels;

namespace IconFontTemplate.Sample;

public partial class RegularIconsPage : ContentPage
{
	public RegularIconsPage()
	{
		InitializeComponent();
		BindingContext = new IconsViewModel("FluentIcons");
	}
}
