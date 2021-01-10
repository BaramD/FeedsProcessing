using FeedsProcessing.Common;
using FeedsProcessing.Common.Models;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeedsProcessing.Dal
{
    public interface IProcessingStateDal
    {
        public Task<int> GetIndex(NotificationSource source);
    }

    public class ProcessingStateDal : IProcessingStateDal
    {
        private static readonly Encoding Encoder = Encoding.UTF8;

        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private ProcessingState _state;

        public async Task<int> GetIndex(NotificationSource source)
        {

            //multiple requests may try to obtain the new index
            //Therefore, to prevent raise conditions, we need to use locking mechanism.
            //It will ensure that only one thread obtains new index.
            await _lock.WaitAsync();

            try
            {
                var state = await Get();
                state.IncrementIndex(source);
                await Write(state);
                _state = state;
                return state.GetIndex(source);
            }
            finally
            {
                _lock.Release();
            }

        }

        private static async Task Write(ProcessingState state)
        {
            await using var stream = File.Open(Constants.StateFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            stream.SetLength(0);
            await using var reader = new StreamWriter(stream, Encoder);
            await reader.WriteAsync(state.ToString());
        }

        private static async Task<ProcessingState> Read()
        {
            if (!File.Exists(Constants.StateFilePath))
                return new ProcessingState();

            await using var stream = File.Open(Constants.StateFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            stream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(stream, Encoder);
            var content = await reader.ReadToEndAsync();
            return ProcessingState.FromString(content) ?? new ProcessingState();
        }

        private async Task<ProcessingState> Get() => _state ??= await Read();
    }
}
