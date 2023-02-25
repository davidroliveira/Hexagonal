using System.Data.SqlClient;

namespace ProjetoHexagonal.Commons.Persistence.SqlServer
{
    public sealed class SqlServerReader : IReader
    {
        private readonly SqlDataReader reader;

        public SqlServerReader(SqlDataReader reader)
        {
            this.reader = reader;
        }

        public bool Read()
        {
            return reader.Read();
        }

        public byte Byte(string column, byte? value = null)
        {
            var result = OptionalByte(column, value);

            if (!result.HasValue)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result.Value;
        }

        public byte? OptionalByte(string column, byte? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value;
            }

            return reader.GetByte(ordinal);
        }

        public byte[] ByteArray(string column, byte[]? value = null)
        {
            var result = OptionalByteArray(column, value!);

            if (result == null)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result;
        }

        public byte[] OptionalByteArray(string column, byte[]? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value!;
            }

            return reader.GetSqlBinary(ordinal).Value;
        }

        public bool Bool(string column, bool? value = null)
        {
            var result = OptionalBool(column, value);

            if (!result.HasValue)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result.Value;
        }

        public bool? OptionalBool(string column, bool? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value;
            }

            return reader.GetBoolean(ordinal);
        }

        public char Char(string column, char? value = null)
        {
            var result = OptionalChar(column, value);

            if (!result.HasValue)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result.Value;
        }

        public char? OptionalChar(string column, char? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value;
            }

            return reader.GetChar(ordinal);
        }

        public DateTime DateTime(string column, DateTime? value = null)
        {
            var result = OptionalDateTime(column, value);

            if (!result.HasValue)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result.Value;
        }

        public DateTime? OptionalDateTime(string column, DateTime? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value;
            }

            return reader.GetDateTime(ordinal);
        }

        public DateTime DateTime(string dateColumn, string timeColumn, DateTime? value = null)
        {
            var result = OptionalDateTime(dateColumn, timeColumn, value);

            if (!result.HasValue)
            {
                throw new InvalidOperationException("Mandatory field not found: '{dateColumn}|{timeColumn}'.");
            }

            return result.Value;
        }

        public DateTime? OptionalDateTime(string dateColumn, string timeColumn, DateTime? value = null)
        {
            var date = OptionalDateTime(dateColumn);

            if (date == null)
            {
                return value;
            }

            try
            {
                date += TimeSpan.Parse(OptionalString(timeColumn));
            }
            catch
            {
                // ignored
            }

            return date;
        }

        public DateTimeOffset DateTimeOffset(string column, DateTimeOffset? value = null)
        {
            var result = OptionalDateTimeOffset(column, value);

            if (!result.HasValue)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result.Value;
        }

        public DateTimeOffset? OptionalDateTimeOffset(string column, DateTimeOffset? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value;
            }

            return reader.GetDateTimeOffset(ordinal);
        }

        public int Int(string column, int? value = null)
        {
            var result = OptionalInt(column, value);

            if (!result.HasValue)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result.Value;
        }

        public int? OptionalInt(string column, int? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value;
            }

            return reader.GetInt32(ordinal);
        }

        public short Short(string column, short? value = null)
        {
            var result = OptionalShort(column, value);

            if (!result.HasValue)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result.Value;
        }

        public short? OptionalShort(string column, short? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value;
            }

            return reader.GetInt16(ordinal);
        }

        public decimal Decimal(string column, decimal? value = null)
        {
            var result = OptionalDecimal(column, value);

            if (!result.HasValue)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result.Value;
        }

        public decimal? OptionalDecimal(string column, decimal? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value;
            }

            return reader.GetDecimal(ordinal);
        }

        public Guid Guid(string column, Guid? value = null)
        {
            var result = OptionalGuid(column, value);

            if (result == null)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result.Value;
        }

        public Guid? OptionalGuid(string column, Guid? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value;
            }

            return reader.GetGuid(ordinal);
        }

        public long Long(string column, long? value = null)
        {
            var result = OptionalLong(column, value);

            if (!result.HasValue)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result.Value;
        }

        public long? OptionalLong(string column, long? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value;
            }

            return reader.GetInt64(ordinal);
        }

        public string String(string column, string? value = null)
        {
            var result = OptionalString(column, value!);

            if (result == null)
            {
                throw new InvalidOperationException($"Mandatory field not found: '{column}'.");
            }

            return result;
        }

        public string OptionalString(string column, string? value = null)
        {
            var ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
            {
                return value!;
            }

            return reader.GetString(ordinal);
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
