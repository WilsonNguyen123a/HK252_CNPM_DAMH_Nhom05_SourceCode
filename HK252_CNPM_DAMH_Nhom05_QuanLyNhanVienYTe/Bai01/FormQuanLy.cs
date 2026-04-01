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
    public partial class FormQuanLy : Form
    {
        List<NhanVien> danhSachNV;
        List<CaTruc> danhSachCa = new List<CaTruc>();

        public FormQuanLy(List<NhanVien> ds)
        {
            InitializeComponent();
            danhSachNV = ds;
        }
        private void HienThiListView()
        {
            listViewCaTruc.Items.Clear();

            foreach (var c in danhSachCa)
            {
                ListViewItem item = new ListViewItem(c.MaNV);
                item.SubItems.Add(c.TenNV);
                item.SubItems.Add(c.NgayTruc);
                item.SubItems.Add(c.Ca);

                listViewCaTruc.Items.Add(item);
            }
        }


        private void FormQuanLy_Load(object sender, EventArgs e)
        {
            cbNhanVien.Items.Clear();

            foreach (var nv in danhSachNV)
            {
                cbNhanVien.Items.Add(nv.MaNV + " - " + nv.TenNV);
            }

            // Load ca
            cbCa.Items.Add("Sáng");
            cbCa.Items.Add("Chiều");
            cbCa.Items.Add("Tối");

            cbCa.SelectedIndex = 0;

            // Load JSON
            danhSachCa = JsonHelper.LoadCaTruc();

            HienThiListView();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbNhanVien.Text == "")
            {
                MessageBox.Show("Chọn nhân viên!");
                return;
            }

            string[] parts = cbNhanVien.Text.Split('-');
            string maNV = parts[0].Trim();
            string tenNV = parts[1].Trim();

            // check trùng
            foreach (var c in danhSachCa)
            {
                if (c.MaNV == maNV &&
                    c.NgayTruc == dtpNgayTruc.Text &&
                    c.Ca == cbCa.Text)
                {
                    MessageBox.Show("Trùng ca!");
                    return;
                }
            }

            CaTruc ca = new CaTruc()
            {
                MaNV = maNV,
                TenNV = tenNV,
                NgayTruc = dtpNgayTruc.Text,
                Ca = cbCa.Text
            };

            danhSachCa.Add(ca);
            JsonHelper.SaveCaTruc(danhSachCa);

            HienThiListView();
        }

        private void listViewCaTruc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewCaTruc.SelectedItems.Count > 0)
            {
                var item = listViewCaTruc.SelectedItems[0];

                cbNhanVien.Text = item.SubItems[0].Text + " - " + item.SubItems[1].Text;
                dtpNgayTruc.Text = item.SubItems[2].Text;
                cbCa.Text = item.SubItems[3].Text;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (listViewCaTruc.SelectedItems.Count == 0)
            {
                MessageBox.Show("Chọn dòng để sửa!");
                return;
            }

            int index = listViewCaTruc.SelectedItems[0].Index;

            string[] parts = cbNhanVien.Text.Split('-');

            danhSachCa[index].MaNV = parts[0].Trim();
            danhSachCa[index].TenNV = parts[1].Trim();
            danhSachCa[index].NgayTruc = dtpNgayTruc.Text;
            danhSachCa[index].Ca = cbCa.Text;

            JsonHelper.SaveCaTruc(danhSachCa);

            HienThiListView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (listViewCaTruc.SelectedItems.Count == 0)
            {
                MessageBox.Show("Chọn dòng để sửa!");
                return;
            }

            int index = listViewCaTruc.SelectedItems[0].Index;

            string[] parts = cbNhanVien.Text.Split('-');

            danhSachCa[index].MaNV = parts[0].Trim();
            danhSachCa[index].TenNV = parts[1].Trim();
            danhSachCa[index].NgayTruc = dtpNgayTruc.Text;
            danhSachCa[index].Ca = cbCa.Text;

            JsonHelper.SaveCaTruc(danhSachCa);

            HienThiListView();
        }
    }
}
