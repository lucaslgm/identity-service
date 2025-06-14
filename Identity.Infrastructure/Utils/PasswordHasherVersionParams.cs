namespace Identity.Infrastructure.Utils
{
    public record PasswordHasherVersionParams(
        string VersionPrefix,
        int SaltSize,
        int HashSize,
        int MemorySize,
        int Iterations,
        int DegreeOfParallelism
    )
    {
        public static PasswordHasherVersionParams GetParams(string version)
        {
            // Exemplo de parâmetros por versão
            return version switch
            {
                "v1" => new PasswordHasherVersionParams("v1", 16, 32, 65536, 4, 2),
                // Add other cases for additional versions as needed
                _ => throw new ArgumentException("Unknown hash version.", nameof(version))
            };
        }
    }
}