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

        public List<KhoanThuChi> GetTop3KhoanThuChiByCategory(Category category)
        {
            try
            {
                List<KhoanThuChi> ListKhoanThuChi = db.Table<KhoanThuChi>()
                    .Where(khoanThuChi => khoanThuChi.Category == category)
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
