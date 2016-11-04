using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace QuanLyLinhKienDienThoai.Common
{
    public class MaHoa
    {
        /// <summary>
        /// phuong thuc dung de ma hoa
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static string EncryptMD5(string Data)
        {
            MD5CryptoServiceProvider myMD5 = new MD5CryptoServiceProvider();
            byte[] b = System.Text.Encoding.UTF8.GetBytes(Data);
            b = myMD5.ComputeHash(b);
            StringBuilder s = new StringBuilder();
            foreach (var item in b)
            {
                s.Append(item.ToString("x").ToLower());

            }
            return s.ToString();
        }
    }
}