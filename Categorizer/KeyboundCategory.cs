using System.Windows.Input;
namespace Categorizer
{
     public class KeyboundCategory
    {
        public KeyboundCategory()
        {
        }

        public KeyboundCategory(Key key, string category)
        {
            this.key = key;
            this.category = category;
        }

        public Key key { get; set; }
        public string category { get; set; }
    }
}