using MediatR;

namespace BasketProject.Contracts.Commands
{
    public class SetQuantitiesCommand : IRequest
    {
        public int Id { get; set; }
        public IDictionary<string, byte> Items { get; set; }
    }
}
