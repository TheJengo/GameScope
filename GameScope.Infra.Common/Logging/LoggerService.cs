using GameScope.Infra.Common.Logging.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Email;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace GameScope.Infra.Common.Logging
{
    public class LoggerService : ILoggerService
    {
        private readonly Logger _logger;
        private readonly IOptions<SerilogEmailConfiguration> _serilogEmailConfiguration;

        public LoggerService(IOptions<SerilogEmailConfiguration> serilogEmailConfiguration)
        {
            _serilogEmailConfiguration = serilogEmailConfiguration;

            // Reading serilog configuration from appsettings.json
            var conf = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build(), "Serilog", null);

            // There is a problem on serilog.sinks.email package to read port number and other extra configs.
            // So we are additionally building serilog email configuration from appsettings.json
            if (_serilogEmailConfiguration.Value.Args != null)
            {
                var fromEmail = _serilogEmailConfiguration.Value.Args.FromEmail;
                var toEmail = _serilogEmailConfiguration.Value.Args.ToEmail;
                var mailServer = _serilogEmailConfiguration.Value.Args.MailServer;
                var networkCredentials = new NetworkCredential
                {
                    UserName = _serilogEmailConfiguration.Value.Args.NetworkCredentials.Username,
                    Password = _serilogEmailConfiguration.Value.Args.NetworkCredentials.Password
                };
                var port = _serilogEmailConfiguration.Value.Args.Port;
                var batchPostingLimit = _serilogEmailConfiguration.Value.Args.BatchPostingLimit;
                var enableSsl = _serilogEmailConfiguration.Value.Args.EnableSsl;
                var isBodyHtml = _serilogEmailConfiguration.Value.Args.IsBodyHtml;
                var subject = _serilogEmailConfiguration.Value.Args.MailSubject;
                var outputTemplate = _serilogEmailConfiguration.Value.Args.OutputTemplate;

                conf.Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Email(new EmailConnectionInfo
                    {
                        EmailSubject = subject,
                        EnableSsl = enableSsl,
                        FromEmail = fromEmail,
                        IsBodyHtml = isBodyHtml,
                        MailServer = mailServer,
                        NetworkCredentials = networkCredentials,
                        Port = port,
                        ToEmail = toEmail
                    }, outputTemplate, LogEventLevel.Error, batchPostingLimit, null);
            }

            _logger = conf.CreateLogger();
            SelfLog.Enable(msg => LogError(msg));
        }

        public void LogInformation(string message, params object[] parameters)
        {
            _logger.Information(message, parameters);
        }

        public void LogInformation(LogChange log)
        {
            _logger.Information(Newtonsoft.Json.JsonConvert.SerializeObject(log));
        }

        public void LogWarning(string message)
        {
            _logger.Warning(message);
        }

        public void LogWarning(LogChange log)
        {
            _logger.Warning(Newtonsoft.Json.JsonConvert.SerializeObject(log));
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogDebug(LogChange log)
        {
            _logger.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(log));
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogError(LogChange log)
        {
            _logger.Error(Newtonsoft.Json.JsonConvert.SerializeObject(log));
        }

        public void LogVerbose(string message)
        {
            _logger.Verbose(message);
        }

        public void LogVerbose(LogChange log)
        {
            _logger.Verbose(Newtonsoft.Json.JsonConvert.SerializeObject(log));
        }

        public void LogFatal(string message)
        {
            _logger.Fatal(message);
        }

        public void LogFatal(LogChange log)
        {
            _logger.Fatal(Newtonsoft.Json.JsonConvert.SerializeObject(log));
        }

        public void Write(LogEventLevel level, Exception ex, string template)
        {
            _logger.Write(level, ex, template);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
