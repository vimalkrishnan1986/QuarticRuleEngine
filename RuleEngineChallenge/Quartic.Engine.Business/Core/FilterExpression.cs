using Quartic.Engine.Business.Enums;

namespace Quartic.Engine.Business.Core
{
    public class FilterExpression
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public Operators Operators { get; set; }
    }
}
