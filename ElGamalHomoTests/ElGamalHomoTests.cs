using Aprismatic.Cryptography;
using Xunit;

namespace ElGamalHomoTests
{
    public class ElGamalHomoTests
    {
        [Fact(DisplayName = "Multiplication")]
        public void Mul_Zero()
        {
            byte[] first = { 1, 2, 3, 4 };
            byte[] second = { 5, 6, 7, 8 };
            byte[] P = { 10 };
            byte[] expected = { 5, 2, 1, 2 };

            var res = ElGamalHomomorphism.Multiply(first, second, P);

            Assert.Equal(expected, res);
        }

        [Fact(DisplayName = "Division Zero")]
        public void Div_Zero()
        {
            byte[] first = { 1, 2, 3, 4 };
            byte[] second = { 5, 6, 7, 8 };
            byte[] P = { 10 };
            byte[] expected = { 7, 6, 5, 4 };

            var res = ElGamalHomomorphism.Divide(first, second, P);

            Assert.Equal(expected, res);
        }
    }
}
