namespace FeedsProcessing.Common.Models
{
    public class BackingStoreInfo
    {
        public BackingStoreInfo(NotificationSource source, int index, string id)
        {
            FileId = id;
            Source = source.ToString().ToLowerInvariant();
            DirIndex = index;
        }


        public string Source { get; }

        public string FileId { get; }

        public int DirIndex { get; }
    }
}
