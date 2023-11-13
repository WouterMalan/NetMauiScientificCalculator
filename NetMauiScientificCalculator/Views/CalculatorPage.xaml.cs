using NetMauiScientificCalculator.ViewModels;

namespace NetMauiScientificCalculator.Views;

public partial class CalculatorPage : ContentPage
{
	public CalculatorPage(CalculatorPageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = this.viewModel = viewModel;

	}

	private CalculatorPageViewModel viewModel;
}