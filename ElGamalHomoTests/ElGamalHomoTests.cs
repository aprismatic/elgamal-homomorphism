using System;
using ElGamalExt.Homomorphism;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElGamalHomoTests
{
    [TestClass]
    public class ElGamalHomoTests
    {
        [TestMethod]
        public void TestZero()
        {
            byte[] p_first = { 0x01 };
            byte[] p_second = { 0x01 };
            byte[] p_P = { 0x01 };
            byte[] expected = { 0x00 };

            var res = ElGamalHomomorphism.Multiply(p_first, p_second, p_P);

            Assert.AreEqual(expected[0], res[0]);
        }
    }
}
