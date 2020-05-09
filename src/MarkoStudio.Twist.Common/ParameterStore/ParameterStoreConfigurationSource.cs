using Amazon.SimpleSystemsManagement;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace MarkoStudio.Twist.Common.ParameterStore
{
    public class ParameterStoreConfigurationSource : IConfigurationSource
    {
        private readonly IAmazonSimpleSystemsManagement _systemManagementClient;
        private readonly List<string> _paramNames;

        public ParameterStoreConfigurationSource(
            IAmazonSimpleSystemsManagement systemManagementClient,
            List<string> paramNames)
        {
            _paramNames = paramNames;
            _systemManagementClient = systemManagementClient ?? throw new ArgumentNullException(nameof(systemManagementClient));
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ParameterStoreConfigurationProvider(
                _systemManagementClient,
                _paramNames);
        }
    }
}
