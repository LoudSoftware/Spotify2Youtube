namespace SpotifyList
{
    partial class WebControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
			this.btnAuth.Location = new System.Drawing.Point(12, 442);
			this.btnAuth.Name = "btnAuth";
			this.btnAuth.Size = new System.Drawing.Size(260, 23);
			this.btnAuth.TabIndex = 0;
			this.btnAuth.Text = "Authenticate Spotify";
			this.btnAuth.UseVisualStyleBackColor = true;
			this.btnAuth.Click += new System.EventHandler(this.BtnAuth_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 17);
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
			this.savedTracksListView.Location = new System.Drawing.Point(15, 25);
			this.savedTracksListView.Name = "savedTracksListView";
			this.savedTracksListView.Size = new System.Drawing.Size(777, 357);
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
			this.ListYoutubeResult.Width = 154;
			// 
			// savedTracksCountLabel
			// 
			this.savedTracksCountLabel.AutoSize = true;
			this.savedTracksCountLabel.Location = new System.Drawing.Point(117, 9);
			this.savedTracksCountLabel.Name = "savedTracksCountLabel";
			this.savedTracksCountLabel.Size = new System.Drawing.Size(13, 13);
			this.savedTracksCountLabel.TabIndex = 3;
			this.savedTracksCountLabel.Text = "--";
			// 
			// BtnYoutubeSearch
			// 
			this.BtnYoutubeSearch.Location = new System.Drawing.Point(584, 442);
			this.BtnYoutubeSearch.Name = "BtnYoutubeSearch";
			this.BtnYoutubeSearch.Size = new System.Drawing.Size(103, 23);
			this.BtnYoutubeSearch.TabIndex = 4;
			this.BtnYoutubeSearch.Text = "Search Youtube";
			this.BtnYoutubeSearch.UseVisualStyleBackColor = true;
			this.BtnYoutubeSearch.Click += new System.EventHandler(this.BtnYoutubeSearch_Click);
			// 
			// DownloadAllBtn
			// 
			this.DownloadAllBtn.Location = new System.Drawing.Point(693, 442);
			this.DownloadAllBtn.Name = "DownloadAllBtn";
			this.DownloadAllBtn.Size = new System.Drawing.Size(99, 23);
			this.DownloadAllBtn.TabIndex = 5;
			this.DownloadAllBtn.Text = "Download All";
			this.DownloadAllBtn.UseVisualStyleBackColor = true;
			this.DownloadAllBtn.Click += new System.EventHandler(this.DownloadAllBtn_Click);
			// 
			// DownloadProgressBar
			// 
			this.DownloadProgressBar.Location = new System.Drawing.Point(15, 415);
			this.DownloadProgressBar.Name = "DownloadProgressBar";
			this.DownloadProgressBar.Size = new System.Drawing.Size(777, 21);
			this.DownloadProgressBar.TabIndex = 6;
			// 
			// DownloadingLabel
			// 
			this.DownloadingLabel.AutoSize = true;
			this.DownloadingLabel.Location = new System.Drawing.Point(12, 399);
			this.DownloadingLabel.Name = "DownloadingLabel";
			this.DownloadingLabel.Size = new System.Drawing.Size(72, 13);
			this.DownloadingLabel.TabIndex = 7;
			this.DownloadingLabel.Text = "Downloading:";
			// 
			// WebControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 477);
			this.Controls.Add(this.DownloadingLabel);
			this.Controls.Add(this.DownloadProgressBar);
			this.Controls.Add(this.DownloadAllBtn);
			this.Controls.Add(this.BtnYoutubeSearch);
			this.Controls.Add(this.savedTracksCountLabel);
			this.Controls.Add(this.savedTracksListView);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnAuth);
			this.DoubleBuffered = true;
			this.Name = "WebControl";
			this.Text = "Spotify Library Explorer";
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
	}
}

