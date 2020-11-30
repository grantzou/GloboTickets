using System.Threading.Tasks;

namespace GloboTickets.Promotion.Data
{
    public interface INotifier<T>
    {
        Task Notify(T entityAdded);
    }
}
