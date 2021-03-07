using System;
using System.Text;

namespace HackAssembler.Lib.Extensions
{
    public static class IntExtensions
    {
        public static string ToBinary(this int number, int bitsAmount)
        {
            if (number >= 0)
            {

                var bin = Convert.ToString(number, 2);
                return bin.PadLeft(bitsAmount, '0');
            }
            else
            {
                var maxNegativeValue = (int)Math.Pow(2, bitsAmount-1);
                if (number < -maxNegativeValue)
                    throw new ArgumentException("Given number cannot be represented with given bitsAmount",
                        nameof(bitsAmount));

                var rest = maxNegativeValue + number;

                var bits = rest.ToBinary(bitsAmount - 1);
                return $"1{bits}";
            }
        } 
    }
}