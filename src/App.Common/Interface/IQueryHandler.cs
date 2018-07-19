using System.Threading.Tasks;

namespace App.Common.Interface
{
    public interface IQueryHandler<in TQuery, TResult> : IHandler
        where TResult : IQueryResult
        where TQuery : IQuery
    {
        Task<TResult> Execute(TQuery query);
    }
}