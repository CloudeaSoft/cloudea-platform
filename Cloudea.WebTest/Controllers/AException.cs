namespace Cloudea.WebTest.Controllers
{
    public sealed class AException : Exception
    {
        public AException(int orderId)
            : base($"The order {orderId} has too many items")
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}
