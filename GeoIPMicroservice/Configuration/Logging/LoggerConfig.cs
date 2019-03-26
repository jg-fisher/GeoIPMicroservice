using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.Runtime.CredentialManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.Amazon.Kinesis;
using Serilog.Sinks.Amazon.Kinesis.Stream.Sinks;
using Serilog.Sinks.AwsCloudWatch;

namespace GeoIPMicroservice.Configuration.Logging
{
    /// <summary>
    /// Configures Serilog sink logging to AWS Cloudwatch
    /// </summary>
    public class LoggerConfig
    {
        public void Build()
        {
            string logGroupName = "GeoIPMicroserviceLogGroup/" +
                               Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            CloudWatchSinkOptions options = new CloudWatchSinkOptions
            {
                LogGroupName = logGroupName,
                MinimumLogEventLevel = LogEventLevel.Information,
            };

            ConfigureCredentials();

            RegionEndpoint awsRegion = RegionEndpoint.USEast2;
            AmazonCloudWatchLogsClient client = new AmazonCloudWatchLogsClient(awsRegion);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.AmazonCloudWatch(options, client)
                .CreateLogger();
        }

        /// <summary>
        /// Sets credentials for AmazonCloudWatchLogsClient
        /// </summary>
        private void ConfigureCredentials()
        {
            var options = new CredentialProfileOptions { AccessKey = "access_key", SecretKey = "secret_key" }; // TODO: These would go into appsettings.json

            var profile = new Amazon.Runtime.CredentialManagement.CredentialProfile("basic_profile", options);

            profile.Region = RegionEndpoint.USEast2;

            var netSdkFile = new NetSDKCredentialsFile();
            netSdkFile.RegisterProfile(profile);
        }
    }
}