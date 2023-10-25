namespace AwsFaceRekognition.API.Models
{
    public class FaceMatchResponse
    {
        public FaceMatchResponse(bool match, float? similarity, string message)
        {
            Match = match;
            Similarity = similarity;
            Message = message;
        }

        public bool Match { get; private set; }
        public float? Similarity { get; private set; }
        public string Message { get; private set; }
    }
}
