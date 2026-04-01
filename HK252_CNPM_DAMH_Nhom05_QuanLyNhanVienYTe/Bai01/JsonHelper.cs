using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuanLyNhanVienYTe
{
    public static class JsonHelper
    {
        private static string filePathCaTruc = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "catruc.json"
        );
        private static string filePath = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
            "nhanvien.json"
        );

        public static List<NhanVien> Load()
        {
            if (!File.Exists(filePath))
                return new List<NhanVien>();

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(json))
                return new List<NhanVien>();

            return JsonConvert.DeserializeObject<List<NhanVien>>(json);
        }

        public static List<CaTruc> LoadCaTruc()
        {
            if (!File.Exists(filePathCaTruc))
                return new List<CaTruc>();

            string json = File.ReadAllText(filePathCaTruc);

            if (string.IsNullOrEmpty(json))
                return new List<CaTruc>();

            return JsonConvert.DeserializeObject<List<CaTruc>>(json);
        }
        public static void SaveCaTruc(List<CaTruc> list)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(filePathCaTruc, json);
        }

        public static void Save(List<NhanVien> list)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }


}