using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using Resources;
using WebStore.Business;

namespace WebStore.UI
{
	public partial class AdminImageGridView : UserControl
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(AdminImageGridView));

		public Guid ReferenceGuid { get; set; } = Guid.Empty;
		public WebStoreConfiguration Config { get; set; }
		public bool Enabled { get; set; } = true;

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			//if (Page is mojoBasePage basePage)
			//{
			//	basePage.ScriptConfig.IncludeColorBox = true;
			//}
			// this needs to be before the grid is bound (bindImagesGrid()) so the headers are labeled
			// before content is added to the grid. The header labels will not show up if they are labeled 
			// after the grid is bound.
			grdImages.Columns[0].HeaderText = WebStoreResources.ImageLabel;
			grdImages.Columns[1].HeaderText = WebStoreResources.DisplayOrderLabel;
			grdImages.Columns[2].HeaderText = WebStoreResources.AltLabel;
			grdImages.Columns[3].HeaderText = WebStoreResources.TitleLabel;

			if (!Page.IsPostBack && Enabled)
			{
				bindImagesGrid();
			}

			btnAddImage.Text = WebStoreResources.AddImageLabel;

			btnAddImage.Enabled = Enabled;
		}

		#region Images

		private void bindImagesGrid()
		{
			if (ReferenceGuid != Guid.Empty)
			{
				var images = StoreImage.GetByReference(ReferenceGuid);
				grdImages.DataSource = images;
				grdImages.DataBind();
			}
		}

		private void btnAddImage_Click(object sender, EventArgs e)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Guid", typeof(Guid));
			dataTable.Columns.Add("StoreGuid", typeof(Guid));
			dataTable.Columns.Add("ReferenceGuid", typeof(Guid));
			dataTable.Columns.Add("ImageUrl", typeof(string));
			dataTable.Columns.Add("DisplayOrder", typeof(int));
			dataTable.Columns.Add("Alt", typeof(string));
			dataTable.Columns.Add("Title", typeof(string));

			DataRow row = dataTable.NewRow();
			row["Guid"] = Guid.Empty;
			row["StoreGuid"] = CacheHelper.GetCurrentSiteSettings().SiteGuid;
			row["ReferenceGuid"] = ReferenceGuid;
			row["ImageUrl"] = string.Empty;
			row["DisplayOrder"] = 0;
			row["Alt"] = string.Empty;
			row["Title"] = string.Empty;
			dataTable.Rows.Add(row);

			grdImages.EditIndex = 0;
			grdImages.DataSource = dataTable.DefaultView;
			grdImages.DataBind();

			btnAddImage.Visible = false;

			upImages.Update();
		}

		private void grdImages_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			GridView grid = (GridView)sender;
			Guid guid = (Guid)grid.DataKeys[e.RowIndex].Value;
			TextBox txtImageUrl = (TextBox)grid.Rows[e.RowIndex].Cells[0].FindControl("txtImageUrl");
			TextBox txtDisplayOrder = (TextBox)grid.Rows[e.RowIndex].Cells[0].FindControl("txtDisplayOrder");
			TextBox txtAlt = (TextBox)grid.Rows[e.RowIndex].Cells[0].FindControl("txtAlt");
			TextBox txtTitle = (TextBox)grid.Rows[e.RowIndex].Cells[0].FindControl("txtTitle");

			StoreImage image;

			if (guid != Guid.Empty)
			{
				image = new StoreImage(guid);
			}
			else
			{

				image = new StoreImage
				{
					Guid = Guid.NewGuid(),
					StoreGuid = CacheHelper.GetCurrentSiteSettings().SiteGuid,
					ReferenceGuid = ReferenceGuid
				};

			}

			image.ImageUrl = txtImageUrl.Text;
			image.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);
			image.Alt = txtAlt.Text;
			image.Title = txtTitle.Text;

			image.Save();

			grdImages.EditIndex = -1;
			btnAddImage.Visible = true;
			bindImagesGrid();
			upImages.Update();

		}

		private void grdImages_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			grdImages.EditIndex = -1;
			btnAddImage.Visible = true;
			bindImagesGrid();
			upImages.Update();
		}

		private void grdImages_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridView grid = (GridView)sender;
			Guid guid = (Guid)grid.DataKeys[e.RowIndex].Value;

			StoreImage.Delete(guid);

			grdImages.EditIndex = -1;
			btnAddImage.Visible = true;
			bindImagesGrid();
			upImages.Update();
		}

		private void grdImages_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			//GridView grid = (GridView)sender;
			//Guid guid = (Guid)grid.DataKeys[e.NewEditIndex].Value;

			//Guid guid = (Guid)grid.DataKeys[e.Row.RowIndex].Value;
			if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
			{
				//ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "testscript1", $"alert('ScriptManager.RegisterClientScriptBlock({this.GetType().ToString()})');", true);
				//ScriptManager.RegisterStartupScript(this, this.GetType(), "testscript2", $"alert('ScriptManager.RegisterStartupScript({this.GetType().ToString()})');", true);
				//ScriptManager.RegisterClientScriptBlock(e.Row, e.Row.GetType(), "testscript5", "alert('ScriptManager.RegisterClientScriptBlock(Row)');", true);
				//ScriptManager.RegisterStartupScript(e.Row, e.Row.GetType(), "testscript6", "alert('ScriptManager.RegisterStartupScript(Row)');", true);
			
				Button btnDelete = (Button)e.Row.Cells[4].FindControl("btnDelete");
				if (btnDelete != null)
				{
					btnDelete.Attributes.Add("OnClick", $"return confirm('{WebStoreResources.DeleteImageWarning}');");
				}
				Panel divInputGroup = (Panel)e.Row.Cells[0].FindControl("divInputGroup");
				//TextBox txtImageUrl = (TextBox)divInputGroup.FindControl("txtImageUrl");
			
				Literal literal = new Literal();
				//Image imgPreview = (Image)e.Row.Cells[0].FindControl("imgPreview");
			
				string fileManagerUrl = string.Empty;
				string startFolder = string.Empty;

				if (!string.IsNullOrWhiteSpace(Config.ImageStartPath))
				{
					startFolder = "&startFolder=" + Config.ImageStartPath;
				}

				fileManagerUrl = $@"{SiteUtils.GetNavigationSiteRoot()}{WebConfigSettings.FileDialogRelativeUrl}?
					editor=filepicker
					&type=image
					&inputId=txtImageUrl
					{startFolder}
					&returnFullPath=true";
				
				literal.Text = string.Format(displaySettings.ModalLinkMarkup, "#filePickerModal", displaySettings.ImagePickerModalLinkCssClass, displaySettings.ImagePickerModalLinkText);
				literal.Text += string.Format(displaySettings.ModalMarkup, "filePickerModal", WebStoreResources.ImagePickerHeading, fileManagerUrl.Replace("\r\n", string.Empty).Replace("\t", string.Empty));
				if (divInputGroup != null)
				{
					divInputGroup.Controls.Add(literal);
				}
				else
				{
					e.Row.Cells[0].Controls.Add(literal);
				}
				string filePicker =
					$@"<script>
						var filePicker = {{
							set: function(url, clientId) {{
								var _inputImg = document.getElementById(clientId); _inputImg.value = url;
								var _inputPrev = document.getElementById('imgPreview'); _inputPrev.src = url;
							}},

							close: function() {{
								$('#filePickerModal').modal('toggle')
							}}
						}};
					</script>";

				ScriptManager.RegisterClientScriptBlock(
					e.Row,
					e.Row.GetType(),
					"SetUrl",
					filePicker,
					false
				);

				string filePickerPreview =
					$@"<script>
						(function() {{
							var prevInput = document.getElementById('txtImageUrl');
							var prevImage = document.getElementById('imgPreview');

							if (prevInput !== null) {{
								prevInput.addEventListener('blur', function() {{
									if (prevInput.value.length > 0) {{
										prevImage.src = prevInput.value;
									}} else {{
										prevImage.src = '#';
									}}
								}});
							}}
						}})();
					</script>";

				ScriptManager.RegisterStartupScript(
					e.Row, 
					e.Row.GetType(),
					"FilePickerPreview",
					filePickerPreview,
					false
				);
			}

		}

		private void grdImages_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView grid = (GridView)sender;
			grid.EditIndex = e.NewEditIndex;
			bindImagesGrid();
			Guid guid = (Guid)grid.DataKeys[e.NewEditIndex].Value;

			btnAddImage.Visible = false;
			upImages.Update();
		}
		private void grdImages_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			//throw new NotImplementedException();
		}
		#endregion

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			grdImages.RowCommand += new GridViewCommandEventHandler(grdImages_RowCommand);
			grdImages.RowEditing += new GridViewEditEventHandler(grdImages_RowEditing);
			grdImages.RowUpdating += new GridViewUpdateEventHandler(grdImages_RowUpdating);
			grdImages.RowCancelingEdit += new GridViewCancelEditEventHandler(grdImages_RowCancelingEdit);
			grdImages.RowDeleting += new GridViewDeleteEventHandler(grdImages_RowDeleting);
			grdImages.RowDataBound += new GridViewRowEventHandler(grdImages_RowDataBound);
			btnAddImage.Click += new EventHandler(btnAddImage_Click);
			
		}
	}
}