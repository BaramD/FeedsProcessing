using FeedsProcessing.Common;
using FeedsProcessing.Common.Models;
using FeedsProcessing.Dal.Utils;
using System.IO;
using System.Threading.Tasks;

namespace FeedsProcessing.Dal
{
    public interface INotificationDal
    {
        public Task Save(BackingStoreInfo stateInfo, string data);
    }

    public class NotificationDal : INotificationDal
    {
        public async Task Save(BackingStoreInfo info, string data)
        {
            var filePath = FilePathBuilder.Build(info, Constants.NotificationFileNameFormat);
            await File.WriteAllTextAsync(filePath, data);
        }

    }
}
