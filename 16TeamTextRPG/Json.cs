using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _16TeamTextRPG
{
    internal class Json
    {
        public string filePath;

        public Json(string _filePath)
        {
            filePath = _filePath;
        }

        public void Save(object obj)
        {
            // Json 직렬화
            string serialize = JsonConvert.SerializeObject(obj);

            // File로 저장
            File.WriteAllText(filePath, serialize);
        }

        public T Load<T>() where T : class
        {
            if (!File.Exists(filePath))
                return null;

            string serialize = File.ReadAllText(filePath);

            // Json 역직렬화
            return JsonConvert.DeserializeObject<T>(serialize);
        }
    }
}
