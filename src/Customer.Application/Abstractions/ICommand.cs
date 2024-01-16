using MediatR;

namespace Customer.Application.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}