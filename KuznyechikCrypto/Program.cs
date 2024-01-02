using System.Text;

namespace KuznyechikCrypto
{
    public class Program
    {
        static void Main(string[] args)
        {
            byte[] key = new byte[32];
            for (int i = 0; i < 32; i++)
            {
                key[i] = (byte)((i * 3) % 256);
            }

            Kuznyechik kuznyechik = new Kuznyechik(key);
            byte[] plaintext = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, };
            byte[] ciphertext = kuznyechik.EncryptBlock(plaintext);
            StringBuilder ct = new StringBuilder();
            for (int i = 0; i < ciphertext.Length; i++)
            {
                ct.Append(ciphertext[i].ToString("X2"));
            }
            Console.WriteLine(ct.ToString());
        }
    }
}
