using System;
using System.Text;
using KuznyechikCrypto;

namespace KuznyechikTests
{
    [TestClass]
    public class KuznyechikTests
    {
        public static string ToHex(byte[] data)
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                output.Append(data[i].ToString("x2"));
            }
            return output.ToString();
        }
        [TestMethod]
        public void TestKuznyechikS()
        {
            string[] input =
            [
                "ffeeddccbbaa99881122334455667700",
                "b66cd8887d38e8d77765aeea0c9a7efc",
                "559d8dd7bd06cbfe7e7b262523280d39",
                "0c3322fed531e4630d80ef5c5a81c50b",
            ];

            string[] output =
            [
                "b66cd8887d38e8d77765aeea0c9a7efc",
                "559d8dd7bd06cbfe7e7b262523280d39",
                "0c3322fed531e4630d80ef5c5a81c50b",
                "23ae65633f842d29c5df529c13f5acda"
            ];

            for (int i = 0; i < 4; i++)
            {
                byte[] inp = Convert.FromHexString(input[i]);
                byte[] sb = Kuznyechik.S(inp);
                Assert.AreEqual(ToHex(sb), output[i]);
            }
        }
        [TestMethod]
        public void KuznyechikTestR()
        {
            string[] input =
            [
                "00000000000000000000000000000100",
                "94000000000000000000000000000001",
                //"à5940000000000000000000000000000",
                "64a59400000000000000000000000000"
            ];
            string[] output =
            [
                "94000000000000000000000000000001",
                "a5940000000000000000000000000000",
                //"64à59400000000000000000000000000",
                "0d64a594000000000000000000000000"
            ];
            for (int i = 0; i < 3; i++)
            {
                byte[] inp = Convert.FromHexString(input[i]);
                byte[] sb = Kuznyechik.R(inp);
                Assert.AreEqual(ToHex(sb), output[i]);
            }
        }
        [TestMethod]
        public void KuznyechikTestL()
        {
            string[] input =
            [
                "64a59400000000000000000000000000",
                "d456584dd0e3e84cc3166e4b7fa2890d",
                "79d26221b87b584cd42fbc4ffea5de9a",
                "0e93691a0cfc60408b7b68f66b513c13"
            ];

            string[] output =
            [
                "d456584dd0e3e84cc3166e4b7fa2890d",
                "79d26221b87b584cd42fbc4ffea5de9a",
                "0e93691a0cfc60408b7b68f66b513c13",
                "e6a8094fee0aa204fd97bcb0b44b8580"
            ];
            for (int i = 0; i < 4; i++)
            {
                byte[] inp = Convert.FromHexString(input[i]);
                byte[] sb = Kuznyechik.L(inp);
                Assert.AreEqual(ToHex(sb), output[i]);
            }
        }
        [TestMethod]
        public void KuznyechikTestExpandKey()
        {
            byte[] key = Convert.FromHexString("8899aabbccddeeff0011223344556677fedcba98765432100123456789abcdef");
            Kuznyechik kuznyechik = new Kuznyechik(key);
            string[] subkeys =
            [
                "8899aabbccddeeff0011223344556677",
                "fedcba98765432100123456789abcdef",
                "db31485315694343228d6aef8cc78c44",
                "3d4553d8e9cfec6815ebadc40a9ffd04",
                "57646468c44a5e28d3e59246f429f1ac",
                "bd079435165c6432b532e82834da581b",
                "51e640757e8745de705727265a0098b1",
                "5a7925017b9fdd3ed72a91a22286f984",
                "bb44e25378c73123a5f32f73cdb6e517",
                "72e9dd7416bcf45b755dbaa88e4a4043"
            ];
            for (int i = 0; i < kuznyechik.subkeys.Length; i++)
            {
                Assert.AreEqual(ToHex(kuznyechik.subkeys[i]), subkeys[i]);
            }
        }
        [TestMethod]
        public void KuznyechikTestC()
        {
            string[] constants =
            [
                "6ea276726c487ab85d27bd10dd849401",
                "dc87ece4d890f4b3ba4eb92079cbeb02",
                "b2259a96b4d88e0be7690430a44f7f03",
                "7bcd1b0b73e32ba5b79cb140f2551504",
                "156f6d791fab511deabb0c502fd18105",
                "a74af7efab73df160dd208608b9efe06",
                "c9e8819dc73ba5ae50f5b570561a6a07",
                "f6593616e6055689adfba18027aa2a08",
            ];
            Kuznyechik.Constants();
            for (int i = 0; i < constants.Length; i++)
            {
                Assert.AreEqual(ToHex(Kuznyechik.c[i]), constants[i]);
            }
        }
        [TestMethod]
        public void KuznyechikTestEncryptBlock()
        {
            byte[] key = Convert.FromHexString("8899aabbccddeeff0011223344556677fedcba98765432100123456789abcdef");
            Kuznyechik kuznyechik = new Kuznyechik(key);
            byte[] plaintext = Convert.FromHexString("1122334455667700ffeeddccbbaa9988");
            byte[] ciphertext = kuznyechik.EncryptBlock(plaintext);
            Assert.AreEqual(ToHex(ciphertext), "7f679d90bebc24305a468d42b9d4edcd");
        }
        [TestMethod]
        public void TestKuznyechikS_inv()
        {
            string[] output =
            [
                "ffeeddccbbaa99881122334455667700",
                "b66cd8887d38e8d77765aeea0c9a7efc",
                "559d8dd7bd06cbfe7e7b262523280d39",
                "0c3322fed531e4630d80ef5c5a81c50b",
            ];

            string[] input =
            [
                "b66cd8887d38e8d77765aeea0c9a7efc",
                "559d8dd7bd06cbfe7e7b262523280d39",
                "0c3322fed531e4630d80ef5c5a81c50b",
                "23ae65633f842d29c5df529c13f5acda"
            ];

            for (int i = 0; i < 4; i++)
            {
                byte[] inp = Convert.FromHexString(input[i]);
                byte[] sb = Kuznyechik.S_inv(inp);
                Assert.AreEqual(ToHex(sb), output[i]);
            }
        }
        [TestMethod]
        public void KuznyechikTestR_inv()
        {
            string[] output =
            [
                "00000000000000000000000000000100",
                "94000000000000000000000000000001",
                //"à5940000000000000000000000000000",
                "64a59400000000000000000000000000"
            ];
            string[] input =
            [
                "94000000000000000000000000000001",
                "a5940000000000000000000000000000",
                //"64à59400000000000000000000000000",
                "0d64a594000000000000000000000000"
            ];
            for (int i = 0; i < 3; i++)
            {
                byte[] inp = Convert.FromHexString(input[i]);
                byte[] sb = Kuznyechik.R_inv(inp);
                Assert.AreEqual(ToHex(sb), output[i]);
            }
        }
        [TestMethod]
        public void KuznyechikTestL_inv()
        {
            string[] output =
            [
                "64a59400000000000000000000000000",
                "d456584dd0e3e84cc3166e4b7fa2890d",
                "79d26221b87b584cd42fbc4ffea5de9a",
                "0e93691a0cfc60408b7b68f66b513c13"
            ];

            string[] input =
            [
                "d456584dd0e3e84cc3166e4b7fa2890d",
                "79d26221b87b584cd42fbc4ffea5de9a",
                "0e93691a0cfc60408b7b68f66b513c13",
                "e6a8094fee0aa204fd97bcb0b44b8580"
            ];
            for (int i = 0; i < 4; i++)
            {
                byte[] inp = Convert.FromHexString(input[i]);
                byte[] sb = Kuznyechik.L_inv(inp);
                Assert.AreEqual(ToHex(sb), output[i]);
            }
        }
        [TestMethod]
        public void KuznyechikTestDecryptBlock()
        {
            byte[] key = Convert.FromHexString("8899aabbccddeeff0011223344556677fedcba98765432100123456789abcdef");
            Kuznyechik kuznyechik = new Kuznyechik(key);
            byte[] ciphertext = Convert.FromHexString("7f679d90bebc24305a468d42b9d4edcd");
            byte[] plaintext = kuznyechik.DecryptBlock(ciphertext);
            Assert.AreEqual(ToHex(plaintext), "1122334455667700ffeeddccbbaa9988");
        }
    }
}