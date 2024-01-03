using System.Text;

namespace Magma
{
    public class Program
    {
        public static void Main(string[] args)
        {
            byte[] key = Convert.FromHexString("ffeeddccbbaa99887766554433221100f0f1f2f3f4f5f6f7f8f9fafbfcfdfeff");
            byte[] plaintext = Convert.FromHexString("fedcba9876543210");

            Magma magma = new Magma(key);

            byte[] ciphertext = magma.EncryptBlock(plaintext);
            byte[] plaintext2 = magma.DecryptBlock(ciphertext);
            StringBuilder ct = new StringBuilder();
            StringBuilder pt = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                ct.Append(ciphertext[i].ToString("x2"));
                pt.Append(plaintext2[i].ToString("x2"));
            }
            Console.WriteLine(ct.ToString());
            Console.WriteLine(pt.ToString());
        }
    }
}
