using Amazon.Rekognition.Model;
using Amazon.Rekognition;
using AwsFaceRekognition.API.Models;
using AwsFaceRekognition.API.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Amazon;

namespace AwsFaceRekognition.API.Services
{
    public class CompareFacesService : ICompareFacesService
    {
        private readonly IUtilService _serviceUtils;
        private readonly AmazonRekognitionClient _rekognitionClient;

        public CompareFacesService(IUtilService serviceUtils)
        {
            _serviceUtils = serviceUtils;
            _rekognitionClient = new AmazonRekognitionClient();
        }

        public async Task<string> CompareFacesAsync(string sourceImage, string targetImage)
        {
            using (var rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.USEast1))
            {
                try
                {
                    // Converte a imagem fonte em um objeto MemoryStream
                    var imageSource = new Amazon.Rekognition.Model.Image();
                    using (FileStream fs = new FileStream(sourceImage, FileMode.Open, FileAccess.Read))
                    {
                        byte[] data = new byte[fs.Length];
                        fs.Read(data, 0, (int)fs.Length);
                        imageSource.Bytes = new MemoryStream(data);
                    }

                    // Converte a imagem alvo em um objeto MemoryStream
                    var imageTarget = new Amazon.Rekognition.Model.Image();
                    using (FileStream fs = new FileStream(targetImage, FileMode.Open, FileAccess.Read))
                    {
                        byte[] data = new byte[fs.Length];
                        data = new byte[fs.Length];
                        fs.Read(data, 0, (int)fs.Length);
                        imageTarget.Bytes = new MemoryStream(data);
                    }

                    // Configura o objeto que fará o request para o AWS Rekognition
                    // A propriedade SimilarityThreshold ajusta o nível de similaridade na comparação das imagens
                    var request = new CompareFacesRequest
                    {
                        SourceImage = imageSource,
                        TargetImage = imageTarget,
                        SimilarityThreshold = 90f
                    };

                    // Faz a chamada do serviço de CompareFaces
                    var response = await rekognitionClient.CompareFacesAsync(request);

                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Verifica se houve algum match nas imagens
                        var hasMatch = response.FaceMatches.Any();

                       

                        // Se não houve match ele retorna um objeto não encontrado
                        if (!hasMatch)
                        {
                            return "Discordância entre as imagens!";
                        }

                        // Pega o percentual de similaridade da imagem encontrada
                        var similarity = response.FaceMatches.FirstOrDefault().Similarity;

                        // Retorna o objeto com as informações encontradas e com a URL para verificar a imagem
                        return $"Houve concordância de {similarity} % entre as fotos!";
                    }

                    return "Não foi possível fazer a verificação entre imagens!";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
