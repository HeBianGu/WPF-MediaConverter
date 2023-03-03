﻿using FFMpegCore;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.App.Converter
{
    /// <summary> 说明</summary>
    public abstract class GroupBase : DisplayerViewModelBase
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.OutPath = AppDomain.CurrentDomain.BaseDirectory;
        }
        private string _outPath;
        [PropertyItemType(typeof(OpenPathTextPropertyItem))]
        [Displayer(Name = "输出文件夹", Icon = "\xe751", GroupName = "配置", Description = "处理器相关数据监控")]
        public string OutPath
        {
            get { return _outPath; }
            set
            {
                _outPath = value;
                RaisePropertyChanged();
            }
        }


        private bool _combineAll;
        [Displayer(Name = "合并全部文件", Icon = "\xe751", GroupName = "配置", Description = "处理器相关数据监控")]
        public bool CombineAll
        {
            get { return _combineAll; }
            set
            {
                _combineAll = value;
                RaisePropertyChanged();
            }
        }


    }

    /// <summary> 说明</summary>
    public abstract class ConverterGroupBase : GroupBase
    {
        public ConverterGroupBase()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    this.Collection.Add(new ConverterItem() { Name = i.ToString() });
            //}
        }
        [Displayer(Name = "添加文件", Icon = "\xe751", GroupName = "Processor", Description = "处理器相关数据监控")]
        public RelayCommand AddFileCommand => new RelayCommand(async (s, e) =>
        {
            //var fromats = FFMpeg.GetContainerFormats();
            //var extensions = fromats.Select(x => $"{x.Name}文件(*{x.Extension})|*{x.Extension}");
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            //openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
            openFileDialog.Filter = "MP4文件(*.mp4)|*.mp4|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            //openFileDialog.Filter = "所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = false;//设置多选
            if (openFileDialog.ShowDialog() != true)
                return;
            var item = await MessageProxy.Messager.ShowWaitter(() =>
              {
                  return this.CreateConverterItem(openFileDialog.FileName);
              });
            this.Collection.Add(item);
            MessageProxy.Snacker.ShowTime("添加完成");
        });

        protected abstract ConverterItemBase CreateConverterItem(string filePath);

        [Displayer(Name = "添加文件夹", Icon = "\xe751", GroupName = "Processor", Description = "处理器相关数据监控")]
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
                var files = HeBianGu.Base.WpfBase.Converter.GetAllFiles(m_Dir, x => extensions.Any(l => l == x.Extension));

                for (int i = 0; i < files.Count; i++)
                {
                    var item = this.CreateConverterItem(files[i]);
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                              {
                                  this.Collection.Add(item);
                              }));
                    x.MessageStr = $"正在加载数据，共计{i}/{files.Count}条";
                }
                x.MessageStr = "添加完成";
                Thread.Sleep(1000);
            });
            MessageProxy.Snacker.ShowTime("添加完成");
        });

        [Displayer(Name = "转换全部", Icon = "\xe751", GroupName = "Processor", Description = "处理器相关数据监控")]
        public RelayCommand ConvertAllCommand => new RelayCommand((s, e) =>
        {
            if (e is string project)
            {

            }
        });

        [Displayer(Name = "清空", Icon = "\xe751", GroupName = "Processor", Description = "处理器相关数据监控")]
        public RelayCommand ClearCommand => new RelayCommand((s, e) =>
        {
            this.Collection.Foreach(x =>
            {
                x.StopCommand.Execute(e); 
            });
            this.Collection.Clear();
        });

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

    }
}
