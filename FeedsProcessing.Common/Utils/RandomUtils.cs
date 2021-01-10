using System;

namespace FeedsProcessing.Common.Utils
{
    public static class RandomUtils
    {
        public static string String(int length = 8) =>
            Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length).ToLowerInvariant();

    }
}
