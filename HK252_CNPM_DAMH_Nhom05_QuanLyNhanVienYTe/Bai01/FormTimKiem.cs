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
    public partial class FormTimKiem : Form
    {
        ListView listViewMain;
        public FormTimKiem(ListView lv)
        {
            InitializeComponent();
            listViewMain = lv;
        }


        private void FormTimKiem_Load(object sender, EventArgs e)
        {
            cbTieuChi.Items.Add("Mã NV");
            cbTieuChi.Items.Add("Tên NV");

            cbTieuChi.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = txtNhap.Text.ToLower();
            string tieuChi = cbTieuChi.Text;
            bool timThay = false;

            foreach (ListViewItem item in listViewMain.Items)
            {
                bool match = false;

                if (tieuChi == "Mã NV")
                {
                    if (item.SubItems[0].Text.ToLower() == keyword)
                        match = true;
                }
                else if (tieuChi == "Tên NV")
                {
                    if (item.SubItems[1].Text.ToLower().Contains(keyword))
                        match = true;
                }

                // Highlight
                if (match)
                {
                    item.BackColor = Color.LightGreen;
                    timThay = true; 
                }
                else
                {
                    item.BackColor = Color.White;
                }
            }
            if (!timThay)
            {
                MessageBox.Show("Không tìm thấy kết quả!", "Thông báo");
                txtNhap.Focus();
                txtNhap.SelectAll();
            }
            else
            {
                MessageBox.Show("Đã tìm thấy!", "Thông báo");
                this.Close(); // chỉ đóng khi có kết quả
            }


        }

        private void txtNhap_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
