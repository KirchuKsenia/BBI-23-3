using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Laba_9.Serializers
{
    internal class MyBin: MySer
    {
        public override void Write<T>(T obj, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Serializer.Serialize<T>(fs, obj);
            }
        }
        public override T Read<T>(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return Serializer.Deserialize<T>(fs);
            }
        }

    }
}
