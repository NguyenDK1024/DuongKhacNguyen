using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongKhacNguyen
{
    //Class check ky tu nhap vao de chon chuc nang co dung khong?
    public class check_chuc_nang
    {
        public int Get_valid(int maxValue)
        {
            int validCharacter;

            do
            {
                Console.WriteLine("Nhập một ký tự hợp lệ (từ 1 đến " + maxValue + "):");
                string input = Console.ReadLine();

                if (int.TryParse(input, out validCharacter))
                {
                    // Kiểm tra điều kiện của ký tự ở đây
                    if (validCharacter < 1 || validCharacter > maxValue)
                    {
                        Console.WriteLine("Ký tự không thoả mãn điều kiện. Vui lòng nhập lại.");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Không phải ký tự số. Vui lòng nhập lại.");
                }
            } while (true);

            return validCharacter;
        }
    }
    //Class Công việc cần làm
    public class To_Do
    {
        public string TenCongViec { get; set; }
        public string DoUuTien { get; set; }
        public string NoiDungMoTa { get; set; }
        public string TrangThai { get; set; }

        public void NhapThongTin()//Chức năng 1
        {
            Console.WriteLine("Nhập thông tin công việc:");

            Console.Write("Tên công việc: ");
            TenCongViec = Console.ReadLine();

            Console.Write("Độ ưu tiên: ");
            DoUuTien = Console.ReadLine();
            while (!IsValidDoUuTien(DoUuTien))
            {
                Console.WriteLine("Độ ưu tiên không hợp lệ. Vui lòng nhập lại.");
                Console.Write("Độ ưu tiên: ");
                DoUuTien = Console.ReadLine();
            }

            Console.Write("Nội dung mô tả: ");
            NoiDungMoTa = Console.ReadLine();

            Console.WriteLine("Trạng thái công việc:");
            Console.WriteLine("1. Not Started");
            Console.WriteLine("2. In Progress");
            Console.WriteLine("3. In Review");
            Console.WriteLine("4. Completed");
            Console.Write("Chọn trạng thái (1-4): ");
            string trangThaiInput = Console.ReadLine();
            while (!IsValidTrangThai(trangThaiInput))
            {
                Console.WriteLine("Trạng thái không hợp lệ. Vui lòng nhập lại.");
                Console.Write("Chọn trạng thái (1-4): ");
                trangThaiInput = Console.ReadLine();
            }
            TrangThai = GetTrangThaiFromInput(trangThaiInput);
        }

        private bool IsValidDoUuTien(string doUuTien)
        {
            return doUuTien == "1" || doUuTien == "2" || doUuTien == "3" || doUuTien == "4" || doUuTien == "5";
        }

        public static bool IsValidTrangThai(string trangThai)
        {
            return trangThai == "1" || trangThai == "2" || trangThai == "3" || trangThai == "4";
        }

        public static string GetTrangThaiFromInput(string trangThaiInput)
        {
            switch (trangThaiInput)
            {
                case "1":
                    return "Not Started";
                case "2":
                    return "In Progress";
                case "3":
                    return "In Review";
                case "4":
                    return "Completed";
                default:
                    return "";
            }
        }
    }

    // class Chức năng
    public class Chuc_nang
    {

        public void XoaCongViec(List<To_Do> danhSachCongViec, int index)//Chức năng 2
        {
                danhSachCongViec.RemoveAt(index);
                Console.WriteLine("Xóa công việc thành công.");
        }
        public void XuatDanhSachCongViec_trangthai(List<To_Do> danhSachCongViec)//Xuất danh sách công việc hiện có chỉ bao gồm tên và trạng thái
        {
            Console.WriteLine("Danh sách công việc:");

            foreach (var item in danhSachCongViec)
            {
                Console.WriteLine("Tên công việc: '{0}' - Trạng Thái: {1}", item.TenCongViec, item.TrangThai);
                Console.WriteLine();
            }
        }

        public void CapNhatTrangThaiCongViec(List<To_Do> danhSachCongViec)//Chức năng 3
        {
            Console.WriteLine("Nhập tên công việc cần cập nhật trạng thái:");
            string tenCongViec = Console.ReadLine();

            bool found = false;

            foreach (var congViec in danhSachCongViec)
            {
                if (congViec.TenCongViec == tenCongViec)
                {
                    found = true;

                    Console.WriteLine("Nhập trạng thái mới cho công việc '{0}':", tenCongViec);

                    Console.WriteLine("1. Not Started");
                    Console.WriteLine("2. In Progress");
                    Console.WriteLine("3. In Review");
                    Console.WriteLine("4. Completed");
                    Console.Write("Chọn trạng thái mới từ 1 đến 4 ứng với trạng thái tương ứng phía trên: ");
                    string trangThaiInput = Console.ReadLine();
                    while (!To_Do.IsValidTrangThai(trangThaiInput))
                    {
                        Console.WriteLine("Trạng thái không hợp lệ. Vui lòng nhập lại.");
                        Console.Write("Chọn trạng thái mới từ 1 đến 4 ứng với trạng thái tương ứng phía trên: ");
                        trangThaiInput = Console.ReadLine();
                    }

                    string trangThaiMoi = To_Do.GetTrangThaiFromInput(trangThaiInput);
                    congViec.TrangThai = trangThaiMoi;
                    Console.WriteLine("Cập nhật trạng thái công việc thành công.");
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Không tìm thấy công việc có tên '{0}'.", tenCongViec);
            }
        }

        public List<int> TimViTriCongViec(List<To_Do> danhSachCongViec, int kieuTimKiem)// Chức năng 4 tìm vị trí công việc theo tên hoặc độ ưu tiên
        {
            List<int> viTriCongViec = new List<int>();

            Console.WriteLine("Nhập thông tin cần tìm:");

            if (kieuTimKiem == 1)
            {
                Console.Write("Nhập tên công việc: ");
                string tenCongViec = Console.ReadLine();

                for (int i = 0; i < danhSachCongViec.Count; i++)
                {
                    if (danhSachCongViec[i].TenCongViec.Equals(tenCongViec))
                    {
                        viTriCongViec.Add(i + 1);
                    }
                }

                if (viTriCongViec.Count > 0)
                {
                    Console.WriteLine("Vị trí công việc theo tên hiện đang đứng ở vị trí");
                    foreach (int viTri in viTriCongViec)
                    {
                        Console.WriteLine(viTri);
                    }
                }
                else
                {
                    Console.WriteLine("Không tìm thấy công việc ứng với tên này.");
                }
            }
            else if (kieuTimKiem == 2)
            {
                Console.Write("Nhập độ ưu tiên công việc: ");
                string doUuTien = Console.ReadLine();

                for (int i = 0; i < danhSachCongViec.Count; i++)
                {
                    if (danhSachCongViec[i].DoUuTien == doUuTien)
                    {
                        viTriCongViec.Add(i + 1);
                    }
                }

                if (viTriCongViec.Count > 0)
                {
                    Console.WriteLine("Vị trí công việc theo độ ưu tiên đang đứng ở vị trí thứ:");
                    foreach (int viTri in viTriCongViec)
                    {
                        Console.WriteLine(viTri);
                    }
                }
                else
                {
                    Console.WriteLine("Không tìm thấy công việc ứng với độ ưu tiên này.");
                }
            }

            return viTriCongViec;
        }

        public void InDanhSachCongViecGiamDanUuTien(List<To_Do> danhSachCongViec)//Chức năng 5
        {
            Console.WriteLine("Danh sách công việc cần làm theo thứ tự giảm dần của độ ưu tiên:");

            var danhSachSapXep = danhSachCongViec.OrderByDescending(congViec => congViec.DoUuTien);

            foreach (var congViec in danhSachSapXep)
            {
                Console.WriteLine("Tên công việc: {0}", congViec.TenCongViec);
                Console.WriteLine("Độ ưu tiên: {0}", congViec.DoUuTien);
                Console.WriteLine("Nội dung mô tả: {0}", congViec.NoiDungMoTa);
                Console.WriteLine("Trạng thái: {0}", congViec.TrangThai);
                Console.WriteLine();
            }
        }
       

        public void XuatDanhSachCongViec(List<To_Do> danhSachCongViec)//chức năng 6
        {
            Console.WriteLine("Danh sách công việc cần làm:");

            foreach (var item in danhSachCongViec)
            {
                Console.WriteLine("Tên công việc: " + item.TenCongViec);
                Console.WriteLine("Độ ưu tiên: " + item.DoUuTien);
                Console.WriteLine("Nội dung mô tả: " + item.NoiDungMoTa);
                Console.WriteLine("Trạng thái: " + item.TrangThai);
                Console.WriteLine();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            //Khai bao To_Do list
            List<To_Do> danhSachCongViec = new List<To_Do>();

            while (true)
            {
                Console.WriteLine("1. Chức năng nhập thông cho công việc cần làm");
                Console.WriteLine("2. Chức năng xóa công việc theo vị trí bạn mong muốn ");
                Console.WriteLine("3. Chức năng cập nhật trạng thái");
                Console.WriteLine("4. Chức năng tìm kiếm công việc theo tên hoặc độ ưu tiên");
                Console.WriteLine("5. Chức năng hiển thị danh sách công việc theo độ ưu tiên giảm dần");
                Console.WriteLine("6. Chức năng hiển thị danh sách công việc cần làm đang tồn tại");
                Console.WriteLine("Vui lòng chọn chức năng");

                check_chuc_nang validator = new check_chuc_nang();
                int chuc_nang = validator.Get_valid(6);

                switch (chuc_nang)
                {
                    case 1:
                        {
                            Console.WriteLine("Chức năng nhập thông cho công việc cần làm");
                            To_Do item = new To_Do();
                            item.NhapThongTin();
                            danhSachCongViec.Add(item);
                            break;
                        }
                    case 2:
                        {
                            if (danhSachCongViec.Count > 0)
                            {
                            Console.WriteLine("Chức năng xóa công việc theo vị trí bạn mong muốn");
                            Console.WriteLine("Công việc bạn muốn xóa đang ở vị trí? Vui lòng nhập vào giá trị hợp lệ hiển thị bên dưới:");
                            int  index = validator.Get_valid(danhSachCongViec.Count);
                                Chuc_nang chucNang2 = new Chuc_nang();
                                chucNang2.XoaCongViec(danhSachCongViec, index-1);  
                            }    
                            else { Console.WriteLine("Hiện tại không thể dùng chức năng này vì chưa có công việc cần làm: vui lòng nhập thông tin"); }
                            break;
                        }
                    case 3:
                        {
                            if (danhSachCongViec.Count > 0)
                            {
                                Console.WriteLine("Chuc Nang 3");
                                Chuc_nang chucNang3_1 = new Chuc_nang();
                                chucNang3_1.XuatDanhSachCongViec_trangthai(danhSachCongViec);
                                Chuc_nang ChucNang3 = new Chuc_nang();
                                ChucNang3.CapNhatTrangThaiCongViec(danhSachCongViec);
                            }
                            else { Console.WriteLine("Hiện tại không thể dùng chức năng này vì chưa có công việc cần làm: vui lòng nhập thông tin"); }
                            break;
                        }
                    case 4:
                        {
                            if (danhSachCongViec.Count > 0)
                            {
                                
                                Console.WriteLine("Chức năng tìm kiếm công việc theo tên hoặc độ ưu tiên");
                                Console.WriteLine("Tìm kiếm công việc theo tên vui lòng nhập số 1");
                                Console.WriteLine("Tìm kiếm công việc độ ưu tiên vui lòng nhập 2");
                                int type = validator.Get_valid(2);
                                Chuc_nang chucNang_4 = new Chuc_nang();
                                chucNang_4.TimViTriCongViec(danhSachCongViec, type);

                            }
                            else { Console.WriteLine("Hiện tại không thể dùng chức năng này vì chưa có công việc cần làm: vui lòng nhập thông tin"); }
                            break;
                        }
                    case 5:
                        {
                            if (danhSachCongViec.Count > 0)
                            {

                                Console.WriteLine("Chức năng hiển thị danh sách công việc theo độ ưu tiên giảm dần");
                                Chuc_nang chucNang_5 = new Chuc_nang();
                                chucNang_5.InDanhSachCongViecGiamDanUuTien(danhSachCongViec);
                                                            }
                            else { Console.WriteLine("Hiện tại không thể dùng chức năng này vì chưa có công việc cần làm: vui lòng nhập thông tin"); }
                            break;
                        }
                    case 6:
                        {
                            if (danhSachCongViec.Count > 0)
                            {
                                Console.WriteLine("Chức năng hiển thị danh sách công việc cần làm đang tồn tại");
                                Chuc_nang chucNang_6 = new Chuc_nang();
                                chucNang_6.XuatDanhSachCongViec(danhSachCongViec);
                            }
                            else { Console.WriteLine("Hiện tại không thể dùng chức năng này vì chưa có công việc cần làm: vui lòng nhập thông tin"); }
                            break;

                        }
                }
                Console.WriteLine("Nhập ký tự Y để thoát khỏi chương trình");
                string y = Console.ReadLine();
                if (y == "y"||y=="Y") break;
                Console.Clear();
            }


        Console.ReadKey();
        }
    }
}
