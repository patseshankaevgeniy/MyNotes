using Application.Users.Commands;
using Application.Users.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Infrastructure;
using WebApi.IntegrationTests.Infrastructure.Client;

namespace IntegrationTests.Infrastructure.Auth;

public sealed class AuthContext
{
    private readonly HttpClient _httpClient;
    private readonly IMynotesClient _myNotesClient;
    private readonly IServiceProvider _serviceProvider;

    public AuthContext(
        HttpClient httpClient,
        IMynotesClient myNotesClient,
        IServiceProvider serviceProvider)
    {
        _httpClient = httpClient;
        _myNotesClient = myNotesClient;
        _serviceProvider = serviceProvider;
    }

    public async Task AuthenticateAsync(Action<AuthenticationOptions> setupOptionsAction)
    {
        var options = new AuthenticationOptions
        {
            Password = SeedData.DefaultPassword
        };

        setupOptionsAction?.Invoke(options);

        var loginResult = await _myNotesClient.LogInAsync(new() { Email = options.Email, Password = options.Password });
        SetAuthorizationHeader(loginResult.AccessToken);
    }

    public async Task<UserModel> CreateUserAsync(Action<CreateUserCommand>? setupUserAction = null)
    {
        var mediator = _serviceProvider.GetRequiredService<IMediator>();

        var createUserCommand = new CreateUserCommand
        {
            UserName = $"UserName_{Guid.NewGuid()}",
            FirstName = $"FirstName_{Guid.NewGuid()}",
            SecondName = $"SecondName_{Guid.NewGuid()}",
            Email = $"{Guid.NewGuid()}@gamil.com",
            Password = SeedData.DefaultPassword
        };

        setupUserAction?.Invoke(createUserCommand);

        return await mediator.Send(createUserCommand);
    }

    public async Task<UserModel> CreateUserAndAuthenticateAsync(Action<CreateUserCommand>? setupUserAction = null)
    {
        var mediator = _serviceProvider.GetRequiredService<IMediator>();

        var createUserCommand = new CreateUserCommand
        {
            UserName = $"UserName_{Guid.NewGuid()}",
            FirstName = $"FirstName_{Guid.NewGuid()}",
            SecondName = $"SecondName_{Guid.NewGuid()}",
            Email = $"{Guid.NewGuid()}@gmail.com",
            Password = SeedData.DefaultPassword
        };

        setupUserAction?.Invoke(createUserCommand);
        var userModel = await mediator.Send(createUserCommand);

        await AuthenticateAsync(options =>
        {
            options.Email = userModel.Email;
            options.Password = createUserCommand.Password;
        });

        return userModel;
    }

    public async Task<UserModel> CreateMemberAsync()
    {
        var mediator = _serviceProvider.GetRequiredService<IMediator>();

        var member = await CreateUserAsync();
        await _myNotesClient.CreateMemberAsync(new CreateMemberDto { UserId = member.UserId });

        return member;
    }

    public void SetAuthorizationHeader(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new(JwtBearerDefaults.AuthenticationScheme, accessToken);
    }
}