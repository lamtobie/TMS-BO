using Newtonsoft.Json;

namespace Services.Helper.Utils;

public static class JsonConvertUtils
{
    public static T ConvertDynamicToObject<T>(dynamic data)
    {
        return JsonConvert.DeserializeObject<T>(data.ToString());
    }
}