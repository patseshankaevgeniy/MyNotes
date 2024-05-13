using Application.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.MediatRPipeline;

public sealed class ExceptionHandlerBehaviour<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public ExceptionHandlerBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = typeof(TRequest).Name;
        var requestBody = request.ToString();
        try
        {
            return await next();
        }
        catch (ValidationException ex)
        {
            _logger.LogInformation($"Can not execute {requestName}: [ {ex.FormattedMessage} ]");
            throw;
        }
        catch (NotFoundException ex)
        {
            _logger.LogInformation($"{nameof(NotFoundException)} in {requestName}: {ex.Message}");
            throw;
        }
        catch (BadRequestException ex)
        {
            _logger.LogInformation($"{nameof(BadRequestException)} in {requestName}: {ex.Message}");
            throw;
        }
        catch (ForbiddenException ex)
        {
            _logger.LogError(ex, $"Access is denied: {requestName} {requestBody}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unhandled exception in {requestName}: {ex.Message}");
            throw;
        }
    }
}