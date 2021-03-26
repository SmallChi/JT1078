using JT1078.SignalR.Test.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;


namespace JT1078.SignalR.Test.Hubs
{

	public class FMp4Hub : Hub
    {
		private readonly ILogger logger;
		private readonly WsSession wsSession;

		public FMp4Hub(
			WsSession wsSession,
			ILoggerFactory loggerFactory) 
		{
			this.wsSession = wsSession;
			logger = loggerFactory.CreateLogger<FMp4Hub>();
		}

		public override Task OnConnectedAsync()
		{		
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug($"链接上:{Context.ConnectionId}");
			}
			wsSession.TryAdd(Context.ConnectionId);
			return base.OnConnectedAsync();	
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug($"断开链接:{Context.ConnectionId}");
			}
			wsSession.TryRemove(Context.ConnectionId);
			return base.OnDisconnectedAsync(exception);
		}
	}
}
