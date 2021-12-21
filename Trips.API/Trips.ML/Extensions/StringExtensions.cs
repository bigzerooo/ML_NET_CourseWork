namespace Trips.ML.API.Extensions
{
    public static class StringExtensions
    {
        public static string AbsolutePath(this string relativeDatasetPath)
        {
            FileInfo dataRoot = new FileInfo(typeof(StringExtensions).Assembly.Location);
            string assemblyFolderPath = dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativeDatasetPath);

            return fullPath;
        }
    }
}
