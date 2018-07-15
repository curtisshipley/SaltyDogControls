using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataTemplateSample
{
	public enum WidgetType { Normal, Enhanced }

	public class TemplateControlViewModel 
	{
		public string WidgetId { get; set; } = "ABC465883";
		public WidgetType WidgetType { get; set; }
		public string WidgetName { get; set; } = "Thangamabob 9000!";
		public string WidgetImage { get; set; } = "thingamabob.png";
		public string WidgetPrice { get; set; } = "$99.99";
		public string WidgetDescription { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
	}
}
