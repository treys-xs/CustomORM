using System.Data;

namespace CustomORM
{
    public static class DataReaderExtensions
    {
        private static bool TryGetOrdinal(this IDataReader reader, string columnName,  out int ordinal)
        {
            ordinal = -1;

            for(int i = 0; i < reader.FieldCount; i++) 
            {
                if(reader.GetName(i) == columnName)
                {
                    ordinal = i;
                    return true;
                }
            }

            return false;
        }

        public static string GetString(this IDataReader reader, string columnName)
        {
            if(TryGetOrdinal(reader, columnName, out int ordinal))
                return reader.GetString(ordinal);
            return default;
        }
        public static int GetInt32(this IDataReader reader, string columnName)
        {
            if (TryGetOrdinal(reader, columnName, out int ordinal))
                return reader.GetInt32(ordinal);
            return default;
        }
    }
}
