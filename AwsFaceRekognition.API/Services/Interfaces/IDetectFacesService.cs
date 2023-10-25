using AwsFaceRekognition.API.Models;

namespace AwsFaceRekognition.API.Services.Interfaces
{
    public interface IDetectFacesService
    {
        Task<FindFacesResponse> DetectFacesAsync(string sourceImage);
    }
}
