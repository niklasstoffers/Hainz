using MessagePack;
using MessagePack.Formatters;

namespace Hainz.Data.Helpers;

public class DBNullFormatter : IMessagePackFormatter<DBNull>
{
    public static DBNullFormatter Instance => new();

    private DBNullFormatter() { }

    public void Serialize(ref MessagePackWriter writer, DBNull value, MessagePackSerializerOptions options) => writer.WriteNil();
    public DBNull Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options) => DBNull.Value;
}