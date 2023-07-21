using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation_Extensions
{
	public static partial class ValidatorExtensions
	{

		/// <summary>
		/// Check if the trimmed length of <typeparamref name="T"/> is between <paramref name="min"/> and <paramref name="max"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> TrimmedLength<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
		{
			return ruleBuilder.SetValidator(new TrimmedLengthValidator<T>(min, max));
		}

		/// <summary>
		/// heck if the trimmed length of <typeparamref name="T"/> is <paramref name="exactLength"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <param name="exactLength"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> TrimmedLength<T>(this IRuleBuilder<T, string> ruleBuilder, int exactLength)
		{
			return ruleBuilder.SetValidator(new TrimmedExactLengthValidator<T>(exactLength));
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is a valid int by parseing
		/// also checks parsed value is the same to allow for decimal
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> ValidInt<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
		{
			return ruleBuilder
				.Must(ValidInt<TProperty>())
				.WithMessage($"'{{PropertyName}}' must be a valid number");
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is a valid byte by parseing
		/// also checks parsed value is the same to allow for decimal
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> ValidByte<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
		{
			return ruleBuilder
				.Must(ValidByte<TProperty>())
				.WithMessage($"'{{PropertyName}}' must be a valid byte");
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is a valid decimal
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> ValidDecimal<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
		{
			return ruleBuilder
				.Must(ValidDecimal<TProperty>())
				.WithMessage($"'{{PropertyName}}' must be a valid number");
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> has upto the given no of decimal places
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> DecimalPlaces<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, int maximumDecimalPlaces)
		{
			return ruleBuilder
				.Must(DecimalPlaces<TProperty>(maximumDecimalPlaces))
				.WithMessage($"'{{PropertyName}}' must have a maximum of {maximumDecimalPlaces} decimal places.");
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is greater then <paramref name="value"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> GreaterThan<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, int value)
		{
			return ruleBuilder
				.Must(GreaterThan<TProperty>(value))
				.WithMessage($"'{{PropertyName}}' must be greater than {value}");
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is a valid date
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> ValidDate<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
		{
			return ruleBuilder
				.Must(ValidDate<TProperty>())
				.WithMessage($"'{{PropertyName}}' must be a valid date");
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is before <paramref name="value"/> by converting to a datime
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> Before<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, DateTime value)
		{
			return ruleBuilder
				.Must(Before<TProperty>(value))
				.WithMessage($"'{{PropertyName}}' must be earlier than {value}");
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is between <paramref name="lowerValue"/> and <paramref name="higherValue"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <param name="lowerValue"></param>
		/// <param name="higherValue"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> Between<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, int lowerValue, int higherValue)
		{
			return ruleBuilder
				.Must(Between<TProperty>(lowerValue, higherValue))
				.WithMessage($"'{{PropertyName}}' should be between {lowerValue} and {higherValue}");
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is after <paramref name="value"/> by converting to a datime
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> After<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, DateTime value)
		{
			return ruleBuilder
				.Must(After<TProperty>(value))
				.WithMessage($"'{{PropertyName}}' must be later than {value}");
		}

		/// <summary>
		/// String specfic In rule
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <param name="validOptions"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> In<T>(this IRuleBuilder<T, string> ruleBuilder, params string[] validOptions)
		{
			return ruleBuilder.In(true, validOptions);

		}

		/// <summary>
		/// String specfic In rule
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <param name="validOptions"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> In<T>(this IRuleBuilder<T, string> ruleBuilder, bool includeMessage, params string[] validOptions)
		{
			if (includeMessage)
			{
				string formatted;
				if (validOptions == null || validOptions.Length == 0)
				{
					throw new ArgumentException("At least one valid option is expected", nameof(validOptions));
				}
				else if (validOptions.Length == 1)
				{
					formatted = validOptions[0].ToString();
				}
				else
				{
					// format like: option1, option2 or option3
					formatted = $"{string.Join(", ", validOptions.Select(vo => vo.ToString()).ToArray(), 0, validOptions.Length - 1)} or {validOptions.Last()}";
				}

				return ruleBuilder
					.Must(Contains(validOptions, StringComparer.OrdinalIgnoreCase))
					.WithMessage($"'{{PropertyName}}' must be one of these values: {formatted}");
			}
			return ruleBuilder
				.Must(Contains(validOptions, StringComparer.OrdinalIgnoreCase));

		}

		public static IRuleBuilderOptions<T, string> NotIn<T>(this IRuleBuilder<T, string> ruleBuilder, params string[] existingOptions)
		{

			return ruleBuilder
				.Must(NotContain(existingOptions, StringComparer.OrdinalIgnoreCase))
				.WithMessage($"'{{PropertyName}}' already exists in the database");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="Tproperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <param name="validOptions"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, Tproperty> Unique<T, Tproperty>(this IRuleBuilder<T, Tproperty> ruleBuilder, IEnumerable<Tproperty> items)
		{
			return ruleBuilder
				.Must(Unique(items))
			.WithMessage($"{{PropertyName}} must be unique");
		}


		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is a valid decimal between 1 & 100
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> ValidPercentage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
		{
			return ruleBuilder
				.Must(ValidPercentage<TProperty>())
				.WithMessage($"'{{PropertyName}}' should be between 1 and 100");
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is a valid year between 1950 and current year + 2
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> ValidYear<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
		{
			return ruleBuilder
				.Must(ValidYear<TProperty>())
				.WithMessage($"'{{PropertyName}}' must be a valid year between 1950 and " + (DateTime.Today.Year + 2).ToString());
		}

		/// <summary>
		/// Check if <typeparamref name="TProperty"/> is an int between 1 & 12
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="ruleBuilder"></param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, TProperty> ValidMonth<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
		{
			return ruleBuilder
				.Must(ValidMonth<TProperty>())
				.WithMessage($"'{{PropertyName}}' must be a valid month between 1 and 12");
		}

		public static IRuleBuilderOptions<T, string> DayIsValidForMonth<T>(this IRuleBuilder<T, string> ruleBuilder, string monthOfYear)
		{
			return ruleBuilder.SetValidator(new DayOfMonthValidator<T, string>(monthOfYear));
		}
	}
}
