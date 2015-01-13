//==================================================
//Descripción: Clase para el cifrado de archivos y cadenas de caracteres.
//Autor: Gonzalo Antonio Sosa M.
//Fecha: Septiembre 2003
//==================================================
using System;
using System.Security.Cryptography;

namespace CifradoCs
{
	/// <summary>
	/// Descripción breve de CryptoServiceProvider.
	/// </summary>
	internal class CryptoServiceProvider
	{
		// proveedor de cifrado.
		private CryptoProvider algorithm;
		// acción a realizar
		private CryptoAction cAction;

		// Lista con las posibles acciones a realizar dentro de la clase
		internal enum CryptoAction
		{
			Encrypt,
			Desencrypt
		}

		// Lista con los proveedores de cifrado que proporciona la clase.
		internal enum CryptoProvider
		{
			DES,
			TripleDES,
			RC2,
			Rijndael
		}

		/// <summary>
		/// Contructor por defecto.
		/// </summary>
		/// <param name="alg">Proveedor (algoritmo) seleccionado.</param>
		/// <param name="action">Acción a realizar.</param>
		internal CryptoServiceProvider(CryptoProvider alg, CryptoAction action)
		{
			// asignamos las opciones seleccionadas.
			// proveedor (algoritmo) de encripción y acción a realizar.
			this.algorithm = alg;
			this.cAction = action;
		}

		/// <summary>
		/// Define un objeto para la operaciones básicas de transformaciones
		/// criptográficas.
		/// </summary>
		/// <param name="Key">Clave de encripción.</param>
		/// <param name="IV">Vector de inicialización.</param>
		/// <returns>Devuelve el objeto que implementa la interfaz ICryptoTransform.
		/// </returns>
		internal ICryptoTransform GetServiceProvider(byte[] Key, byte[] IV)
		{
			// creamos la variable que contendrá al objeto ICryptoTransform.
			ICryptoTransform transform = null;

			// dependiendo del algoritmo seleccionado, se devuelve el objeto adecuado.
			switch (this.algorithm)
			{
				// Algoritmo DES.
				case CryptoProvider.DES:
					// dependiendo de la acción a realizar.
					// creamos el objeto adecuado.
					DESCryptoServiceProvider des = new DESCryptoServiceProvider();
					switch (cAction)
					{
						case CryptoAction.Encrypt:// si estamos cifrando,
							// creamos el objeto cifrador. 
							transform = des.CreateEncryptor(Key, IV);
							break;
						case CryptoAction.Desencrypt:// sí estamos descifrando,
							// creamos el objeto descifrador.
							transform = des.CreateDecryptor(Key, IV);
							break;
					}
					// devolvemos el objeto transform correspondiente.
					return transform;
				// algoritmo TripleDES.
				case CryptoProvider.TripleDES:
					TripleDESCryptoServiceProvider des3 = new TripleDESCryptoServiceProvider();
					switch (cAction)
					{
						case CryptoAction.Encrypt:
							transform = des3.CreateEncryptor(Key, IV);
							break;
						case CryptoAction.Desencrypt:
							transform = des3.CreateDecryptor(Key, IV);
							break;
					}
					return transform;
				// algoritmo RC2.
				case CryptoProvider.RC2:
					RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
					switch (cAction)
					{
						case CryptoAction.Encrypt:
							transform = rc2.CreateEncryptor(Key, IV);
							break;
						case CryptoAction.Desencrypt:
							transform = rc2.CreateDecryptor(Key, IV);
							break;
					}
					return transform;
				// algoritmo Rijndael.
				case CryptoProvider.Rijndael:
					Rijndael rijndael = new RijndaelManaged();
					switch (cAction)
					{
						case CryptoAction.Encrypt:
							transform = rijndael.CreateEncryptor(Key, IV);
							break;
						case CryptoAction.Desencrypt:
							transform = rijndael.CreateDecryptor(Key, IV);
							break;
					}
					return transform;
				default:
					// en caso que no exista el proveedor seleccionado, generamos
					// una excepción para informarlo.
					throw new CryptographicException("Error al inicializar al proveedor de cifrado");
			}
		}
	}
}
