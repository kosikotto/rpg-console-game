
namespace T0Y9UZ_HSZF_2024251.Model.Exceptions {
    [Serializable]
    public class LoadingFileNotFound : Exception
    {
        public LoadingFileNotFound()
        {
        }

        public LoadingFileNotFound(string? message) : base(message)
        {
        }

        public LoadingFileNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}