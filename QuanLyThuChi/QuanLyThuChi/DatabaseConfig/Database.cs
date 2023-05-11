using QuanLyThuChi.Enums;
using QuanLyThuChi.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace QuanLyThuChi.DatabaseConfig
{
    class Database
    {
        public readonly SQLiteConnection db;
        public Database()
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                db = new SQLiteConnection(System.IO.Path.Combine(folder, "QuanLyThuChi.db3"));
                db.CreateTable<KhoanThuChi>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool CreateKhoanThuChi(KhoanThuChi khoanThuChi)
        {
            try
            {
                db.Insert(khoanThuChi);
                return true;
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return false;
                throw;
            }
        }

        public List<KhoanThuChi> GetAllKhoanThuChi()
        {
            try
            {
                List<KhoanThuChi> ListKhoanThuChi = db.Table<KhoanThuChi>()
                    .OrderBy(khoanThuChi => khoanThuChi.Date)
                    .ToList();
                return ListKhoanThuChi;
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return null;
                throw;
            }
        }

        public double GetTotalCostKhoanThuChiByMonth(Category category, int month)
        {
            try
            {
                double totalCost = 0;
                totalCost = db.Table<KhoanThuChi>().ToList()
                    .Where(k => k.Category == category && k.Date.Month == month)
                    .Sum(k => k.Cost);
                return totalCost;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return 0;
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return 0;
                throw;
            }
        }

        public List<KhoanThuChi> GetAllKhoanThuChiByCategory(Category category)
        {
            try
            {
                List<KhoanThuChi> ListKhoanThuChi = db.Table<KhoanThuChi>()
                    .Where(khoanThuChi => khoanThuChi.Category == category)
                    .OrderBy(khoanThuChi => khoanThuChi.Date)
                    .ToList();
                return ListKhoanThuChi;
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return null;
                throw;
            }
        }

        public List<KhoanThuChi> GetTop3KhoanThuByMonth(int month)
        {
            try
            {
                List<KhoanThuChi> ListKhoanThuChi = db.Table<KhoanThuChi>()
                    .ToList()
                    .Where(khoanThuChi => khoanThuChi.Category == Category.THU && khoanThuChi.Date.Month == month)
                    .OrderByDescending(khoanThuChi => khoanThuChi.Cost)
                    .Take(3)
                    .ToList();
                return ListKhoanThuChi;
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return null;
                throw;
            }
        }

        public List<KhoanThuChi> GetTop3KhoanChiByMonth(int month)
        {
            try
            {
                List<KhoanThuChi> ListKhoanThuChi = db.Table<KhoanThuChi>()
                    .ToList()
                    .Where(khoanThuChi => khoanThuChi.Category == Category.CHI && khoanThuChi.Date.Month == month)
                    .OrderBy(khoanThuChi => khoanThuChi.Cost)
                    .Take(3)
                    .ToList();
                return ListKhoanThuChi;
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return null;
                throw;
            }
        }

        public KhoanThuChi GetKhoanThuChiById(Guid khoanThuChiId)
        {
            try
            {
                KhoanThuChi KhoanThuChiById = db.Table<KhoanThuChi>()
                    .SingleOrDefault(khoanThuChi => khoanThuChi.Id == khoanThuChiId);
                return KhoanThuChiById;
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return null;
                throw;
            }
        }

        public bool UpdateKhoanThuChi(KhoanThuChi khoanThuChi)
        {
            try
            {
                db.Update(khoanThuChi);
                return true;
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return false;
                throw;
            }
        }

        public bool DeleteKhoanThuChi(Guid khoanThuChiId)
        {
            try
            {
                var khoanThuChi = db.Table<KhoanThuChi>().FirstOrDefault(k => k.Id == khoanThuChiId);

                if (khoanThuChi != null)
                {
                    db.Delete(khoanThuChi);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return false;
                throw;
            }
        }
    }
}
