using System.Threading.Tasks;

namespace App.Common.Interface
{
    public interface ICommandHandler<in T> : IHandler
        where T : ICommand
    {
        Task Execute(T command);
    }
}