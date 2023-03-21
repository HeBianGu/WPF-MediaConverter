using FFMpegCore;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Domain.Converter;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.Domain.Converter
{
    [Vip(-1)]
    public abstract class GroupBase : DisplayerViewModelBase
    {
        public GroupBase()
        {
            var att = this.GetType().GetCustomAttribute<VipAttribute>(true);
            if (att != null)
                this.Vip = att.Level;
        }
        public override void LoadDefault()
        {
            base.LoadDefault();
            OutPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Name);
            if (!Directory.Exists(OutPath))
                Directory.CreateDirectory(OutPath);
        }
        private string _outPath;
        [PropertyItemType(typeof(OpenPathTextPropertyItem))]
        [Displayer(Name = "输出文件夹", Icon = "\xe751", GroupName = "输出文件夹", Description = "处理器相关数据监控")]
        public string OutPath
        {
            get { return _outPath; }
            set
            {
                _outPath = value;
                RaisePropertyChanged();
            }
        }


        private int _vip = -1;
        /// <summary> 说明  </summary>
        public int Vip
        {
            get { return _vip; }
            set
            {
                _vip = value;
                RaisePropertyChanged();
            }
        }


        //private bool _combineAll;
        //[Displayer(Name = "合并全部文件", Icon = "\xe751", GroupName = "配置", Description = "处理器相关数据监控")]
        //public bool CombineAll
        //{
        //    get { return _combineAll; }
        //    set
        //    {
        //        _combineAll = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private ObservableSource<ConverterItemBase> _collection = new ObservableSource<ConverterItemBase>();
        /// <summary> 说明  </summary>
        public ObservableSource<ConverterItemBase> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        [Displayer(Name = "添加任务", Icon = "\xe6e1", GroupName = "工具栏", Description = "添加任务")]
        public RelayCommand AddFileCommand => new RelayCommand(async (s, e) =>
        {
            await this.CreateConverterItemAsync();
        });

        protected abstract Task CreateConverterItemAsync();
    }

    /// <summary> 说明</summary>
    public abstract class ConverterGroupBase : GroupBase
    {
        protected override async Task CreateConverterItemAsync()
        {
            //var fromats = FFMpeg.GetContainerFormats();
            //var extensions = fromats.Select(x => $"{x.Name}文件(*{x.Extension})|*{x.Extension}");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
            string p = "*.wmv;*.asf;*.asx;*.rm;*.rmvb;*.mpg;*.mpeg;*.mpe;*.3gp;*.mov;*.mp4;*.m4v;*.avi;*.dat;*.mkv;*.flv;*.vob;*.dat;*.bdmv;";
            //openFileDialog.Filter = "MP4文件(*.mp4)|*.mp4|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.Filter = $"视频文件|{p}|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            //openFileDialog.Filter = "所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 1; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = false;//设置多选
            if (openFileDialog.ShowDialog() != true)
                return;
            var item = await MessageProxy.Messager.ShowWaitter(() =>
            {
                return CreateConverterItem(openFileDialog.FileName);
            });
            Collection.Add(item);
            MessageProxy.Snacker.ShowTime("添加完成");
        }


        protected abstract ConverterItemBase CreateConverterItem(string filePath);

        [Displayer(Name = "添加文件夹", Icon = "\xe6e1", GroupName = "工具栏", Description = "处理器相关数据监控")]
        public RelayCommand AddFolderCommand => new RelayCommand(async (s, e) =>
        {
            System.Windows.Forms.FolderBrowserDialog m_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = m_Dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;
            string m_Dir = m_Dialog.SelectedPath.Trim();

            await MessageProxy.Messager.ShowStringProgress(x =>
            {
                x.MessageStr = "正在查找文件...";
                var extensions = FFMpeg.GetContainerFormats().Select(x => x.Extension);
                var files = m_Dir.GetAllFiles(x => extensions.Any(l => l == x.Extension));

                for (int i = 0; i < files.Count; i++)
                {
                    var item = CreateConverterItem(files[i]);
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                              {
                                  Collection.Add(item);
                              }));
                    x.MessageStr = $"正在加载数据，共计{i}/{files.Count}条";
                }
                x.MessageStr = "添加完成";
                Thread.Sleep(1000);
            });
            MessageProxy.Snacker.ShowTime("添加完成");
        });

        //[Displayer(Name = "转换全部", Icon = "\xe8dc", GroupName = "工具栏", Description = "处理器相关数据监控")]
        //public RelayCommand ConvertAllCommand => new RelayCommand((s, e) =>
        //{
        //    if (e is string project)
        //    {

        //    }
        //});

        [Displayer(Name = "清空", Icon = "\xe844", GroupName = "工具栏", Description = "处理器相关数据监控")]
        public RelayCommand ClearCommand => new RelayCommand((s, e) =>
        {
            Collection.Foreach(x =>
            {
                x.StopCommand.Execute(e);
            });
            Collection.Clear();
        });

    }
}
