using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTH3
{
    class XuLy : DataTable
    {
        #region Bien cuc bo
        public static String Chuoi_lien_ket;
        private SqlDataAdapter mBo_doc_ghi = new SqlDataAdapter();
        private SqlConnection mKet_noi;
        private String mChuoi_SQL;
        public string chuoi_csdl;
        private String mTen_bang; 
        #endregion

        #region Cac thuoc tinh
        public String Chuoi_SQL
        {
            get { return mChuoi_SQL; }
            set { mChuoi_SQL = value; }
        }
        public String Ten_bang
        {
            get { return mTen_bang; }
            set { mTen_bang = value; }
        }
        public int So_dong
        {
            get { return this.DefaultView.Count; }
           
        }
        #endregion

        #region Cac phuong thuc khoi tao
        public XuLy():base(){}
        public XuLy(String pTenBang)
        {
            mTen_bang=pTenBang;
            Doc_bang();
        }
        #endregion
        private void Doc_bang()
        {
            
        }
        
      
    }

}
