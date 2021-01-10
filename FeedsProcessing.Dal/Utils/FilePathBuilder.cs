using FeedsProcessing.Common;
using FeedsProcessing.Common.Models;
using System.IO;

namespace FeedsProcessing.Dal.Utils
{
    internal static class FilePathBuilder
    {
        public static string Build(BackingStoreInfo info, string fileName)
        {
            var path = string.Format(Constants.PathFormat, info.Source, info.DirIndex);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path, string.Format(fileName, info.FileId));
        }
    }
}
