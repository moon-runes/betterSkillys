﻿using TKR.Shared;

namespace TKR.WorldServer.networking.packets.outgoing
{
    public class Text : OutgoingMessage
    {
        public string Name { get; set; }
        public int ObjectId { get; set; }
        public int NumStars { get; set; }
        public byte BubbleTime { get; set; }
        public string Recipient { get; set; }
        public string Txt { get; set; }
        public string CleanText { get; set; }
        public int NameColor { get; set; } = 0;
        public int TextColor { get; set; } = 0;

        public override MessageId MessageId => MessageId.TEXT;

        public override void Write(NWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.Write(ObjectId);
            wtr.Write(NumStars);
            wtr.Write(BubbleTime);
            wtr.WriteUTF(Recipient);
            wtr.WriteUTF(Txt);
            wtr.WriteUTF(CleanText);
            wtr.Write(NameColor);
            wtr.Write(TextColor);
        }
    }
}
