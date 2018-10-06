using Quartic.Engine.Dto;
using Quartic.Engine.Business.Models;
namespace Quartic.Engine.Api.Common
{
    public static class ExtensionMethods
    {
        public static Message ToMessage(this Packet packet)
        {
            return new Message
            { };
        }

        public static Packet ToPacket(this Message message)
        {
            return new Packet()
            {

            };
        }
    }
}
