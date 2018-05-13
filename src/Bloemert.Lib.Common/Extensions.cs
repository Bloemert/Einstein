using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Lib.Common
{
	public static class Extensions
	{

		/// <summary>
		/// Returns the input string with the first character converted to uppercase
		/// </summary>
		public static string FirstLetterToUpperCase(this string s)
		{
			if (string.IsNullOrEmpty(s))
				throw new ArgumentException("There is no first letter");

			char[] a = s.ToCharArray();
			a[0] = char.ToUpper(a[0]);
			return new string(a);
		}




		public static string ToHexString(this byte[] ba)
		{
			if (ba == null || ba.Length == 0)
			{
				return null;
			}

			StringBuilder hex = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
				hex.AppendFormat("{0:x2}", b);
			return hex.ToString();
		}

		public static byte[] HexToByteArray(this string hex)
		{
			if (string.IsNullOrEmpty(hex))
			{
				return null;
			}

			int NumberChars = hex.Length;
			byte[] bytes = new byte[NumberChars / 2];
			for (int i = 0; i < NumberChars; i += 2)
				bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			return bytes;
		}
	}
}
