using FluentValidation;
using FluentValidation.Validators;

namespace FluentValidation_Extensions
{
	public class TrimmedLengthValidator<T> : LengthValidator<T>, ILengthValidator, IPropertyValidator
	{

		public TrimmedLengthValidator(int min, int max) : base(min, max)
		{
			if (max != -1 && max < min)
			{
				throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
			}
		}

		public override bool IsValid(ValidationContext<T> context, string value)
		{
			if (value == null)
				return true;

			int length = value.ToString().Trim().Length;

			if (length < Min || (length > Max && Max != -1))
			{
				context.MessageFormatter.AppendArgument("MinLength", this.Min).AppendArgument("MaxLength", this.Max).AppendArgument("TotalLength", length);
				return false;
			}
			return true;
		}
	}
}
