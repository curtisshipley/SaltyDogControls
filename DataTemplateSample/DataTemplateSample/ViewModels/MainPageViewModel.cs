using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DataTemplateSample.Pages;
using DataTemplateSample.ViewModels;
using Xamarin.Forms;

namespace DataTemplateSample
{
	public interface IButtonInfo
	{
		string Name { get; set; }
		Color Color { get; set; }
		ContentPage Instantiate();
	}
	public class ButtonInfo<P,V> : IButtonInfo where P : ContentPage, new() where V : new()
	{
		public string Name { get; set; }
		public Color Color { get; set; }
		public ContentPage Instantiate()
		{
			var page = new P();
			var model = new V();

			page.BindingContext = model;

			return page;
		}
	}

	public class MainPageViewModel
	{
		public ICommand CmdGoThere { get; set; }
		public INavigation Navigation { get; set; }

		public List<IButtonInfo> Buttons { get; set; } = new List<IButtonInfo>
		{
			BI<TemplateControlPage, TemplateControlViewModel>("Data Template Control", Color.Blue),
			BI<QuestionnairePage, QuestionnaireViewModel>("StackLayout Template", Color.Red)
		};

		public MainPageViewModel()
		{
			CmdGoThere = new Command(ButtonTapped);
		}

		protected async void ButtonTapped(object parameter)
		{
			var buttonInfo = parameter as IButtonInfo;

			var page = buttonInfo.Instantiate();

			await Navigation.PushAsync(page);
		}

		protected static IButtonInfo BI<P,V>(string name, Color color) where P : ContentPage, new() where V : new()
		{
			return new ButtonInfo<P,V> { Name = name, Color = color};
		}
	}
}
