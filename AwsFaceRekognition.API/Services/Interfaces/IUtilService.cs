using aws = Amazon.Rekognition.Model;

namespace AwsFaceRekognition.API.Services.Interfaces
{
    public interface IUtilService
    {
        MemoryStream ConvertImageToMemoryStream(string imageBase64);
        string Drawing(MemoryStream imageSource, aws.ComparedSourceImageFace faceMatch);
        string Drawing(MemoryStream imageSource, List<aws.FaceDetail> faceDetails);
    }
}
