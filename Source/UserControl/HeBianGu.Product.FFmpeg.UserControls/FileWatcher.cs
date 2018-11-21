using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.UserControls
{
  /// <summary>
      /// 文件监控类，用于监控指定目录下文件以及文件夹的变化
      /// </summary>
      public class FileWatcher
      {
          private FileSystemWatcher _watcher = null;
          private string _path = string.Empty;
          private string _filter = string.Empty;
          private bool _isWatch = false;
  
          /// <summary>
          /// 监控是否正在运行
          /// </summary>
          public bool IsWatch
          {
              get
              {
                  return _isWatch;
              }
          }
  
          /// <summary>
          /// 初始化FileWatcher类
          /// </summary>
          /// <param name="path">监控路径</param>
          public FileWatcher(string path)
          {
              _path = path;
          }
          /// <summary>
          /// 初始化FileWatcher类，并指定是否持久化文件变更消息
          /// </summary>
          /// <param name="path">监控路径</param>
          /// <param name="isPersistence">是否持久化变更消息</param>
          /// <param name="persistenceFilePath">持久化保存路径</param>
          public FileWatcher(string path, bool isPersistence, string persistenceFilePath)
          {
              _path = path;
          }
  
          /// <summary>
          /// 初始化FileWatcher类，并指定是否监控指定类型文件
          /// </summary>
          /// <param name="path">监控路径</param>
          /// <param name="filter">指定类型文件，格式如:*.txt,*.doc,*.rar</param>
          public FileWatcher(string path, string filter)
          {
              _path = path;
              _filter = filter;
          }
  
          /// <summary>
          /// 打开文件监听器
          /// </summary>
          public void Open()
          {
              if (!Directory.Exists(_path))
              {
                  Directory.CreateDirectory(_path);
              }
  
              if (string.IsNullOrEmpty(_filter))
              {
                  _watcher = new FileSystemWatcher(_path);
              }
              else
              {
                  _watcher = new FileSystemWatcher(_path, _filter);
              }

              //注册监听事件
            _watcher.Created += new FileSystemEventHandler(OnProcess);
            _watcher.Changed += new FileSystemEventHandler(OnProcess);
            _watcher.Deleted += new FileSystemEventHandler(OnProcess);
            _watcher.Renamed += new RenamedEventHandler(OnFileRenamed);
            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
            _isWatch = true;
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            if(this.ChangeAction!=null)
            {
                this.ChangeAction();
            }
        }

        private void OnProcess(object sender, FileSystemEventArgs e)
        {
            if (this.ChangeAction != null)
            {
                this.ChangeAction();
            }
        }

        public Action ChangeAction;

        /// <summary>
        /// 关闭监听器
        /// </summary>
        public void Close()
        {
            _isWatch = false;
            _watcher.Created -= new FileSystemEventHandler(OnProcess);
            _watcher.Changed -= new FileSystemEventHandler(OnProcess);
            _watcher.Deleted -= new FileSystemEventHandler(OnProcess);
            _watcher.Renamed -= new RenamedEventHandler(OnFileRenamed);
            _watcher.EnableRaisingEvents = false;
            _watcher = null;
        }

    }
}
