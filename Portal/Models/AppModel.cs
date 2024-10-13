using Portal.Data.SearchModel;

namespace Portal.Models
{
    public class AppModel<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
        public PageModel PageModel { get; set; }
    }
}
