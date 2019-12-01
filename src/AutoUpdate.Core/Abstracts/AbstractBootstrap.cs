using AutoUpdate.Core.Events;
using AutoUpdate.Core.Interfaces;
using AutoUpdate.Core.Models;
using AutoUpdate.Core.Update;
using AutoUpdate.Core.Utils;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Net;
using System.Threading;

namespace AutoUpdate.Core.Abstracts
{
    public abstract class AbstractBootstrap<TBootstrap, TStrategy>
        where TBootstrap : AbstractBootstrap<TBootstrap, TStrategy>
        where TStrategy : IStrategy
    {
        #region Private Members

        private Timer _speedTimer;
        private readonly ConcurrentDictionary<UpdateOption, UpdateOptionValue> options;
        private volatile Func<TStrategy> strategyFactory;
        private readonly WebClient webClient;
        private DateTime _startTime = new DateTime();
        private UpdatePacket _packet;
        private IStrategy strategy;

        public delegate void UpdateStatusEventHandler(object sender, UpdateStatusEventArgs e);
        public event UpdateStatusEventHandler UpdateStatusChanged;

        public delegate void DownloadStatisticsEventHandler(object sender, DownloadStatisticsEventArgs e);
        public event DownloadStatisticsEventHandler DownloadStatistics;

        public delegate void DownloadProgressChangedExEventHandler(object sender, DownloadProgressChangedExEventArgs e);
        public event DownloadProgressChangedExEventHandler DownloadProgressChangedEx;

        #endregion

        #region Constructors

        protected internal AbstractBootstrap() {
            this.options = new ConcurrentDictionary<UpdateOption, UpdateOptionValue>();
            this.webClient = new WebClient();
            webClient.DownloadFileCompleted += OnDownloadFileCompleted;
            webClient.DownloadProgressChanged += OnDownloadProgressChanged;
            _speedTimer = new Timer(SpeedTimerOnTick, null, 0, 1000);
        }

        protected internal AbstractBootstrap(AbstractBootstrap<TBootstrap, TStrategy> bootstrap)
        {
            this.Packet = bootstrap.Packet;
            this.options = new ConcurrentDictionary<UpdateOption, UpdateOptionValue>();
            this.webClient = new WebClient();
            webClient.DownloadFileCompleted += OnDownloadFileCompleted;
            webClient.DownloadProgressChanged += OnDownloadProgressChanged;
            _speedTimer = new Timer(SpeedTimerOnTick, null, 0, 1000);
        }

        #endregion

        #region Public Properties

        public UpdatePacket Packet {
            get { return _packet ?? (_packet = new UpdatePacket()); }
            set { _packet = value; }
        }

        #endregion

        #region Methods

        public virtual TBootstrap Strategy<T>() where T : TStrategy, new() => this.StrategyFactory(() => new T());

        public TBootstrap StrategyFactory(Func<TStrategy> strategyFactory)
        {
            Contract.Requires(strategyFactory != null);
            this.strategyFactory = strategyFactory;
            return (TBootstrap)this;
        }

        public virtual TBootstrap Option<T>(UpdateOption<T> option, T value) {
            Contract.Requires(option != null);
            if (value == null)
            {
                UpdateOptionValue removed;
                this.options.TryRemove(option, out removed);
            }
            else
            {
                this.options[option] = new UpdateOptionValue<T>(option, value);
            }
            return (TBootstrap)this;
        }

        public virtual T GetOption<T>(UpdateOption<T> option){
            if (UpdateOption.Format.Equals(option))
            {
                var val = options[option];
                return (T)val.GetValue();
            }
            return default;
        }

        protected static void SetChannelOptions(IStrategy strategy, UpdateOptionValue[] options)
        {
            foreach (var e in options)
            {
                SetChannelOption(strategy, e);
            }
        }

        protected static void SetChannelOption(IStrategy strategy, UpdateOptionValue option)
        {
            try
            {
                if (!option.Set(strategy.Configuration))
                {
                    //logger.Warn("Unknown channel option '{}' for channel '{}'", option.Option, channel);
                }
            }
            catch (Exception ex)
            {
                //logger.Warn("Failed to set channel option '{}' with value '{}' for channel '{}'", option.Option, option, channel, ex);
            }
        }

        public virtual TBootstrap Launch() {
            var pacektFormat = GetOption(UpdateOption.Format) ?? "zip";
            Packet.Format = $".{pacektFormat}";
            webClient.DownloadFileAsync(new Uri(Packet.Url), $"{Packet.TempPath}{Packet.Format}");
            return (TBootstrap)this;
        }

        private void SpeedTimerOnTick(object sender)
        {
            var interval = DateTime.Now - _startTime;

            //下载速度
            var downLoadSpeed = interval.Seconds < 1
                ? StatisticsUtil.ToUnit(Packet.ReceivedBytes)
                : StatisticsUtil.ToUnit(Packet.ReceivedBytes / interval.Seconds);

            //剩余时间
            var size = (Packet.TotalBytes - Packet.ReceivedBytes) / (1024 * 1024);
            var remainingTime = new DateTime().AddSeconds(Convert.ToDouble(size));

            var args = new DownloadStatisticsEventArgs();
            args.Remaining = remainingTime;
            args.Speed = downLoadSpeed;
            DownloadStatistics(this, args);
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var value = e.ProgressPercentage;
            if (value > 90)
            {
                value = 90;
            }
            Packet.ReceivedBytes = e.BytesReceived;
            Packet.TotalBytes = e.TotalBytesToReceive;

            var args = new DownloadProgressChangedExEventArgs();
            args.ProgressValue = value;
            args.ReceivedSize = e.BytesReceived / (1024.0 * 1024.0);
            args.TotalSize = e.TotalBytesToReceive / (1024.0 * 1024.0);
            DownloadProgressChangedEx(this, args);
        }

        /// <summary>
        /// 下载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //停止下载信息统计
            _speedTimer.Dispose();
            _speedTimer = null;

            //触发进度事件
            UpdateStatusEventArgs args = new UpdateStatusEventArgs();
            args.Status = "Completed";
            args.Code = 200;
            DoStatusEvent(this,args);

            //执行策略
            DoExcute();
        }

        protected void DoStatusEvent(object sender,UpdateStatusEventArgs eventArgs) {
            UpdateStatusChanged(sender, eventArgs);
        }

        public abstract TBootstrap Clone();

        #region Strategy

        protected IStrategy InitStrategy()
        {
            if (strategy == null)
            {
                Validate();
                strategy = this.strategyFactory();
                Packet.Format = GetOption(UpdateOption.Format);
                strategy.Create(Packet, DoStatusEvent);
            }
            return strategy;
        }

        IStrategy DoExcute()
        {
            var strategy = this.InitStrategy();
            strategy.Excute();
            return strategy;
        }

        public virtual TBootstrap Validate()
        {
            if (this.strategyFactory == null)
            {
                throw new InvalidOperationException("strategy or strategyFactory not set");
            }
            return (TBootstrap)this;
        }

        #endregion

        #endregion

        #region class

        protected abstract class UpdateOptionValue
        {
            public abstract UpdateOption Option { get; }
            public abstract bool Set(IUpdateConfiguration config);

            public abstract object GetValue();
        }

        protected sealed class UpdateOptionValue<T> : UpdateOptionValue
        {
            public override UpdateOption Option { get; }
            readonly T value;

            public UpdateOptionValue(UpdateOption<T> option, T value)
            {
                this.Option = option;
                this.value = value;
            }

            public override object GetValue() {
                return this.value;
            }

            public override bool Set(IUpdateConfiguration config) => config.SetOption(this.Option, this.value);

            public override string ToString() => this.value.ToString();

        }

        #endregion
    }
}
