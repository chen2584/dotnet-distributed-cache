namespace TestDistributeCache.Models
{
    public class RedisSetting
    {
        public string Host { get; set; }
        public string InstanceName { get; set; }
    }

    public class AppSetting
    {
        public RedisSetting Redis { get; set; }
    }
}