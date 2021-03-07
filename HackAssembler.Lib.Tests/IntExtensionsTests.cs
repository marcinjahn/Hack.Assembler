using System;
using HackAssembler.Lib.Extensions;
using NUnit.Framework;

namespace HackAssembler.Lib.Tests
{
    public class IntExtensionsTests
    {
        [TestCase(0, "0000000000000000")]
        [TestCase(4, "0000000000000100")]
        [TestCase(-1, "1111111111111111")]
        public void ToBinary_NumberIsGiven_ProperBianryFormIsReturned(int intNumber, string binNumber)
        {
            var result = intNumber.ToBinary(16);
            
            Assert.AreEqual(binNumber, result);
        }
    }
}