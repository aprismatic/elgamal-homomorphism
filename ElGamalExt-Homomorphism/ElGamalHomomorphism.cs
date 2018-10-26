using System;
using System.Numerics;

namespace Aprismatic.ElGamalExt.Homomorphism
{
    public static class ElGamalHomomorphism
    {
        public static byte[] Multiply(byte[] first, byte[] second, byte[] P)
        {
            var firstNumerator = new byte[first.Length / 2];
            Array.Copy(first, firstNumerator, first.Length / 2);
            var firstDenominator = new byte[first.Length / 2];
            Array.Copy(first, first.Length / 2, firstDenominator, 0, first.Length / 2);
            var secondNumerator = new byte[second.Length / 2];
            Array.Copy(second, secondNumerator, second.Length / 2);
            var secondDenominator = new byte[second.Length / 2];
            Array.Copy(second, second.Length / 2, secondDenominator, 0, second.Length / 2);

            var mulNumerator = MultiplyParts(firstNumerator, secondNumerator, P);
            var mulDenominator = MultiplyParts(firstDenominator, secondDenominator, P);

            var mul = new byte[first.Length];
            Array.Copy(mulNumerator, 0, mul, 0, mulNumerator.Length);
            Array.Copy(mulDenominator, 0, mul, mul.Length / 2, mulDenominator.Length);

            return mul;
        }

        public static byte[] Divide(byte[] first, byte[] second, byte[] P)
        {
            var firstNumerator = new byte[first.Length / 2];
            Array.Copy(first, firstNumerator, first.Length / 2);
            var firstDenominator = new byte[first.Length / 2];
            Array.Copy(first, first.Length / 2, firstDenominator, 0, first.Length / 2);
            var secondNumerator = new byte[second.Length / 2];
            Array.Copy(second, secondNumerator, second.Length / 2);
            var secondDenominator = new byte[second.Length / 2];
            Array.Copy(second, second.Length / 2, secondDenominator, 0, second.Length / 2);

            var divNumerator = MultiplyParts(firstNumerator, secondDenominator, P);
            var divDenominator = MultiplyParts(firstDenominator, secondNumerator, P);

            var div = new byte[first.Length];
            Array.Copy(divNumerator, 0, div, 0, divNumerator.Length);
            Array.Copy(divDenominator, 0, div, div.Length / 2, divDenominator.Length);

            return div;
        }

        private static byte[] MultiplyParts(byte[] first, byte[] second, byte[] P)
        {
            var blocksize = first.Length;

            var res = new byte[blocksize];

            var temp = new byte[blocksize / 2];
            Array.Copy(first, temp, blocksize / 2);
            var A_left = new BigInteger(temp);
            Array.Copy(first, blocksize / 2, temp, 0, blocksize / 2);
            var A_right = new BigInteger(temp);
            Array.Copy(second, temp, blocksize / 2);
            var B_left = new BigInteger(temp);
            Array.Copy(second, blocksize / 2, temp, 0, blocksize / 2);
            var B_right = new BigInteger(temp);

            var Pbi = new BigInteger(P);

            var res_left = (A_left * B_left) % Pbi;
            var res_right = (A_right * B_right) % Pbi;

            var cAbytes = res_left.ToByteArray();
            var cBbytes = res_right.ToByteArray();

            Array.Copy(cAbytes, 0, res, 0, cAbytes.Length);
            Array.Copy(cBbytes, 0, res, blocksize / 2, cBbytes.Length);

            return res;
        }
    }
}
