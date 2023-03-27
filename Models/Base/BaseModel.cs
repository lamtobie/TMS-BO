using System.Text.Json.Serialization;

namespace Services.Models.Base
{
    public class BaseModel
    {
        public BaseModel()
        {
            Success = true;
            ErrorCode = null;
            ErrorMessage = null;
        }

        public bool Success { get; set; }
        
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        [JsonIgnore]
        public bool HasError => string.IsNullOrEmpty(ErrorMessage);
    }

    public class BaseModel<T> : BaseModel where T : class, new()
    {
        public BaseModel() : base()
        {
            Data = new T();
        }

        [JsonPropertyName("data")]
        public T Data {get;set;}
    }

    public class BaseListModel<T> : BaseModel where T : class, new()
    {
        [JsonIgnore]
        public List<T> Items { get; set; }
        public ItemCollection<T> Data
        {
            get => new ItemCollection<T> { Items = Items };
        }
        public BaseListModel() : base()
        {
            Items = new List<T>();
        }
    }

    public record ItemCollection<T>
    {
        public List<T> Items { get; set; }
    }
}
