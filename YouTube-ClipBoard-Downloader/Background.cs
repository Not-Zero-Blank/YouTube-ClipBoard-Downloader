using Syroot.Windows.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YouTube_ClipBoard_Downloader
{
    public partial class Background : Form
    {
        public Background()
        {
            InitializeComponent();
            this.Visible = false;
            CheckClipboard();
        }

        private void Background_Load(object sender, EventArgs e)
        {

        }
        string Checked = "";
        async void CheckClipboard()
        {
            string txt = Clipboard.GetText();
            if (true)
            {

                Console.WriteLine(txt);
                if (txt != Checked)
                {
                    Console.WriteLine("Checking " + txt);
                    Checked = txt;
                    if (txt.Contains("youtube") || txt.Contains("youtu.be"))
                    {

                        try
                        {
                            var youtube = new YoutubeClient();
                            var video = await youtube.Videos.GetAsync(txt);
                            var title = video.Title;
                            var author = video.Author.Title;
                            var result = MessageBox.Show("Do you want to Download " + title + " from " + author, string.Empty, MessageBoxButtons.YesNo);
                            while (result == DialogResult.None)
                            {

                            }
                            if (result == DialogResult.Yes)
                            {
                                string downloadsPath = KnownFolders.Downloads.Path;
                                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(txt);
                                var result2 = MessageBox.Show("Do you want to Download it as MP3?\nYes=MP3\nNo=MP4", string.Empty, MessageBoxButtons.YesNo);
                                while (result2 == DialogResult.None)
                                {

                                }
                                if (result2 == DialogResult.Yes)
                                {
                                    var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                                    var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                                    await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{downloadsPath}/{video.Title}.{streamInfo.Container}");
                                }
                                else if (result2 == DialogResult.No)
                                {
                                    var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                                    var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                                    await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{downloadsPath}/{video.Title}.{streamInfo.Container}");
                                }
                                else
                                {
                                    Checked = "";
                                }
                            }
                            else
                            {

                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
            await Task.Delay(1000);
            CheckClipboard();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Do you want to Close the Programm?", string.Empty, MessageBoxButtons.YesNo);
            while (result == DialogResult.None)
            {

            }
            if (result == DialogResult.Yes)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {

            }
        }

        private void Background_Load_1(object sender, EventArgs e)
        {

        }
    }
}
