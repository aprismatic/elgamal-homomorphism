using System;
using System.Numerics;

namespace Aprismatic.ElGamalExt.Homomorphism
{
    public static class ElGamalHomomorphism
    {
        /// <summary>
        /// Performs a homomorphic multiplication of two encrypted BigFraction values.
        /// </summary>
        /// <param name="first">First BigFraction to multiply</param>
        /// <param name="second">Second BigFraction to multiply</param>
        /// <param name="P">ElGamal modulo</param>
        /// <returns>Byte array that contains an ElGamal encryption of the product of the two BigFractions</returns>
        public static byte[] MultiplyFractions(byte[] first, byte[] second, byte[] P)
        {
            var hb = first.Length >> 1;

            var fas = first.AsSpan();
            var sas = second.AsSpan();

            var firstNumerator = fas.Slice(0, hb);
            var firstDenominator = fas.Slice(hb, hb);

            var secondNumerator = sas.Slice(0, hb);
            var secondDenominator = sas.Slice(hb, hb);

            var res = new byte[first.Length];
            var ras = res.AsSpan();
            MultiplyIntegers(firstNumerator, secondNumerator, P, ras.Slice(0, hb));
            MultiplyIntegers(firstDenominator, secondDenominator, P, ras.Slice(hb, hb));

            return res;
        }

        /// <summary>
        /// Performs a homomorphic division of two encrypted BigFraction values. Works through multiplying the first BigFraction by a flipped second BigFraction.
        /// </summary>
        /// <param name="first">BigFraction to divide</param>
        /// <param name="second">BigFraction to divide by</param>
        /// <param name="P">ElGamal modulo</param>
        /// <returns>Byte array that contains an ElGamal encryption of the quotient of the two BigFractions</returns>
        public static byte[] DivideFractions(byte[] first, byte[] second, byte[] P)
        {
            var hb = first.Length >> 1;

            var fas = first.AsSpan();
            var sas = second.AsSpan();

            var firstNumerator = fas.Slice(0, hb);
            var firstDenominator = fas.Slice(hb, hb);

            var secondNumerator = sas.Slice(0, hb);
            var secondDenominator = sas.Slice(hb, hb);

            var res = new byte[first.Length];
            var ras = res.AsSpan();
            MultiplyIntegers(firstNumerator, secondDenominator, P, ras.Slice(0, hb));
            MultiplyIntegers(firstDenominator, secondNumerator, P, ras.Slice(hb, hb));

            return res;
        }

        /// <summary>
        /// Lower level function that performs a homomorphic multiplication of two encrypted BigInteger values.
        /// </summary>
        /// <param name="first">BigFraction to divide</param>
        /// <param name="second">BigFraction to divide by</param>
        /// <param name="P">ElGamal modulo</param>
        /// <param name="dest">Byte span to write the ElGamal encryption of the product of the two BigIntegers to</param>
        public static void MultiplyIntegers(ReadOnlySpan<byte> first, ReadOnlySpan<byte> second, byte[] P, Span<byte> dest)
        {
            var hb = first.Length >> 1;

            var A_left = new BigInteger(first.Slice(0, hb));
            var A_right = new BigInteger(first.Slice(hb, hb));

            var B_left = new BigInteger(second.Slice(0, hb));
            var B_right = new BigInteger(second.Slice(hb, hb));

            var Pbi = new BigInteger(P);

            var res_left = (A_left * B_left) % Pbi;
            var res_right = (A_right * B_right) % Pbi;

            res_left.TryWriteBytes(dest.Slice(0, hb), out _);
            res_right.TryWriteBytes(dest.Slice(hb, hb), out _);
        }
    }
}
