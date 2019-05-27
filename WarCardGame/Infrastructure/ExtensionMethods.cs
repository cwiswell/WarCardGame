using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace WarCardGame.Infrastructure
{
    internal static class ExtensionMethods
    {
        //Taken from Stackoverflow answer https://stackoverflow.com/questions/273313/randomize-a-listt
        //which is an implentation of the Fisher-Yates shuffle
        public static void Shuffle<T>(this IList<T> list)
        {
            var provider = new RNGCryptoServiceProvider();
            var n = list.Count;
            while (n > 1)
            {
                var box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                var k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
