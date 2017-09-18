using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTH3
{
    class XuLy : DataTable
    {
        #region bien cuc bo
        public static String Chuoi_lien_ket;
        private SqlDataAdapter mBo_doc_ghi = new SqlDataAdapter();
        private SqlConnection mKet_noi;
        private string chuoi_csdl;   
        private String mChuoi_SQL;     
        private String mTen_bang;
        #endregion

        #region Cac thuoc tinh
        public string Chuoi_csdl
        {
            get { return chuoi_csdl; }
            set { chuoi_csdl = value; }
        }
        public String MChuoi_SQL
        {
            get { return mChuoi_SQL; }
            set { mChuoi_SQL = value; }
        }
        public int So_dong
        {
            get { return this.DefaultView.Count; }
        }
        #endregion
        #region Cac phuong thuc khoi tao
        public XuLy() : base() { }
        // tao moi mot bảng với pTEn_bang
        public XuLy(String pTen_bang)
        {
            mTen_bang = pTen_bang;
            Doc_bang();
        }
        // tạo mới bảng với câu truy vấn
        public XuLy(String pTen_bang, String pChuoi_SQL)
        {
            mTen_bang = pTen_bang;
            mChuoi_SQL = pChuoi_SQL;
            Doc_bang();
        }
        #endregion
        #region

        public void Doc_bang()
        {
            if (mChuoi_SQL == null)
                mChuoi_SQL = "SELECT * FROM " + mTen_bang;
            if (mKet_noi == null)
                mKet_noi = new SqlConnection(Chuoi_lien_ket);
            try
            {
                mBo_doc_ghi = new SqlDataAdapter(mChuoi_SQL, mKet_noi);
                mBo_doc_ghi.FillSchema(this, SchemaType.Mapped);
                mBo_doc_ghi.Fill(this);
                mBo_doc_ghi.RowUpdated += new SqlRowUpdatedEventHandler(mBo_doc_ghi_RowUpdated);
                SqlCommandBuilder Bo_phat_sinh = new SqlCommandBuilder(mBo_doc_ghi);
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public Boolean Ghi()
        {
            Boolean ket_qua = true;
            try
            {
                this.AcceptChanges();
            }
            catch (SqlException )
            {
                this.RejectChanges();
                ket_qua = false;
            }
            return ket_qua;
        }
        // lọc dữ liệu
        public void Loc_du_lieu(String pDieu_kien)
        {
            try
            {
                this.DefaultView.RowFilter = pDieu_kien;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public int Thuc_hien_lenh(String Lenh)
        {
            try
            {
                SqlCommand Cau_lenh = new SqlCommand(Lenh, mKet_noi);
                mKet_noi.Open();
                int ket_qua = Cau_lenh.ExecuteNonQuery();
                mKet_noi.Close();
                return ket_qua;
            }
            catch
            {
                return -1;
            }
        }
        public Object Thuc_hien_lenh_tinh_toan(String Lenh)
        {
            try
            {
                SqlCommand Cau_lenh = new SqlCommand(Lenh, mKet_noi);
                mKet_noi.Open();
                Object ket_qua = Cau_lenh.ExecuteScalar();
                mKet_noi.Close();
                return ket_qua;
            }
            catch
            {
                return null;
            }
            
        }
        #endregion
        #region xu lu su kien

        private void mBo_doc_ghi_RowUpdated(Object sender,SqlRowUpdatedEventArgs e)
        {
            if(this.PrimaryKey[0].AutoIncrement)
            {
                if((e.Status==UpdateStatus.Continue)&&(e.StatementType==StatementType.Insert))
                {
                    SqlCommand cmd = new SqlCommand("Select @@IDENTITY ", mKet_noi);
                    e.Row.ItemArray[0] = cmd.ExecuteScalar();
                    e.Row.AcceptChanges();
                }
            }
        }
        #endregion
    }
}
