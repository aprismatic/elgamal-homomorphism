using System;
using System.Numerics;

namespace Aprismatic.ElGamal.Homomorphism
{
    /// <summary>
    /// The class implements the homomorphism of the ElGamal encryption scheme.
    /// </summary>
    public static class ElGamalHomomorphism
    {
        /// <summary>
        /// Performs a homomorphic multiplication of two encrypted BigFraction values.
        /// </summary>
        /// <param name="first">First BigFraction to multiply</param>
        /// <param name="second">Second BigFraction to multiply</param>
        /// <param name="P">ElGamal modulo</param>
        /// <returns>Byte array that contains an ElGamal encryption of the product of the two BigFractions</returns>
        public static byte[] MultiplyFractions(ReadOnlySpan<byte> first, ReadOnlySpan<byte> second, ReadOnlySpan<byte> P)
        {
            var hb = first.Length >> 1;

            var firstNumerator = first.Slice(0, hb);
            var firstDenominator = first.Slice(hb, hb);

            var secondNumerator = second.Slice(0, hb);
            var secondDenominator = second.Slice(hb, hb);

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
        public static byte[] DivideFractions(ReadOnlySpan<byte> first, ReadOnlySpan<byte> second, ReadOnlySpan<byte> P)
        {
            var hb = first.Length >> 1;

            var firstNumerator = first.Slice(0, hb);
            var firstDenominator = first.Slice(hb, hb);

            var secondNumerator = second.Slice(0, hb);
            var secondDenominator = second.Slice(hb, hb);

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
        public static void MultiplyIntegers(ReadOnlySpan<byte> first, ReadOnlySpan<byte> second, ReadOnlySpan<byte> P, Span<byte> dest)
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
