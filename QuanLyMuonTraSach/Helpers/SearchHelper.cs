using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyMuonTraSach.Helpers
{
    public static class SearchHelper
    {
        /// <summary>
        /// Hàm này sẽ lọc một DataTable dựa trên các cột được chỉ định.
        /// Nó hoạt động bằng cách sửa đổi thuộc tính RowFilter của DataView.
        /// </summary>
        /// <param name="dataTable">Bảng dữ liệu (ví dụ: dtDocGia, dtSach) cần lọc.</param>
        /// <param name="searchText">Từ khóa tìm kiếm (từ TextBox).</param>
        /// <param name="columnsToSearch">Danh sách tên các cột cần tìm (ví dụ: "HoTen", "SoDienThoai").</param>
        public static void ApplyFilter(DataTable dataTable, string searchText, params string[] columnsToSearch)
        {
            // Nếu không có DataTable (ví dụ: form chưa tải xong), thì không làm gì cả
            if (dataTable == null)
            {
                return;
            }

            // Lấy DataView mặc định của DataTable
            DataView dv = dataTable.DefaultView;

            // 1. Nếu ô tìm kiếm trống, xóa bộ lọc và hiển thị lại tất cả
            if (string.IsNullOrWhiteSpace(searchText))
            {
                dv.RowFilter = string.Empty;
                return;
            }

            // 2. Xây dựng chuỗi truy vấn RowFilter

            // Hàm này "làm sạch" từ khóa tìm kiếm để tránh lỗi
            // Ví dụ: nếu người dùng tìm "O'Brien", dấu ' sẽ gây lỗi cú pháp
            string sanitizedSearchText = Sanitize(searchText);

            // Dùng List để lưu các điều kiện OR
            var filterParts = new List<string>();

            foreach (string column in columnsToSearch)
            {
                // Đảm bảo cột có tồn tại trong DataTable
                if (dataTable.Columns.Contains(column))
                {
                    // Thêm điều kiện LIKE
                    // Ví dụ: "HoTen LIKE '%Nguyễn%'"
                    // Cú pháp %...% nghĩa là "chứa"
                    filterParts.Add($"{column} LIKE '%{sanitizedSearchText}%'");
                }
            }

            // Nếu có ít nhất 1 điều kiện, nối chúng lại bằng "OR"
            // Ví dụ: "HoTen LIKE '%An%' OR SoDienThoai LIKE '%An%'"
            if (filterParts.Any())
            {
                try
                {
                    dv.RowFilter = string.Join(" OR ", filterParts);
                }
                catch (Exception ex)
                {
                    // Xử lý nếu cú pháp filter bị lỗi
                    System.Diagnostics.Debug.WriteLine("Lỗi RowFilter: " + ex.Message);
                    dv.RowFilter = string.Empty;
                }
            }
            else
            {
                dv.RowFilter = string.Empty; // Không có cột nào hợp lệ để tìm
            }
        }

        /// <summary>
        /// Hàm nội bộ để "làm sạch" text, thay thế dấu ' thành ''
        /// </summary>
        private static string Sanitize(string text)
        {
            return text.Replace("'", "''");
        }
    }
}
