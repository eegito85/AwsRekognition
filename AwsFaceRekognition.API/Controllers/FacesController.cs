using AwsFaceRekognition.API.Models;
using AwsFaceRekognition.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AwsFaceRekognition.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacesController : ControllerBase
    {
        private readonly IDetectFacesService _serviceDetectFaces;
        private readonly ICompareFacesService _serviceCompareFaces;

        public FacesController(
            IDetectFacesService serviceDetectFaces,
            ICompareFacesService serviceCompareFaces)
        {
            _serviceDetectFaces = serviceDetectFaces;
            _serviceCompareFaces = serviceCompareFaces;
        }

        /// <summary>
        /// Endpoint que compara duas faces em imagens e retorna o grau de similaridade entre as duas
        /// </summary>
        /// <param name="faceMatchRequest">Fotos a serem comparadas</param>
        /// <returns></returns>
        [HttpPost("facematch")]
        public async Task<IActionResult> GetFaceMatches([FromBody] FaceMatchRequest faceMatchRequest)
        {
            try
            {
                var result = await _serviceCompareFaces.CompareFacesAsync(
                    faceMatchRequest.SourceImage,
                    faceMatchRequest.TargetImage
                );

                return StatusCode(HttpStatusCode.OK.GetHashCode(), result);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("facematch/{sourceImage}/{targetImage}")]
        public async Task<IActionResult> ReturnFaceMatches(string sourceImage,string targetImage)
        {
            try
            {
                var result = await _serviceCompareFaces.CompareFacesAsync(
                    sourceImage,
                    targetImage
                );

                return StatusCode(HttpStatusCode.OK.GetHashCode(), result);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("findfaces")]
        public async Task<IActionResult> GetFaceMatches([FromBody] FindFacesRequest request)
        {
            try
            {
                var response = await _serviceDetectFaces.DetectFacesAsync(
                    request.SourceImage
                );

                return StatusCode(HttpStatusCode.OK.GetHashCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
