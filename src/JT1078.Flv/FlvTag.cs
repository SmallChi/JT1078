namespace JT1078.Flv
{
    public class FlvTag
    {
        public FlvTagHeader TagHeader { get; set; }
        /// <summary>
        /// 根据tag类型
        /// </summary>
        public byte[] TagData { get; set; }
    }
}