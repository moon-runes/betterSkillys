﻿using common;

namespace wServer.networking.packets.outgoing
{
    internal class InvitedToGuild : OutgoingMessage
    {
        public string Name;
        public string GuildName;

        public override PacketId MessageId => PacketId.INVITEDTOGUILD;

        public override Packet CreateInstance()
        {
            return new InvitedToGuild();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.WriteUTF(GuildName);
        }
    }
}
