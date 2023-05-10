using QuanLyThuChi.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyThuChi.Models
{
    public class KhoanThuChi
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public double Cost { get; set; }
        public string Color { get; set; }
        public string ColorString => Color = Category == Category.THU ? "#169F00" : "#B12B00";
    }
}
