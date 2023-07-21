namespace FluentValidation_Extensions
{
	public static partial class ValidatorExtensions
	{

		#region DateTime
		private static Func<T, bool> After<T>(DateTime dateToCompare)
		{
			return dateGiven => Convert.ToDateTime(dateGiven).Date.CompareTo(dateToCompare.Date) >= 0;
		}

		private static Func<T, bool> Before<T>(DateTime dateToCompare)
		{
			return dateGiven => Convert.ToDateTime(dateGiven).Date.CompareTo(dateToCompare.Date) <= 0;
		}

		private static Func<T, bool> ValidDate<T>()
		{
			DateTime date;

			return dateGiven =>
			{
				if (dateGiven == null)
				{
					return false;
				}
				return DateTime.TryParse(dateGiven.ToString(), out date);
			};
		}

		#endregion DateTime

		#region numeric

		public static Func<T, bool> ValidDecimal<T>()
		{
			decimal output = 0;
			return numberGiven => decimal.TryParse(numberGiven.ToString(), out output);
		}

		public static Func<T, bool> ValidPercentage<T>()
		{
			return percentGiven =>
			{
				decimal percent = 0;
				if (decimal.TryParse(percentGiven.ToString(), out percent))
				{
					return Between(percent, 0, 100);
				}
				return false;
			};
		}

		public static Func<T, bool> Between<T>(int lowerValue, int higherValue)
		{
			return valueGiven =>
			{
				decimal decVal = 0;
				if (decimal.TryParse(valueGiven.ToString(), out decVal))
				{
					return Between(decVal, lowerValue, higherValue);
				}
				return false;
			};
		}

		private static bool Between(int value, int start, int end)
		{
			return value >= start && value <= end;
		}

		private static bool Between(decimal value, decimal start, decimal end)
		{
			return value >= start && value <= end;
		}

		private static Func<T, bool> GreaterThan<T>(int value)
		{
			return valueEntered => Convert.ToDecimal(valueEntered) >= value;
		}

		private static Func<T, bool> ValidInt<T>()
		{

			return numberGiven =>
			{
				string numberAsString = numberGiven.ToString().Trim();
				if (numberAsString.Length > 1)
				{
					numberAsString = numberAsString.TrimStart('0');

				}
				int output = 0;
				return int.TryParse(numberAsString, out output) && output.ToString() == numberAsString;
			};
		}

		private static Func<T, bool> ValidByte<T>()
		{
			return valueGiven =>
			{
				int byteValue = 0;
				if (int.TryParse(valueGiven.ToString(), out byteValue))
				{
					return Between(byteValue, 0, 1);
				}
				return false;
			};
		}

		#endregion numeric

		private static Func<T, bool> ValidMonth<T>()
		{
			return monthGiven =>
			{
				int month = 0;
				if (int.TryParse(monthGiven.ToString(), out month))
				{
					return Between(month, 1, 12);
				}
				return false;
			};
		}

		private static Func<T, bool> ValidYear<T>()
		{
			return yearGiven =>
			{
				int year = 0;
				if (int.TryParse(yearGiven.ToString(), out year))
				{
					return Between(year, 1950, DateTime.Today.Year + 2);
				}
				return false;
			};
		}

		private static Func<string, bool> Contains(string[] allowedOptions, StringComparer comparer)
		{
			return itemGiven =>
			{
				if (string.IsNullOrWhiteSpace(itemGiven))
				{
					return false;
				}
				return allowedOptions.Cast<string>().Contains(itemGiven.ToString().Trim(), comparer);
			};
		}

		private static Func<string, bool> NotContain(string[] existingOptions, StringComparer comparer)
		{
			return itemGiven =>
			{
				if (string.IsNullOrWhiteSpace(itemGiven))
				{
					return true;
				}
				return !existingOptions.Cast<string>().Contains(itemGiven.ToString().Trim(), comparer);
			};
		}

		public static Func<T, bool> Unique<T>(IEnumerable<T> items)
		{
			return source =>
			{

				int count = 0;
				foreach (var item in items)
				{
					if (item == null)
					{
						continue;
					}
					if (source.ToString().Trim().Equals(item.ToString().Trim(), StringComparison.OrdinalIgnoreCase))
					{
						count++;
					}
					if (count > 1)
					{
						return false;
					}
				}

				return true;
			};
		}

		#region decimal places
		//based on https://github.com/JeremySkinner/FluentValidation/blob/master/src/FluentValidation/Validators/ScalePrecisionValidator.cs
		public static Func<T, bool> DecimalPlaces<T>(int dp)
		{
			return number =>
			{
				decimal decimalVal = 0;
				if (decimal.TryParse(number.ToString(), out decimalVal))

				{
					var scale = GetScale(true, decimalVal);
					return scale <= dp;
				}
				return true;
			};
		}

		private static uint GetUnsignedScale(decimal Decimal)
		{
			var bits = GetBits(Decimal);
			uint scale = (bits[3] >> 16) & 31;
			return scale;
		}

		private static int GetScale(bool ignoreTrailingZeros, decimal Decimal)
		{
			uint scale = GetUnsignedScale(Decimal);
			if (ignoreTrailingZeros)
			{
				return (int)(scale - NumTrailingZeros(Decimal));
			}

			return (int)scale;
		}

		private static uint NumTrailingZeros(decimal Decimal)
		{
			uint trailingZeros = 0;
			uint scale = GetUnsignedScale(Decimal);
			for (decimal tmp = GetMantissa(Decimal); tmp % 10m == 0 && trailingZeros < scale; tmp /= 10)
			{
				trailingZeros++;
			}

			return trailingZeros;
		}

		private static UInt32[] GetBits(decimal Decimal)
		{
			// We want the integer parts as uint
			// C# doesn't permit int[] to uint[] conversion, but .NET does. This is somewhat evil...
			return (uint[])(object)decimal.GetBits(Decimal);
		}

		private static decimal GetMantissa(decimal Decimal)
		{
			var bits = GetBits(Decimal);
			return (bits[2] * 4294967296m * 4294967296m) + (bits[1] * 4294967296m) + bits[0];
		}

		#endregion decimal places
	}
}