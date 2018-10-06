using Quartic.Engine.Business.Enums;

namespace Quartic.Engine.Business.Models
{
    public class Message
    {
        public string Signal { get; set; }
        public string Value { get; set; }
        public DataTypes Value_Type { get; set; }
    }
}
