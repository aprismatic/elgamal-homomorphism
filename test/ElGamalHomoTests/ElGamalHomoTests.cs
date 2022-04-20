using Aprismatic.ElGamal.Homomorphism;
using Xunit;

namespace ElGamalHomoTests
{
    public class ElGamalHomoTests
    {
        [Fact(DisplayName = "INT: Multiplication")]
        public void IntMultiplication()
        {
            byte[] first = { 1, 2 };
            byte[] second = { 5, 6 };
            byte[] P = { 10 };
            byte[] expected = { 5, 2 };

            byte[] res = new byte[2];

            ElGamalHomomorphism.MultiplyIntegers(first, second, P, res);

            Assert.Equal(expected, res);
        }

        [Fact(DisplayName = "FRAC: Multiplication")]
        public void Multiplication()
        {
            byte[] first = { 1, 2, 3, 4 };
            byte[] second = { 5, 6, 7, 8 };
            byte[] P = { 10 };
            byte[] expected = { 5, 2, 1, 2 };

            var res = ElGamalHomomorphism.MultiplyFractions(first, second, P);

            Assert.Equal(expected, res);
        }

        [Fact(DisplayName = "FRAC: Division")]
        public void Division()
        {
            byte[] first = { 1, 2, 3, 4 };
            byte[] second = { 5, 6, 7, 8 };
            byte[] P = { 10 };
            byte[] expected = { 7, 6, 5, 4 };

            var res = ElGamalHomomorphism.DivideFractions(first, second, P);

            Assert.Equal(expected, res);
        }
    }
}
