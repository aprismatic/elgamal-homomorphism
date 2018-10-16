/************************************************************************************
 This implementation of the ElGamal encryption scheme is based on the code from [1].

 This library is provided as-is and is covered by the MIT License [2] (except for the
 parts that belong to O'Reilly - they are covered by [3]).

 [1] Adam Freeman & Allen Jones, Programming .NET Security: O'Reilly Media, 2003,
     ISBN 9780596552275 (http://books.google.com.sg/books?id=ykXCNVOIEuQC)

 [2] The MIT License (MIT), website, (http://opensource.org/licenses/MIT)

 [3] Tim O'Reilly, O'Reilly Policy on Re-Use of Code Examples from Books: website,
     2001, (http://www.oreillynet.com/pub/a/oreilly/ask_tim/2001/codepolicy.html)
 ************************************************************************************/

using System;
using System.Numerics;

namespace ElGamalExt.Homomorphism
{
    public static class ElGamalHomomorphism
    {
        public static byte[] Multiply(byte[] p_first, byte[] p_second, byte[] p_P)
        {
            var p_first_numerator = new byte[p_first.Length / 2];
            Array.Copy(p_first, p_first_numerator, p_first.Length / 2);
            var p_first_denominator = new byte[p_first.Length / 2];
            Array.Copy(p_first, p_first.Length / 2, p_first_denominator, 0, p_first.Length / 2);
            var p_second_numerator = new byte[p_second.Length / 2];
            Array.Copy(p_second, p_second_numerator, p_second.Length / 2);
            var p_second_denominator = new byte[p_second.Length / 2];
            Array.Copy(p_second, p_second.Length / 2, p_second_denominator, 0, p_second.Length / 2);

            var mul_numerator = MultiplyParts(p_first_numerator, p_second_numerator, p_P);
            var mul_denominator = MultiplyParts(p_first_denominator, p_second_denominator, p_P);

            var mul = new byte[p_first.Length];
            Array.Copy(mul_numerator, 0, mul, 0, mul_numerator.Length);
            Array.Copy(mul_denominator, 0, mul, mul.Length / 2, mul_denominator.Length);

            return mul;
        }

        public static byte[] Divide(byte[] p_first, byte[] p_second, byte[] p_P)
        {
            var p_first_numerator = new byte[p_first.Length / 2];
            Array.Copy(p_first, p_first_numerator, p_first.Length / 2);
            var p_first_denominator = new byte[p_first.Length / 2];
            Array.Copy(p_first, p_first.Length / 2, p_first_denominator, 0, p_first.Length / 2);
            var p_second_numerator = new byte[p_second.Length / 2];
            Array.Copy(p_second, p_second_numerator, p_second.Length / 2);
            var p_second_denominator = new byte[p_second.Length / 2];
            Array.Copy(p_second, p_second.Length / 2, p_second_denominator, 0, p_second.Length / 2);

            var div_numerator = MultiplyParts(p_first_numerator, p_second_denominator, p_P);
            var div_denominator = MultiplyParts(p_first_denominator, p_second_numerator, p_P);

            var div = new byte[p_first.Length];
            Array.Copy(div_numerator, 0, div, 0, div_numerator.Length);
            Array.Copy(div_denominator, 0, div, div.Length / 2, div_denominator.Length);

            return div;
        }

        private static byte[] MultiplyParts(byte[] p_first, byte[] p_second, byte[] p_P)
        {
            var blocksize = p_first.Length;

            var res = new byte[blocksize];

            var temp = new byte[blocksize / 2];
            Array.Copy(p_first, temp, blocksize / 2);
            var A_left = new BigInteger(temp);
            Array.Copy(p_first, blocksize / 2, temp, 0, blocksize / 2);
            var A_right = new BigInteger(temp);
            Array.Copy(p_second, temp, blocksize / 2);
            var B_left = new BigInteger(temp);
            Array.Copy(p_second, blocksize / 2, temp, 0, blocksize / 2);
            var B_right = new BigInteger(temp);

            var P = new BigInteger(p_P);

            var res_left = (A_left * B_left) % P;
            var res_right = (A_right * B_right) % P;

            var cAbytes = res_left.ToByteArray();
            var cBbytes = res_right.ToByteArray();

            Array.Copy(cAbytes, 0, res, 0, cAbytes.Length);
            Array.Copy(cBbytes, 0, res, blocksize / 2, cBbytes.Length);

            return res;
        }
    }
}
