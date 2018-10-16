using ElGamalExt.Homomorphism;
using Xunit;

namespace ElGamalHomoTests
{
    public class ElGamalHomoTests
    {
        [Fact(DisplayName = "Multiplication Zero")]
        public void Mul_Zero()
        {
            byte[] p_first = { 0x01, 0x01 };
            byte[] p_second = { 0x01, 0x01 };
            byte[] p_P = { 0x01 };
            byte[] expected = { 0x00, 0x00 };

            var res = ElGamalHomomorphism.Multiply(p_first, p_second, p_P);

            Assert.Equal(expected[0], res[0]);
        }

        [Fact(DisplayName = "Division Zero")]
        public void Div_Zero()
        {
            byte[] p_first = { 0x01, 0x01 };
            byte[] p_second = { 0x01, 0x01 };
            byte[] p_P = { 0x01 };
            byte[] expected = { 0x00, 0x00 };

            var res = ElGamalHomomorphism.Divide(p_first, p_second, p_P);

            Assert.Equal(expected[0], res[0]);
        }
    }
}
