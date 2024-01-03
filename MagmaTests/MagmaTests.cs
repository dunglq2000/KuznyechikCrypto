//using Magma;

namespace MagmaTests
{
    [TestClass]
    public class MagmaTests
    {
        public uint HexToUInt(string hex)
        {
            return Convert.ToUInt32(hex, 16);
        }
        [TestMethod]
        public void TestMagmaT()
        {
            string[] input =
                [
                    "fdb97531",
                    "2a196f34",
                    "ebd9f03a",
                    "b039bb3d"
                ];
            string[] output =
                [
                    "2a196f34",
                    "ebd9f03a",
                    "b039bb3d",
                    "68695433"
                ];
            for (int i = 0; i < input.Length;i++)
            {
                Assert.AreEqual(HexToUInt(output[i]), Magma.Magma.t(HexToUInt(input[i])));
            }
        }
        [TestMethod]
        public void TestMagmaG()
        {
            string[][] input =
                [
                    ["87654321", "fedcba98"],
                    ["fdcbc20c", "87654321"],
                    ["7e791a4b", "fdcbc20c"],
                    ["c76549ec", "7e791a4b"]
                ];
            string[] output =
                [
                    "fdcbc20c",
                    "7e791a4b",
                    "c76549ec",
                    "9791c849"
                ];
            for (int i = 0; i < input.Length; i++)
            {
                uint key = Convert.ToUInt32(input[i][0], 16);
                uint right = Convert.ToUInt32(input[i][1], 16);
                uint result = Magma.Magma.rot11(Magma.Magma.t(Magma.Magma.plus32(key, right)));
                Assert.AreEqual(HexToUInt(output[i]), result);
            }
        }
        [TestMethod]
        public void TestMagmaSubkeys()
        {
            byte[] key = Convert.FromHexString("ffeeddccbbaa99887766554433221100f0f1f2f3f4f5f6f7f8f9fafbfcfdfeff");
            Magma.Magma magma = new Magma.Magma(key);
            string[] output =
                [
                    "ffeeddcc",
                    "bbaa9988",
                    "77665544",
                    "33221100",
                    "f0f1f2f3",
                    "f4f5f6f7",
                    "f8f9fafb",
                    "fcfdfeff",
                ];
            for (int i = 0; i <  output.Length; i++)
            {
                Assert.AreEqual(HexToUInt(output[i]), magma.subkeys[i]);
            }
        }
        [TestMethod]
        public void TestMagmaRound()
        {
            byte[] key = Convert.FromHexString("ffeeddccbbaa99887766554433221100f0f1f2f3f4f5f6f7f8f9fafbfcfdfeff");
            Magma.Magma magma = new Magma.Magma(key);
            ulong plaintext = Convert.ToUInt64("fedcba9876543210", 16);
            string[] output =
                [
                    //"fedcba9876543210",
                    "7654321028da3b14",
                    "28da3b14b14337a5",
                    "b14337a5633a7c68",
                    "633a7c68ea89c02c",
                    "ea89c02c11fe726d",
                    "11fe726dad0310a4",
                    "ad0310a437d97f25",
                    "37d97f2546324615",
                    "46324615ce995f2a",
                    "ce995f2a93c1f449",
                    "93c1f4494811c7ad",
                    "4811c7adc4b3edca",
                    "c4b3edca44ca5ce1",
                    "44ca5ce1fef51b68",
                    "fef51b682098cd86",
                    "2098cd864f15b0bb",
                    "4f15b0bbe32805bc",
                    "e32805bce7116722",
                    "e711672289cadf21",
                    "89cadf21bac8444d",
                    "bac8444d11263a21",
                    "11263a21625434c3",
                    "625434c38025c0a5",
                    "8025c0a5b0d66514",
                    "b0d6651447b1d5f4",
                    "47b1d5f4c78e6d50",
                    "c78e6d5080251e99",
                    "80251e992b96eca6",
                    "2b96eca605ef4401",
                    "05ef4401239a4577",
                    "239a4577c2d8ca3d",
                    "4ee901e5c2d8ca3d"
                ];
            for (int i = 0; i < 24; i++)
            {
                Assert.AreEqual(Convert.ToUInt64(output[i], 16), Magma.Magma.round(plaintext, magma.subkeys[i % 8]));
                plaintext = Convert.ToUInt64(output[i], 16);
            }
            for (int i = 24; i < 31; i++)
            {
                Assert.AreEqual(Convert.ToUInt64(output[i], 16), Magma.Magma.round(plaintext, magma.subkeys[7 - (i % 8)]));
                plaintext = Convert.ToUInt64(output[i], 16);
            }
            ulong tmp = Magma.Magma.round(plaintext, magma.subkeys[0]);
            Assert.AreEqual(Convert.ToUInt64(output[31], 16), (tmp << 32) | (tmp >> 32));
        }
    }
}