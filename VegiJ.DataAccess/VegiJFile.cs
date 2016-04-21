namespace VegiJ.DataAccess
{
    public class VegiJFile : BaseEntity
    {
        // name, id, byte[] data, content type
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }

        // TODO: Constuctors of file
    }
}
