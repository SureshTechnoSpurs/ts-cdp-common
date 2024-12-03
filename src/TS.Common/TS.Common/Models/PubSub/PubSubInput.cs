namespace TS.Common.Models.PubSub
{
    public class PubSubInput
    {
        public string ProjectId { get; set; }
        public string TopicId { get; set; }
        public int ByteCountThreshold { get; set; } = 1000000;
        public int ElementCountThreshold { get; set; } = 100;
        public int DelayThreshold { get; set; } = 10;

    }
}
