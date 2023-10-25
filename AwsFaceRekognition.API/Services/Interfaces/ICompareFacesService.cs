using AwsFaceRekognition.API.Models;

namespace AwsFaceRekognition.API.Services.Interfaces
{
    public interface ICompareFacesService
    {
        Task<string> CompareFacesAsync(string sourceImage, string targetImage);
    }
}
