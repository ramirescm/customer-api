using MediatR;

namespace Customer.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}