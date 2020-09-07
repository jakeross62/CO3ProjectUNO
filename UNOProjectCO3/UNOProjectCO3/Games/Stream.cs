using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UNOProjectCO3.Games
{
    public static class Stream
    {
        public static void WriteLongString(BinaryWriter w, string s)
        {
            w.Write((ushort)s.Length);
            var chars = Encoding.UTF8.GetBytes(s);
            w.Write(chars);
        }

        public static string ReadLongString(BinaryReader r)
        {
            var len = (int)r.ReadUInt16();
            var chars = r.ReadBytes(len);
            return Encoding.UTF8.GetString(chars);
        }
    }
}
