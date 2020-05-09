using Amazon;
using Amazon.SimpleSystemsManagement;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace MarkoStudio.Twist.Common.ParameterStore
{
    public static class Extensions
    {
        public static IConfigurationBuilder AddParameterStoreConfig(
            this IConfigurationBuilder builder,
            List<string> paramNames)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            var systemManagementClient = new AmazonSimpleSystemsManagementClient(RegionEndpoint.USWest2);
            return builder.Add((IConfigurationSource)new ParameterStoreConfigurationSource(systemManagementClient, paramNames));
        }
    }
}
