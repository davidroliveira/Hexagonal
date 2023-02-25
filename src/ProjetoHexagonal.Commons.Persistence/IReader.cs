namespace ProjetoHexagonal.Commons.Persistence
{
    public interface IReader : IDisposable
    {
        bool Read();

        byte Byte(string column, byte? value = null);

        byte? OptionalByte(string column, byte? value = null);

        byte[] ByteArray(string column, byte[]? value = null);

        byte[] OptionalByteArray(string column, byte[]? value = null);

        bool Bool(string column, bool? value = null);

        bool? OptionalBool(string column, bool? value = null);

        char Char(string column, char? value = null);

        char? OptionalChar(string column, char? value = null);

        DateTime DateTime(string column, DateTime? value = null);

        DateTime? OptionalDateTime(string column, DateTime? value = null);

        DateTime DateTime(string dateColumn, string timeColumn, DateTime? value = null);

        DateTime? OptionalDateTime(string dateColumn, string timeColumn, DateTime? value = null);

        DateTimeOffset DateTimeOffset(string column, DateTimeOffset? value = null);

        DateTimeOffset? OptionalDateTimeOffset(string column, DateTimeOffset? value = null);

        int Int(string column, int? value = null);

        int? OptionalInt(string column, int? value = null);

        short Short(string column, short? value = null);

        short? OptionalShort(string column, short? value = null);

        decimal Decimal(string column, decimal? value = null);

        decimal? OptionalDecimal(string column, decimal? value = null);

        Guid Guid(string column, Guid? value = null);

        Guid? OptionalGuid(string column, Guid? value = null);

        long Long(string column, long? value = null);

        long? OptionalLong(string column, long? value = null);

        string String(string column, string? value = null);

        string OptionalString(string column, string? value = null);
    }
}
