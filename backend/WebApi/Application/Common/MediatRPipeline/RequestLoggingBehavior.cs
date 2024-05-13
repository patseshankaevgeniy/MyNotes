using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.MediatRPipeline;

public sealed class RequestLoggingBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public RequestLoggingBehavior(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var currentUserEmail = _currentUserService.User?.Email ?? "Application";
        var requestBody = JsonConvert.SerializeObject(request, Formatting.None);

        _logger.LogInformation(
            "- [{RequestName}] started: {RequestBody} for userId: {UserEmail}",
            typeof(TRequest).Name,
            requestBody,
            currentUserEmail);

        return next();
    }
}