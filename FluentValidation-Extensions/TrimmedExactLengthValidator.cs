using FluentValidation;
using FluentValidation.Validators;

namespace FluentValidation_Extensions
{
	public class TrimmedExactLengthValidator<T> : ExactLengthValidator<T>, IPropertyValidator
	{
		public TrimmedExactLengthValidator(int exactLength) : base(exactLength)
		{
			if (exactLength <= 0)
			{
				throw new ArgumentOutOfRangeException("exactLength", "Length should be larger than 0");
			}
		}

		public override bool IsValid(ValidationContext<T> context, string value)
		{
			if (value == null)
			{
				return true;
			}

			int length = value.Trim().Length;

			if (length < Min || (length > Max && Max != -1))
			{
				context.MessageFormatter.AppendArgument("MinLength", this.Min).AppendArgument("MaxLength", this.Max).AppendArgument("TotalLength", length);
				return false;
			}
			return true;
		}
	}
}
