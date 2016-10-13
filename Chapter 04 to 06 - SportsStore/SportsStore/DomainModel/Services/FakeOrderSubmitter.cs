using DomainModel.Entities;

namespace DomainModel.Services
{
    public class FakeOrderSubmitter : IOrderSubmitter
    {
        public void SubmitOrder(Cart cart)
        {
            // Do nothing
        }
    }
}