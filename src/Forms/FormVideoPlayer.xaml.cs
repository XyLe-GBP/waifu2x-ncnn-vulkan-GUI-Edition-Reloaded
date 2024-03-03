using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;
using NVGE.Localization;

namespace NVGE
{
    /// <summary>
    /// FormVideoPlayer.xaml の相互作用ロジック
    /// </summary>
    public partial class FormVideoPlayer : Window
    {
        private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };
        private bool _isPlayed = false, _isPaused = false;

        // タイムスライダーのドラッグ中を判定するフラグ
        private bool _isSliderDragging = false;
        public FormVideoPlayer(string path)
        {
            InitializeComponent();

            loop.Content = Strings.VideoplayerEnableloopCaption;
            capture.Content = Strings.VideoplayerCurrentFrameCaption;
            labelvolume.Content = Strings.VideoplayerVolumeCaption;
            labelspeed.Content = Strings.VideoplayerSpeedCaption;

            // 再生時刻表示用タイマーにイベントハンドラを登録
            _timer.Tick += Timer_Tick;

            // 各種コントロールのリセット
            ResetVideoPlayer();

            play.Content = "⏸";
            player.Source = new Uri(path);
            player.Play();
            _isPlayed = true;
            _timer.Start();
        }

        /// <summary>
        /// ビデオ再生関連のコントロールの値をリセット
        /// </summary>
        public void ResetVideoPlayer()
        {
            //ボリュームと再生速度のスライダー初期値をリセット
            time.Value = 0;
            uxTime.Text = "00:00:00";
            uxVolume.Text = ((int)(volume.Value * 100)).ToString() + "%";
            uxSpeed.Text = "x" + Math.Round(speed.Value, 1).ToString();

            // MediaElement の値をリセット
            player.Position = TimeSpan.FromMilliseconds(time.Value);
            player.Volume = (double)volume.Value;
            player.SpeedRatio = (double)speed.Value;

            _isPlayed = false; _isPaused = false;
        }

        /// <summary>
        /// 再生時刻の表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (player.NaturalDuration.HasTimeSpan)
            {
                if (!_isSliderDragging)
                {
                    time.Value = player.Position.TotalMilliseconds;
                    uxTime.Text = player.Position.ToString()[..8];
                }
            }
            if (player.NaturalDuration.HasTimeSpan && player.NaturalDuration.TimeSpan.TotalMilliseconds == time.Value)
            {
                if (loop.IsChecked == true)
                {
                    player.Position = TimeSpan.FromMilliseconds(0);
                    player.Stop();
                    player.Close();
                    _timer.Stop();
                    ResetVideoPlayer();
                    _isPlayed = true;
                    play.Content = "⏸";
                    player.Play();
                    _timer.Start();
                }
                else
                {
                    play.Content = "▶";
                    player.Position = TimeSpan.FromMilliseconds(0);
                    player.Stop();
                    player.Close();
                    _timer.Stop();
                    ResetVideoPlayer();
                }
            }
        }

        /// <summary>
        /// 再生ボタンクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (_isPlayed)
            {
                _isPlayed = false;
                _isPaused = true;
                play.Content = "▶";
                player.Pause();
                _timer.Stop();
            }
            else if (_isPaused)
            {
                _isPaused = false;
                _isPlayed = true;
                play.Content = "⏸";
                player.Play();
                _timer.Start();
            }
            else
            {
                _isPlayed = true;
                play.Content = "⏸";
                player.Play();
                _timer.Start();
            }
        }

        /// <summary>
        /// 一時停止ボタンクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
            _timer.Stop();
        }

        /// <summary>
        /// ストップボタンクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            play.Content = "▶";
            player.Position = TimeSpan.FromMilliseconds(0);

            player.Stop();
            player.Close();
            _timer.Stop();
            ResetVideoPlayer();
        }

        /// <summary>
        /// 動画ファイルをオープンした時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Video Files|*.mp4;*.mkv;*.avi;*.wmv|All Files|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                // 選択された動画ファイルを再生
                player.Source = new Uri(dialog.FileName);
                player.Play();
                _timer.Start();
            }
        }


        /// <summary>
        /// タイムラインスライダーの値が変化した時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // タイムラインスライダーがドラッグ中の時はスライダーの値で時刻表示を更新
            if (_isSliderDragging)
            {
                uxTime.Text = TimeSpan.FromMilliseconds(time.Value).ToString()[..8];
            }
        }

        /// <summary>
        /// タイムラインスライダーのマウスが押された時（ドラッグ開始）のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isSliderDragging = true;
        }

        /// <summary>
        /// タイムラインスライダーのマウスが離された時（ドラッグ終了）のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isSliderDragging = false;
            player.Position = TimeSpan.FromMilliseconds(time.Value);
        }

        /// <summary>
        /// 動画ファイル再生開始のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Player_MediaOpened(object sender, RoutedEventArgs e)
        {
            time.Maximum = player.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        /// <summary>
        /// ボリュームスライダーのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.Volume = (double)volume.Value;
            if (uxVolume != null)
            {
                uxVolume.Text = ((int)(player.Volume * 100)).ToString() + "%";
            }
        }

        /// <summary>
        /// 再生スピードスライダーのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Speed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.SpeedRatio = (double)speed.Value;
            if (uxSpeed != null)
            {
                uxSpeed.Text = "x" + Math.Round(player.SpeedRatio, 1).ToString();
            }
        }

        /// <summary>
        /// キャプチャボタンのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Capture_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new()
            {
                FileName = "Capture_" + Common.SFDRandomNumber(),
                InitialDirectory = "",
                Filter = Strings.VideoplayerSaveFramefilterCaption,
                FilterIndex = 1,
                Title = Strings.VideoplayerSaveFrameTitleCaption,
                OverwritePrompt = true,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() == true)
            {
                FileInfo fi = new(sfd.FileName);
                //var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                //var filename = "Capture_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";

                SaveFrame(player, Path.Combine(fi.DirectoryName, fi.Name));
                if (File.Exists(fi.FullName))
                {
                    Process.Start("EXPLORER.EXE", @"/select,""" + fi.FullName + @"""");
                }
                else
                {
                    MessageBox.Show(Strings.VideoplayerSaveFrameErrorCaption, Strings.MSGError, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                return;
            }
            
        }

        private void Speed_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Pause();
        }

        private void Speed_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            player.Play();
        }

        /// <summary>
        /// 動画のキャプチャを取る
        /// </summary>
        /// <param name="mediaElement"></param>
        /// <param name="fileName"></param>
        private static void SaveFrame(MediaElement mediaElement, string fileName)
        {
            //動画が再生されていないと例外が発生するので、try～catchで例外を無効化
            try
            {
                // キャプチャ対象のフレームをRenderTargetBitmapにレンダリングする
                RenderTargetBitmap renderTargetBitmap = new(
                    (int)mediaElement.ActualWidth,
                    (int)mediaElement.ActualHeight,
                    96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(mediaElement);

                // ファイルに保存する
                using FileStream fileStream = new(fileName, FileMode.Create);
                JpegBitmapEncoder encoder = new();
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                encoder.Save(fileStream);
            }
            catch { }
        }
    }
}
