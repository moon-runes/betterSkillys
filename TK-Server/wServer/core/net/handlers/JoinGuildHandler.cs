﻿using common;
using common.database;
using System;
using wServer.core.worlds.logic;
using wServer.networking;

namespace wServer.core.net.handlers
{
    internal class JoinGuildHandler : IMessageHandler
    {
        public override PacketId MessageId => PacketId.JOINGUILD;

        public override void Handle(Client client, NReader rdr, ref TickTime tickTime)
        {
            var guildName = rdr.ReadUTF();
            if (client.Player == null || client?.Player?.World is TestWorld)
                return;

            if (client.Player.GuildInvite == null)
            {
                client.Player.SendError("You have not been invited to a guild.");
                return;
            }

            var guild = client.CoreServerManager.Database.GetGuild((int)client.Player.GuildInvite);

            if (guild == null)
            {
                client.Player.SendError("Internal server error.");
                return;
            }

            if (!guild.Name.Equals(guildName, StringComparison.InvariantCultureIgnoreCase))
            {
                client.Player.SendError("You have not been invited to join " + guildName + ".");
                return;
            }

            var result = client.CoreServerManager.Database.AddGuildMember(guild, client.Account);
            if (result != DbAddGuildMemberStatus.OK)
            {
                client.Player.SendError("Could not join guild. (" + result + ")");
                return;
            }

            client.Player.Guild = guild.Name;
            client.Player.GuildRank = 0;
            client.CoreServerManager.ChatManager.Guild(client.Player, client.Player.Name + " has joined the guild!");
        }
    }
}