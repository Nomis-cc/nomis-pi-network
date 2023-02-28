// ------------------------------------------------------------------------------------------------------
// <copyright file="PiNetworkHelpers.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;

namespace Nomis.PiBlockexplorer.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods for Pi Network.
    /// </summary>
    public static class PiNetworkHelpers
    {
        /// <summary>
        /// Convert Wei value to Pi.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Pi.</returns>
        public static decimal ToPi(this in BigInteger valueInWei)
        {
            if (valueInWei > new BigInteger(decimal.MaxValue))
            {
                return (decimal)(valueInWei / new BigInteger(1_000_000));
            }

            return (decimal)valueInWei * 0.000_000_1M;
        }

        /// <summary>
        /// Convert Wei value to Pi.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Pi.</returns>
        public static decimal ToPi(this decimal valueInWei)
        {
            return new BigInteger(valueInWei).ToPi();
        }

        /// <summary>
        /// Convert Wei value to Pi multiplied value.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Pi multiplied value.</returns>
        public static decimal ToPiMultiplied(this string? valueInWei)
        {
            decimal.TryParse(valueInWei, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out decimal value);
            return value * 10000000;
        }
    }
}