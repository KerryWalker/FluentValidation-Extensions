using FluentValidation;
using FluentValidation.Validators;

namespace FluentValidation_Extensions
{
	public class DayOfMonthValidator<T, TProperty> : PropertyValidator<T, TProperty>
	{
		//TODO
		//Test this still works after updating FluentValidation https://docs.fluentvalidation.net/en/latest/upgrading-to-10.html
		private string _monthOfYear;

		private const string _notNumeric = "is not a valid number";
		private const string _notInMonth = "is not a valid date during the month given.";

		public DayOfMonthValidator(string monthOfYear)
		{
			_monthOfYear = monthOfYear;
		}

		public override string Name => "DayOfMonthValidator";

		protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} {Message}";

		public override bool IsValid(ValidationContext<T> context, TProperty value)
		{
			//validate its a number
			string dayPassedIn = value as string;
			int day;
			if (!int.TryParse(dayPassedIn, out day))
			{
				context.MessageFormatter.AppendArgument("Message", _notNumeric);
				return false;
			}
			int month;
			if (!int.TryParse(_monthOfYear, out month))
			{
				//the month isnt valid so return, that will be caught else where
				return true;
			}
			int days = DateTime.DaysInMonth(1950, month);
			if (day > days)
			{
				context.MessageFormatter.AppendArgument("Message", _notInMonth);
				return false;
			}

			return true;
		}
	}
}
