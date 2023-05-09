using QuanLyThuChi.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyThuChi.Models
{
    public class KhoanThuChi
    {
        [PrimaryKey]
        public string Id { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public double Cost { get; set; }
    }
}
