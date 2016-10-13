using DomainModel.Entities;

namespace DomainModel.Services
{
    public interface IOrderSubmitter
    {
        void SubmitOrder(Cart cart);
    }
}