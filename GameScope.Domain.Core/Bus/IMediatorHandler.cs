using GameScope.Domain.Core.Commands;
using System.Threading.Tasks;

namespace GameScope.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
    }
}
