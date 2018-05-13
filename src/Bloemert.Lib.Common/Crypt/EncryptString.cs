using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Bloemert.Lib.Common.Crypt
{
	public class Encryption
	{

		private  ILogger Log { get; }

		private  string Key { get; }
		private  string Vector { get; }


		public Encryption(string strKey, string strVector, ILogger log)
		{
			Key = strKey;
			Vector = strVector;
			Log = log;
		}


		public string EncryptString(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "";
			}

			try
			{
				byte[] key = Encoding.UTF8.GetBytes(Key);
				byte[] IV = Encoding.ASCII.GetBytes(Vector);

				using (var aesAlg = Aes.Create())
				{
					using (var encryptor = aesAlg.CreateEncryptor(key, IV))
					{
						using (var msEncrypt = new MemoryStream())
						{
							using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
							using (var swEncrypt = new StreamWriter(csEncrypt))
							{
								swEncrypt.Write(text);
							}

							var decryptedContent = msEncrypt.ToArray();

							var result = new byte[IV.Length + decryptedContent.Length];

							Buffer.BlockCopy(IV, 0, result, 0, IV.Length);
							Buffer.BlockCopy(decryptedContent, 0, result, IV.Length, decryptedContent.Length);

							return Convert.ToBase64String(result);
						}
					}
				}
			}
			catch (Exception e)
			{
				Log.Error("Unable to encrypt string: " + text, e);

				throw e;
			}
		}

		public string DecryptString(string cipherText)
		{
			if (string.IsNullOrEmpty(cipherText))
			{
				return "";
			}
			try
			{
				var fullCipher = Convert.FromBase64String(cipherText);

				byte[] IV = Encoding.ASCII.GetBytes(Vector);
				var cipher = new byte[fullCipher.Length - IV.Length];

				Buffer.BlockCopy(fullCipher, 0, IV, 0, IV.Length);
				Buffer.BlockCopy(fullCipher, IV.Length, cipher, 0, fullCipher.Length - IV.Length);
				var key = Encoding.UTF8.GetBytes(Key);

				using (var aesAlg = Aes.Create())
				{
					using (var decryptor = aesAlg.CreateDecryptor(key, IV))
					{
						string result;
						using (var msDecrypt = new MemoryStream(cipher))
						{
							using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
							{
								using (var srDecrypt = new StreamReader(csDecrypt))
								{
									result = srDecrypt.ReadToEnd();
								}
							}
						}

						return result;
					}
				}
			}
			catch (Exception e)
			{
				Log.Error("Unable to decrypt string: " + cipherText, e);
				throw e;
			}
		}

	}
}
