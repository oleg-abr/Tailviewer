﻿using System;
using System.Collections.Generic;

namespace Tailviewer.BusinessLogic
{
	public sealed class LogFileListenerCollection
	{
		private readonly Dictionary<ILogFileListener, LogFileListenerNotifier> _listeners;
		private int _currentLineIndex;
		private readonly ILogFile _logFile;

		public LogFileListenerCollection(ILogFile logFile)
		{
			if (logFile == null) throw new ArgumentNullException("logFile");

			_logFile = logFile;
			_listeners = new Dictionary<ILogFileListener, LogFileListenerNotifier>();
			_currentLineIndex = -1;
		}

		public void AddListener(ILogFileListener listener, TimeSpan maximumWaitTime, int maximumLineCount)
		{
			lock (_listeners)
			{
				if (!_listeners.ContainsKey(listener))
				{
					var notifier = new LogFileListenerNotifier(_logFile, listener, maximumWaitTime, maximumLineCount);
					_listeners.Add(listener, notifier);
					notifier.OnRead(_currentLineIndex);
				}
			}
		}

		public void RemoveListener(ILogFileListener listener)
		{
			lock (_listeners)
			{
				_listeners.Remove(listener);
			}
		}

		public void OnRead(int numberOfLinesRead)
		{
			lock (_listeners)
			{
				foreach (var notifier in _listeners.Values)
				{
					notifier.OnRead(numberOfLinesRead);
				}
			}
			_currentLineIndex = numberOfLinesRead;
		}
	}
}