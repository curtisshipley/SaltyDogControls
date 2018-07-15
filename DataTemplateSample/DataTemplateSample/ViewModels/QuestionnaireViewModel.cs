using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SaltyDog;
using Xamarin.Forms;

namespace DataTemplateSample.ViewModels
{
	public enum QuestionType
	{
		StringQuestion, IntQuestion, BooleanQuestion
	}

	public class Question : ITemplateSelector
	{
		public string DisplayName { get; set; }
		public QuestionType QuestionType { get; set; }
		public object Value { get; set; }

		public string TemplateName => QuestionType.ToString();
	}

	public class QuestionnaireViewModel
	{
		public List<Question> Questions { get; set; } = new List<Question>
		{
			SQ("First Name"), IQ("Age"), BQ("Likes Dogs")
		};

		public ICommand CmdSubmit { get; set; }

		public QuestionnaireViewModel()
		{
			CmdSubmit = new Command(SubmitCommand);
		}

		public void SubmitCommand()
		{

			foreach ( var question in Questions)
			{
				Console.WriteLine($"{question.DisplayName}: {question.Value}");
			}
		}

		public static Question IQ(string DisplayName)
		{
			return new Question
			{
				DisplayName = DisplayName,
				QuestionType = QuestionType.IntQuestion
			};
		}

		public static Question SQ(string DisplayName)
		{
			return new Question
			{
				DisplayName = DisplayName,
				QuestionType = QuestionType.StringQuestion
			};
		}


		public static Question BQ(string DisplayName)
		{
			return new Question
			{
				DisplayName = DisplayName,
				QuestionType = QuestionType.BooleanQuestion,
				Value = false
			};
		}


	}
}
