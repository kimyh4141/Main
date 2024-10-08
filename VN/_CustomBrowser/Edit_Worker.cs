using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using WiseM.Data;
using WiseM.Forms;
using System.IO;
using System.Data.SqlClient;

namespace WiseM.Browser
{
	public partial class Edit_Worker : SkinForm
	{
		private CustomPanelLinkEventArgs e = null;
		private string ImageFileFullPath1 = string.Empty;
		private string AttachFileFullPath1 = string.Empty;
		private string ImageChoice = string.Empty;
		public Edit_Worker(CustomPanelLinkEventArgs e)
		{
			InitializeComponent();
			this.e = e;

			InitializeField();
			if (this.e.DataGridView.CurrentRow != null)
			{
				if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Worker"].Value)))
				{
					this.Tb_Worker.Text = this.e.DataGridView.CurrentRow.Cells["Worker"].Value.ToString();
					this.TB_WorkerName.Text = this.e.DataGridView.CurrentRow.Cells["WorkerName"].Value.ToString();
					if (Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value))
					{
						this.CB_Status.Checked = true;
					}
					else
					{
						this.CB_Status.Checked = false;
					}
					
					this.TB_Date.Text = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value).ToString("yyyy-MM-dd HH:mm:ss");
					this.Combo_WorkTeam.Text = this.e.DataGridView.CurrentRow.Cells["WorkTeam"].Value.ToString();

					string SelectQuery = " Select Image , ImageName From Worker "
								   + " Where Worker = '" + this.e.DataGridView.CurrentRow.Cells["Worker"].Value.ToString() + "'  ";
					DataTable Selectdt = DbAccess.Default.GetDataTable(SelectQuery);
					if (Selectdt.Rows.Count == 0)
					{ }
					else
					{
						//이미지 파일 없을 경우 , 이미지 생성 안함.
						if (string.IsNullOrEmpty(Selectdt.Rows[0]["ImageName"].ToString()))
						{

						}
						else
						{
							ImageChoice = Selectdt.Rows[0]["ImageName"].ToString();
							this.ImageDatastread(Selectdt);
						}
					}
				}
			}
		}

		private void InitializeField()
		{
			this.Tb_Worker.Text = string.Empty;
			this.TB_WorkerName.Text = string.Empty;
			this.Combo_WorkTeam.Text = string.Empty;
			this.Combo_WorkTeam.Enabled = true;
			this.CB_Status.Checked = false;
			this.Tb_Worker.Enabled = true;
			
			this.TB_WorkerName.Enabled = true;

			Image1.BackgroundImage = null;
			ImageFileFullPath1 = string.Empty;
			AttachFileFullPath1 = string.Empty;
			ImageChoice = "Image Choice";
		
			ComboWorkTeam();
		}


		private void ComboWorkTeam()
		{
			DataTable dt = DbAccess.Default.GetDataTable("Select WorkTeam From WorkTeam  Where Status = 1 Order by ViewSeq");
			this.Combo_WorkTeam.Items.Clear();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				this.Combo_WorkTeam.Items.Add(dt.Rows[i]["WorkTeam"]);
			}
		}

		private void Btn_Search_Click_1(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.Tb_Worker.Text) || string.IsNullOrEmpty(this.Combo_WorkTeam.Text) || string.IsNullOrEmpty(this.TB_WorkerName.Text))
			{
				WiseM.MessageBox.Show("Check basic data.", "Warning", MessageBoxIcon.None);
				this.Btn_Insert.Enabled = false;
				return;
			}

			string CheckQuery = " Select * From Worker Where Worker = '" + this.Tb_Worker.Text + "' ";
			DataTable CheckQuerydt = DbAccess.Default.GetDataTable(CheckQuery);
			if (CheckQuerydt.Rows.Count == 0)
			{
				WiseM.MessageBox.Show("Do not have worker information.", "Warning", MessageBoxIcon.None);
				this.Btn_Insert.Enabled = false;
				return;
			}
			else
			{
				SearchQuery();
			}
		}

		private void SearchQuery()
		{
			string SearchQuery = string.Empty;
			SearchQuery = " Select Worker ,	Certi_Line ,Certi_Route ,Certi_Item,	 "
						+ "        Certi_Date ,	Certi_Level ,Certi_Score ,	Certi_Exp ,	Updated ,	Updater "
						+ " From WorkerLicense  "
						+ " Where Worker = '" + this.Tb_Worker.Text + "' And  "
						+ "       Certi_Line = '" + this.Combo_WorkTeam.Text + "' And  "
						+ "       Certi_Route = '" + this.TB_WorkerName.Text + "' ";
			DataTable dt = DbAccess.Default.GetDataTable(SearchQuery);

			
		}

		private void Btn_Insert_Click(object sender, EventArgs e)
		{
			//데이터 입력
			string InsertQuery = string.Empty;
			string InsertQueryHist = string.Empty;
			string UpdateQuery = string.Empty;

			string CheckQuery = " Select * From Worker Where Worker = '" + this.Tb_Worker.Text +"' ";
			DataTable Checkdt = DbAccess.Default.GetDataTable(CheckQuery);

			if (Checkdt.Rows.Count == 0)
			{
				InsertQuery = " Insert into Worker (Worker , Text , Status , Updated , Bunch  ) Values (";
				InsertQuery += " '" + this.Tb_Worker.Text + "', ";
				InsertQuery += " '" + this.TB_WorkerName.Text + "', ";
				if (this.CB_Status.Checked == true)
				{
					InsertQuery += "1,";
				}
				else
				{
					InsertQuery += "0,";
				}
				InsertQuery += " Getdate(), ";
				InsertQuery += " '" + this.Combo_WorkTeam.Text + "')";
				
				//데이터 조회 Refresh
				if (DbAccess.Default.ExecuteQuery(InsertQuery) > 0)
				{
					UpdateQuery = " Update WorkerLicense Set ";
					if (!string.IsNullOrEmpty(ImageFileFullPath1))
					{
						UpdateQuery += " Image = @Image , ";
						UpdateQuery += " ImageName = N'" + ImageChoice + "', ";
					}
					UpdateQuery += " Updated = Getdate() ";
					UpdateQuery += " Where Worker = '" + this.Tb_Worker.Text + "' ";

					if (InsertBinary(UpdateQuery) == -1)
					{	
						Image1.BackgroundImage = null;
						ImageFileFullPath1 = string.Empty;
						AttachFileFullPath1 = string.Empty;
						ImageChoice = "Image Choice";
						WiseM.MessageBox.Show("Process OK.", "OK", MessageBoxIcon.None);
						this.Close();
					}
					else
					{
						WiseM.MessageBox.Show("Image update fail.", "Warning", MessageBoxIcon.None);
					}
				}
				else
				{
					WiseM.MessageBox.Show("Process fail.", "Warning", MessageBoxIcon.None);
				}
			}
			else if (Checkdt.Rows.Count == 1)
			{
				UpdateQuery = " Update Worker Set ";
				UpdateQuery += " Text = '" + Convert.ToDateTime(this.TB_Date.Text).ToString("yyyy-MM-dd") + "' ,";
				if (this.CB_Status.Checked == true)
				{
					UpdateQuery += " Status = 1, ";
				}
				else
				{
					UpdateQuery += " Status = 0, ";
				}
				UpdateQuery += " Bunch = '" + this.Combo_WorkTeam.Text + "' ,	";
				if (!string.IsNullOrEmpty(ImageFileFullPath1))
				{
					UpdateQuery += " Image = @Image , ";
					UpdateQuery += " ImageName = N'" + ImageChoice + "', ";
				}
				UpdateQuery += " Updated = Getdate() ";
				UpdateQuery += " Where Worker = '" + this.Tb_Worker.Text + "'  ";
				

				if (InsertBinary(UpdateQuery) == -1)
				{	
					Image1.BackgroundImage = null;
					ImageFileFullPath1 = string.Empty;
					AttachFileFullPath1 = string.Empty;
					ImageChoice = "Image Choice";
					WiseM.MessageBox.Show("Process OK.", "OK", MessageBoxIcon.None);
					this.Close();
				}
				else
				{
					WiseM.MessageBox.Show("Process fail.", "Warning", MessageBoxIcon.None);
				}
			}
			else
			{
				WiseM.MessageBox.Show("Re-try search button.", "Warning", MessageBoxIcon.None);
			}

		}

		public int InsertBinary(string query)
		{
			int insertNum = -1;
			FileInfo fi;
			BinaryReader br;
			byte[] brs;
			int size;

			SqlConnection conn = new SqlConnection(DbAccess.Default.ConnectionString);
			try
			{


				//db연결
				conn.Open();
				SqlCommand cmd = new SqlCommand(query, conn);

				//실제파일
				if (!string.IsNullOrEmpty(ImageFileFullPath1))
				{
					fi = new FileInfo(ImageFileFullPath1);
					br = new BinaryReader(fi.OpenRead(), Encoding.GetEncoding("euc-kr"));

					brs = new byte[br.BaseStream.Length];
					br.Read(brs, 0, Convert.ToInt32(br.BaseStream.Length));
					size = Convert.ToInt32(br.BaseStream.Length);

					cmd.Parameters.Add("@Image", SqlDbType.VarBinary, size).Value = brs;

					brs = null;
					br.Close();
					fi = null;

				}

				cmd.ExecuteNonQuery();
				conn.Close();
			}
			catch (Exception e)
			{
				insertNum = 0;
				MessageBox.ShowCaption(e.Message, "", MessageBoxIcon.Warning, null);
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
				GC.Collect();
			}
			return insertNum;
		}
		private void ImageDatastread(DataTable dtImage)
		{
			byte[] bData = new byte[0];
			bData = (byte[])dtImage.Rows[0][0];

			//string filename = Convert.ToDateTime(reader[1].ToString()).ToString("yyyyMMddHHmmss") + ".jpg";
			string temp = dtImage.Rows[0][1].ToString().Substring(dtImage.Rows[0][1].ToString().LastIndexOf(@".") + 1, dtImage.Rows[0][1].ToString().Length - dtImage.Rows[0][1].ToString().LastIndexOf(@".") - 1).ToLower();

			switch (temp)
			{
				case "bmp":
				case "jpg":
				case "gif":
				case "png":
					Image1.BackgroundImage = ConvertByteArrayToImage(bData);
					break;
				default:
					break;
			}
		}

		public Image ConvertByteArrayToImage(byte[] pByte)
		{
			MemoryStream memoryStream = new MemoryStream();
			Image convertImage = null;

			try
			{
				memoryStream.Position = 0;
				memoryStream.Write(pByte, 0, (Int32)pByte.Length);
				convertImage = Image.FromStream(memoryStream);
			}
			catch (System.Exception ex)
			{
				throw ex;
			}

			return convertImage;
		}
	
		private void button1_Click(object sender, EventArgs e)
		{
			InitializeField();
		}

		private void Image1_Click(object sender, EventArgs e)
		{
			try
			{
				FileDialog fileDlg = new OpenFileDialog();
				fileDlg.Filter = "All files (*.*)|*.*|BMP(*.bmp)|*.bmp|(*.jpg)|*.jpg|(*.gif)|*.gif|(*.png)|*.png";
				fileDlg.InitialDirectory = @"C:\";

				if (fileDlg.ShowDialog() == DialogResult.OK)
				{
					string temp = Path.GetExtension(fileDlg.FileName.ToLower());
					string fileName = Path.GetFileName(fileDlg.FileName.ToLower());

					switch (temp)
					{
						case ".bmp":
						case ".jpg":
						case ".gif":
						case ".png":
							{
								Image image = System.Drawing.Image.FromFile(fileDlg.FileName);
								byte[] imageData = ConvertImageToByteArray(image);
								ImageFileFullPath1 = Path.GetFullPath(fileDlg.FileName.ToLower());
								ImageChoice = fileName;
								Image1.BackgroundImage = System.Drawing.Image.FromFile(fileDlg.FileName);
							}
							break;
						default:
							{
								MessageBox.Show("Unformatted file", "Warning", MessageBoxIcon.Warning);
								ImageChoice = "Image Choice";
								return;
							}
							break;
					}

				}
				else
				{
					ImageChoice = "Image Choice";
				}
			}
			catch (Exception ex)
			{
			}
		}
		public byte[] ConvertImageToByteArray(Image theImage)
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] convertByte;

			theImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
			convertByte = new byte[memoryStream.Length];
			memoryStream.Position = 0;
			memoryStream.Read(convertByte, 0, (int)memoryStream.Length);
			memoryStream.Close();

			return convertByte;
		}

	
	}
}
