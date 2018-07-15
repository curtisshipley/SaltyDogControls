using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace SaltyDog
{
	public class DataTemplateControl : ContentView
	{
		public DataTemplateControl()
		{
		}


		public static readonly BindableProperty TemplateDictionaryProperty =
			BindableProperty.Create("TemplateDictionary", typeof(IDictionary<string, object>), typeof(DataTemplateControl), null,
				propertyChanged: (b, o, n) => (b as DataTemplateControl).ItemTemplateProperty_Changed(b, o, n));

		/// <summary>
		/// The dictionary object from which the DataTemplateControl gets data template items. Can be set to a ResourceDictionary.
		/// </summary>
		public IDictionary<string,object> TemplateDictionary
		{
			get => (ResourceDictionary)GetValue(TemplateDictionaryProperty);
			set => SetValue(TemplateDictionaryProperty, value);
		}


		public static readonly BindableProperty SelectorProperty =
			BindableProperty.Create("Selector", typeof(object), typeof(DataTemplateControl), null,
				propertyChanged: (b, o, n) => (b as DataTemplateControl).SelectorProperty_Changed(b, o, n));

		/// <summary>
		/// The value indicating which template key to use for the template. If not a string, this value is converted using ToString(). 
		/// </summary>
		public Object Selector
		{
			get => (Object)GetValue(SelectorProperty);
			set => SetValue(SelectorProperty, value);
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			CreateAndBindContent();
		}

		protected void SelectorProperty_Changed(BindableObject bindable, object oldValue, object newValue)
		{
			CreateAndBindContent();
		}

		protected void ItemTemplateProperty_Changed(BindableObject bindable, object oldValue, object newValue)
		{
			CreateAndBindContent();
		}

		/// <summary>
		/// Given the value selector, return the DataTemplate from the Dictionary. If selector or 
		/// TemplateDictionary property is null, return null. If the value returned from the dictionary 
		/// is not a DataTemplate, return null.
		/// </summary>
		/// <param name="selector"></param>
		/// <returns></returns>
		protected DataTemplate SelectTemplate(String selector)
		{
			if (selector == null)
				return null;

			if (TemplateDictionary == null)
			{
				Console.WriteLine("Templates not set");
				return null;
			}

			Object entry;

			if (!TemplateDictionary.TryGetValue(selector, out entry))
			{
				Console.Error.WriteLine($"Dictionary key {selector.ToString()} not found");
				return null;
			}

			if (!(entry is DataTemplate))
			{
				Console.Error.WriteLine($"Dictionary item {selector.ToString()} is not a DataTemplate");
				return null;

			}

			return entry as DataTemplate;
		}


		/// <summary>
		/// Find the DataTemplate from the dictionary. Set Content 
		/// </summary>
		public void CreateAndBindContent()
		{
			if (Selector == null || TemplateDictionary == null)
				return;

			DataTemplate template = SelectTemplate(Selector.ToString());
			var o = template.CreateContent();

			if (o != null)
			{
				var view = o as View;
				view.BindingContext = BindingContext;
				Content = view;
			}
		}
		
	}
}
