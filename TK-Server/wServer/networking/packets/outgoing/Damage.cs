﻿using common;
using common.resources;
using System.Collections.Generic;

namespace wServer.networking.packets.outgoing
{
    public class Damage : OutgoingMessage
    {
        public int TargetId { get; set; }
        public ConditionEffectIndex Effects { get; set; }
        public int DamageAmount { get; set; }
        public bool Kill { get; set; }
        public byte BulletId { get; set; }
        public int ObjectId { get; set; }

        public override MessageId MessageId => MessageId.DAMAGE;

        protected override void Write(NWriter wtr)
        {
            wtr.Write(TargetId);
            List<byte> eff = new List<byte>();
            for (byte i = 1; i < 255; i++)
                if ((Effects & (ConditionEffectIndex)(1 << i)) != 0)
                    eff.Add(i);
            wtr.Write((byte)eff.Count);
            foreach (var i in eff) wtr.Write(i);
            wtr.Write(DamageAmount);
            wtr.Write(Kill);
            wtr.Write(BulletId);
            wtr.Write(ObjectId);
        }
    }
}
