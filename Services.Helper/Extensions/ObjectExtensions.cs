using Newtonsoft.Json;

namespace Services.Helper.Extensions
{
    public static class ObjectExtensions
    {
        public static string AsJsonString(this object obj)
        {
            var content = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return content;
        }
    }
}
