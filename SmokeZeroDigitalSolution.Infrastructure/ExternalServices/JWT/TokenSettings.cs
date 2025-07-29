namespace SmokeZeroDigitalSolution.Infrastructure.ExternalServices.JWT
{
    public class TokenSettings
    {
        public string Key { get; init; } = null!;
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public int ExpireInMinute { get; init; }
        public double ClockSkewInMinute { get; init; }
    }
}
