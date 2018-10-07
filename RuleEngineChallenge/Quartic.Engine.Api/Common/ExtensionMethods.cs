using System;
using System.Collections.Generic;
using System.Linq;
using Quartic.Engine.Dto;
using Quartic.Engine.Business.Models;
using Quartic.Engine.Business.Enums;

namespace Quartic.Engine.Api.Common
{
    public static class ExtensionMethods
    {
        public static Message ToMessage(this Packet packet)
        {
            if (!Enum.IsDefined(typeof(DataTypes), packet.DataType))
            {
                throw new Exception($"Conversion failed {nameof(packet.DataType)}");
            }
            return new Message
            {
                Signal = packet.Signal,
                Value = packet.Value,
                Value_Type = (DataTypes)Enum.Parse(typeof(DataTypes), packet.DataType.ToString())
            };
        }

        public static Packet ToPacket(this Message message)
        {
            return new Packet
            {
                Signal = message.Signal,
                DataType = (int)message.Value_Type,
                Value = message.Value
            };
        }

        public static List<Message> ToMessages(this List<Packet> packets)
        {
            return (from packet in packets
                    select packet.ToMessage()
                    ).ToList();
        }

        public static List<Packet> ToPackets(this List<Message> messages)
        {
            return (from message in messages
                    select message.ToPacket()).ToList();
        }
    }
}
