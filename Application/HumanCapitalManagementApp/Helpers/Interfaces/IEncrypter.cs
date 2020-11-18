namespace Helpers.Interfaces
{
    public interface IEncrypter
    {
        /// <summary>
        /// Reveals the meaning of the hidden words.
        /// </summary>
        /// <param name="cipherText">Encrypted string</param>
        /// <returns>Meaningfull string</returns>
        string Decrypt(string cipherText);

        /// <summary>
        /// Conceal the meaning of the words.
        /// </summary>
        /// <param name="clearText">Meaningfull string</param>
        /// <returns>Reversably Encrypted String with no meaning</returns>
        string Encrypt(string clearText);
    }
}