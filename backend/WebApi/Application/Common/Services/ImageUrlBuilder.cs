using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Services;

public sealed class ImageUrlBuilder : IImageUrlBuilder
{
    private readonly IConfiguration _configuration;

    public ImageUrlBuilder(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Build(string imageId)
    {
        return string.Format(_configuration["UserImageUrl"], imageId);
    }
}