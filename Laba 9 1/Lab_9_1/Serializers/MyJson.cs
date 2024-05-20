﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Laba_9.Serializers
{
    internal class MyJson : MySer
    {
        public override void Write<T>(T obj, string path)
        {
            using (FileStream fs= new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<T>(fs, obj);
            }
        }
        public override T Read<T>(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<T>(fs);
            }
        }
    }

}
