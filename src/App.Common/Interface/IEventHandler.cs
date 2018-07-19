using System.Threading.Tasks;

namespace App.Common.Interface
{
    public interface IEventHandler<in T> : IHandler
        where T : IEvent
    {
        Task Handle(T evnt);
    }
}