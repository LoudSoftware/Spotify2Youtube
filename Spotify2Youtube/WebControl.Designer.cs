using Spotify2Youtube.Helpers;

namespace Spotify2Youtube
{
	partial class WebControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Authentication Authentication
		{
			get { return _authentication; }
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnAuth = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.savedTracksListView = new System.Windows.Forms.ListView();
			this.ListTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ListArtist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ListAlbum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ListURI = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ListYoutubeResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.savedTracksCountLabel = new System.Windows.Forms.Label();
			this.BtnYoutubeSearch = new System.Windows.Forms.Button();
			this.DownloadAllBtn = new System.Windows.Forms.Button();
			this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
			this.DownloadingLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnAuth
			// 
			this.btnAuth.Location = new System.Drawing.Point(18, 680);
			this.btnAuth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnAuth.Name = "btnAuth";
			this.btnAuth.Size = new System.Drawing.Size(390, 35);
			this.btnAuth.TabIndex = 0;
			this.btnAuth.Text = "Authenticate Spotify";
			this.btnAuth.UseVisualStyleBackColor = true;
			this.btnAuth.Click += new System.EventHandler(this.BtnAuth_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label1.Location = new System.Drawing.Point(17, 10);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(137, 25);
			this.label1.TabIndex = 1;
			this.label1.Text = "Saved tracks: ";
			// 
			// savedTracksListView
			// 
			this.savedTracksListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListTitle,
            this.ListArtist,
            this.ListAlbum,
            this.ListURI,
            this.ListYoutubeResult});
			this.savedTracksListView.Cursor = System.Windows.Forms.Cursors.Default;
			this.savedTracksListView.FullRowSelect = true;
			this.savedTracksListView.Location = new System.Drawing.Point(22, 38);
			this.savedTracksListView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.savedTracksListView.Name = "savedTracksListView";
			this.savedTracksListView.Size = new System.Drawing.Size(1164, 547);
			this.savedTracksListView.TabIndex = 2;
			this.savedTracksListView.UseCompatibleStateImageBehavior = false;
			this.savedTracksListView.View = System.Windows.Forms.View.Details;
			// 
			// ListTitle
			// 
			this.ListTitle.Text = "Title";
			this.ListTitle.Width = 148;
			// 
			// ListArtist
			// 
			this.ListArtist.Text = "Artist";
			this.ListArtist.Width = 133;
			// 
			// ListAlbum
			// 
			this.ListAlbum.Text = "Album";
			this.ListAlbum.Width = 154;
			// 
			// ListURI
			// 
			this.ListURI.Text = "Spotify URI";
			this.ListURI.Width = 171;
			// 
			// ListYoutubeResult
			// 
			this.ListYoutubeResult.Text = "Youtube Result";
			this.ListYoutubeResult.Width = 147;
			// 
			// savedTracksCountLabel
			// 
			this.savedTracksCountLabel.AutoSize = true;
			this.savedTracksCountLabel.Location = new System.Drawing.Point(176, 14);
			this.savedTracksCountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.savedTracksCountLabel.Name = "savedTracksCountLabel";
			this.savedTracksCountLabel.Size = new System.Drawing.Size(19, 20);
			this.savedTracksCountLabel.TabIndex = 3;
			this.savedTracksCountLabel.Text = "--";
			// 
			// BtnYoutubeSearch
			// 
			this.BtnYoutubeSearch.Location = new System.Drawing.Point(876, 680);
			this.BtnYoutubeSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.BtnYoutubeSearch.Name = "BtnYoutubeSearch";
			this.BtnYoutubeSearch.Size = new System.Drawing.Size(154, 35);
			this.BtnYoutubeSearch.TabIndex = 4;
			this.BtnYoutubeSearch.Text = "Search Youtube";
			this.BtnYoutubeSearch.UseVisualStyleBackColor = true;
			this.BtnYoutubeSearch.Click += new System.EventHandler(this.BtnYoutubeSearch_Click);
			// 
			// DownloadAllBtn
			// 
			this.DownloadAllBtn.Location = new System.Drawing.Point(1040, 680);
			this.DownloadAllBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DownloadAllBtn.Name = "DownloadAllBtn";
			this.DownloadAllBtn.Size = new System.Drawing.Size(148, 35);
			this.DownloadAllBtn.TabIndex = 5;
			this.DownloadAllBtn.Text = "Download All";
			this.DownloadAllBtn.UseVisualStyleBackColor = true;
			this.DownloadAllBtn.Click += new System.EventHandler(this.DownloadAllBtn_Click);
			// 
			// DownloadProgressBar
			// 
			this.DownloadProgressBar.Location = new System.Drawing.Point(22, 638);
			this.DownloadProgressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DownloadProgressBar.Name = "DownloadProgressBar";
			this.DownloadProgressBar.Size = new System.Drawing.Size(1166, 32);
			this.DownloadProgressBar.TabIndex = 6;
			// 
			// DownloadingLabel
			// 
			this.DownloadingLabel.AutoSize = true;
			this.DownloadingLabel.Location = new System.Drawing.Point(18, 614);
			this.DownloadingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.DownloadingLabel.Name = "DownloadingLabel";
			this.DownloadingLabel.Size = new System.Drawing.Size(105, 20);
			this.DownloadingLabel.TabIndex = 7;
			this.DownloadingLabel.Text = "Downloading:";
			// 
			// WebControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1206, 734);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.DownloadingLabel);
			this.Controls.Add(this.DownloadProgressBar);
			this.Controls.Add(this.DownloadAllBtn);
			this.Controls.Add(this.BtnYoutubeSearch);
			this.Controls.Add(this.savedTracksCountLabel);
			this.Controls.Add(this.savedTracksListView);
			this.Controls.Add(this.btnAuth);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "WebControl";
			this.Text = "Spotify2Youtube";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnAuth;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView savedTracksListView;
		private System.Windows.Forms.ColumnHeader ListTitle;
		private System.Windows.Forms.ColumnHeader ListArtist;
		private System.Windows.Forms.ColumnHeader ListAlbum;
		private System.Windows.Forms.ColumnHeader ListURI;
		private System.Windows.Forms.ColumnHeader ListYoutubeResult;
		private System.Windows.Forms.Label savedTracksCountLabel;
		private System.Windows.Forms.Button BtnYoutubeSearch;
		private System.Windows.Forms.Button DownloadAllBtn;
		private System.Windows.Forms.ProgressBar DownloadProgressBar;
		private System.Windows.Forms.Label DownloadingLabel;
		private readonly Authentication _authentication;
	}
}

