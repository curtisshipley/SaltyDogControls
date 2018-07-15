using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace SaltyDog
{

	public interface ITemplateSelector
	{
		/// <summary>
		/// Returns the name of the template used to display this data item
		/// </summary>
		string TemplateName { get; }
	}


	public class StackTemplateControl : StackLayout
	{

		public static readonly BindableProperty DataTemplateProperty =
			BindableProperty.Create("DataTemplate", typeof(DataTemplate), typeof(StackTemplateControl), null,
				propertyChanged: (b, o, n) => (b as StackTemplateControl).DataTemplateProperty_Changed(b, o, n));

		public DataTemplate DataTemplate
		{
			get => (DataTemplate)GetValue(DataTemplateProperty);
			set => SetValue(DataTemplateProperty, value);
		}


		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(StackTemplateControl), null,
				propertyChanged: (b, o, n) => (b as StackTemplateControl).ItemsSourceProperty_Changed(b, o, n));

		public IEnumerable ItemsSource
		{
			get => (IEnumerable)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}


		public static readonly BindableProperty TemplateDictionaryProperty =
			BindableProperty.Create("TemplateDictionary", typeof(IDictionary<string, object>), typeof(StackTemplateControl), null,
				propertyChanged: (b, o, n) => (b as StackTemplateControl).TemplateDictionaryProperty_Changed(b, o, n));

		public IDictionary<string, object> TemplateDictionary
		{
			get => (IDictionary<string, object>)GetValue(TemplateDictionaryProperty);
			set => SetValue(TemplateDictionaryProperty, value);
		}

		protected void ItemsSourceProperty_Changed(BindableObject bindable, object oldValue, object newValue)
		{
			CreateAndBindContent();
		}

		protected void DataTemplateProperty_Changed(BindableObject bindable, object oldValue, object newValue)
		{
			CreateAndBindContent();
		}

		protected void TemplateDictionaryProperty_Changed(BindableObject bindable, object oldValue, object newValue)
		{
			CreateAndBindContent();
		}

		protected DataTemplate SelectTemplate(object selector)
		{
			if (selector == null)
				return null;

			if (TemplateDictionary == null)
			{
				return DataTemplate;
			}

			string key = null;

			if (selector is String)
				key = selector as string;
			else if (selector is ITemplateSelector)
				key = (selector as ITemplateSelector).TemplateName;
			else
			{
				Console.Error.WriteLine($"StackTemplateControl: item template selector must be a string or a ITemplateSelector. Found {selector.GetType().Name}");
				return null;
			}

			Object entry;

			if (!TemplateDictionary.TryGetValue(key, out entry))
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

		public void CreateAndBindContent()
		{
			if (ItemsSource == null || (TemplateDictionary == null && DataTemplate == null))
				return;

			Children.Clear();

			foreach (var item in ItemsSource)
			{
				DataTemplate template = SelectTemplate(item);
				var o = template.CreateContent();

				if (o != null)
				{
					var view = o as View;
					view.BindingContext = item;
					Children.Add(view);
				}
			}
		}
	}
}
