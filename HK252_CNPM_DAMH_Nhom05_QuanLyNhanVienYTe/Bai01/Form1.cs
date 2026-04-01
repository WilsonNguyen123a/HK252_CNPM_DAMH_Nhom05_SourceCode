using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanVienYTe
{
    public partial class Form1 : Form
    {
        List<NhanVien> danhSach = new List<NhanVien>();
        public Form1()
        {
            InitializeComponent();
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            // Check rỗng
            if (txtMaNV.Text == "" || txtTenNV.Text == "" || txtLuong.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (!txtMaNV.Text.All(char.IsDigit))
            {
                MessageBox.Show("Mã nhân viên phải là số!");
                return;
            }
            // tên không chứa số
            if (txtTenNV.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Tên nhân viên không được chứa số!");
                return;
            }

            // lương phải là số
            if (!double.TryParse(txtLuong.Text, out _))
            {
                MessageBox.Show("Hệ số lương phải là số!");
                return;
            }

            foreach (ListViewItem i in listView1.Items)
            {
                if (i.Text == txtMaNV.Text)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại!");
                    return;
                }
            }

            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";

            NhanVien nv = new NhanVien()
            {
                MaNV = txtMaNV.Text,
                TenNV = txtTenNV.Text,
                GioiTinh = gioiTinh,
                NgaySinh = dtpNgaySinh.Text,
                HeSoLuong = txtLuong.Text,
                ChucVu = cbChucVu.Text,
                PhongBan = cbPhongBan.Text
            };

            danhSach.Add(nv);
            JsonHelper.Save(danhSach);

            ListViewItem item = new ListViewItem(nv.MaNV);
            item.SubItems.Add(nv.TenNV);
            item.SubItems.Add(nv.GioiTinh);
            item.SubItems.Add(nv.NgaySinh);
            item.SubItems.Add(nv.HeSoLuong);
            item.SubItems.Add(nv.ChucVu);
            item.SubItems.Add(nv.PhongBan);

            listView1.Items.Add(item);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;

                listView1.Items.RemoveAt(index);
                danhSach.RemoveAt(index);

                JsonHelper.Save(danhSach);
            }
            else
            {
                MessageBox.Show("Chọn dòng để xóa!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];

                // update ListView
                item.SubItems[1].Text = txtTenNV.Text;
                item.SubItems[2].Text = radNam.Checked ? "Nam" : "Nữ";
                item.SubItems[3].Text = dtpNgaySinh.Text;
                item.SubItems[4].Text = txtLuong.Text;
                item.SubItems[5].Text = cbChucVu.Text;
                item.SubItems[6].Text = cbPhongBan.Text;

                // 🔥 update JSON
                int index = item.Index;

                danhSach[index].TenNV = txtTenNV.Text;
                danhSach[index].GioiTinh = radNam.Checked ? "Nam" : "Nữ";
                danhSach[index].NgaySinh = dtpNgaySinh.Text;
                danhSach[index].HeSoLuong = txtLuong.Text;
                danhSach[index].ChucVu = cbChucVu.Text;
                danhSach[index].PhongBan = cbPhongBan.Text;

                JsonHelper.Save(danhSach);
            }
            else
            {
                MessageBox.Show("Chọn dòng để sửa!");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            FormTimKiem f = new FormTimKiem(listView1);
            f.ShowDialog();
        }

        private void btnQl_Click(object sender, EventArgs e)
        {
            FormQuanLy f = new FormQuanLy(danhSach);
            f.ShowDialog();
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            danhSach = JsonHelper.Load();

            listView1.Items.Clear();

            foreach (var nv in danhSach)
            {
                ListViewItem item = new ListViewItem(nv.MaNV);

                item.SubItems.Add(nv.TenNV);
                item.SubItems.Add(nv.GioiTinh);
                item.SubItems.Add(nv.NgaySinh);
                item.SubItems.Add(nv.HeSoLuong);
                item.SubItems.Add(nv.ChucVu);
                item.SubItems.Add(nv.PhongBan);

                listView1.Items.Add(item);
                listView1.View = View.Details;
                listView1.FullRowSelect = true;
                listView1.GridLines = true;
            }

            // Format ngày
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";

            cbPhongBan.Items.Add("Nội Khoa");
            cbPhongBan.Items.Add("Ngoại Khoa");
            cbPhongBan.Items.Add("ICU");
            cbChucVu.Items.Add("Bác Sĩ");
            cbChucVu.Items.Add("Y Tá");
            cbChucVu.Items.Add("Điều Dưỡng");
            cbChucVu.SelectedIndex = 0;

            cbPhongBan.SelectedIndex = 0;
        }
        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];

                txtMaNV.Text = item.SubItems[0].Text;
                txtTenNV.Text = item.SubItems[1].Text;

                if (item.SubItems[2].Text == "Nam")
                    radNam.Checked = true;
                else
                    radNu.Checked = true;

                dtpNgaySinh.Value = DateTime.ParseExact(
                item.SubItems[3].Text,
                "dd/MM/yyyy",
                null);
                txtLuong.Text = item.SubItems[4].Text;
                cbPhongBan.Text = item.SubItems[5].Text;
            }
        }

        private void cbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbChucVu.Text == "Bác Sĩ")
            //{
            //    cbPhongBan.Enabled = true;
            //}
            //else
            //{
            //    cbPhongBan.Enabled = false;
            //    cbPhongBan.SelectedIndex = -1; // reset
            //}

        }
    }
}
