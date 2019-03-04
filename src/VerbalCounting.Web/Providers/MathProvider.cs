using System;

namespace VerbalCounting.Web.Providers
{
	public class Example
	{
		public string Left { get; set; }
		public string Operator { get; set; }
		public string Right { get; set; }
		public string Template { get; set; }
		public string Result { get; set; }
	}

	public class MathProvider
	{
		Random rnd = new Random();

		private int Generate(int from, int to)
		{
			from = from > 0 ? (int)Math.Pow(10, from) : 1;
			to = (int)Math.Pow(10, to);

			return rnd.Next(from, to);
		}

		private int GenerateRangeByTemplate(string t)
		{
			var range = t.Split('-', ' ', StringSplitOptions.RemoveEmptyEntries);

			string from = range[0];
			string to = range.Length == 1 ? range[0] : range[1];

			return Generate(int.Parse(from) - 1, int.Parse(to));
		}

		private string GenerateOperator(string t)
		{
			if (t == "?")
			{
				return GenerateString(1, 1, "+-*/");
			}

			return t;
		}

		public Example GetExample(string template)
		{
			string[] t = template.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			return new Example
			{
				Template = template,
				Left = GenerateRangeByTemplate(t[0]).ToString(),
				Operator = GenerateOperator(t[1]),
				Right = GenerateRangeByTemplate(t[2]).ToString(),
			};

		}

		private string GenerateString(int minLength, int maxLength, string allowedChars)
		{
			maxLength = maxLength != int.MaxValue ? maxLength : rnd.Next(128, 512);
			char[] chars = new char[maxLength];
			int setLength = allowedChars.Length;

			int length = rnd.Next(minLength, maxLength + 1);

			for (int i = 0; i < length; ++i)
			{
				chars[i] = allowedChars[rnd.Next(setLength)];
			}

			return new string(chars, 0, length);
		}
	}
}
