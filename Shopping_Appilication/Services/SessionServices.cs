using Newtonsoft.Json;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public static class SessionServices
    {
        public static List<CartItem> GetObjFromSession(ISession session, string key)
        {
            // Lấy string json từ Session
            var jsonData = session.GetString(key);
            if (jsonData == null) return new List<CartItem>();//Nếu null trả về một list rỗng
            //Chuyển đổi dl vừa lấy đc sang dạng mong muốn
            var products = JsonConvert.DeserializeObject<List<CartItem>>(jsonData);
            //Nếu null trả về một list rỗng
            return products;
        }
        public static void SetObjToSession(ISession session, string key, object values)
        {
            var jsonData = JsonConvert.SerializeObject(values);
            session.SetString(key, jsonData);
        }
        public static bool CheckObjInList(Guid id, List<Product> products)
        {
            return products.Any(c => c.Id == id);
        }
    }
}
