using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace MarkoStudio.Twist.Common.ParameterStore
{
    public class ParameterStoreConfigurationProvider : ConfigurationProvider
    {
        private readonly IAmazonSimpleSystemsManagement _systemManagementClient;
        private readonly IEnumerable<string> _paramNames;

        public ParameterStoreConfigurationProvider(
            IAmazonSimpleSystemsManagement systemManagementClient,
            IEnumerable<string> paramNames)
        {
            _systemManagementClient = systemManagementClient ?? throw new ArgumentNullException(nameof(systemManagementClient));
            _paramNames = paramNames;
        }

        public override void Load()
        {
            try
            {
                var request = new GetParametersRequest
                {
                    Names = _paramNames.ToList()
                };

                var task = _systemManagementClient.GetParametersAsync(request).ConfigureAwait(false);
                var response = task.GetAwaiter().GetResult();

                foreach (var parameter in response.Parameters)
                {
                    if (parameter.Type == ParameterType.StringList)
                    {
                        var values = parameter.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        var index = 0;
                        foreach (var value in values)
                        {
                            Data.Add($"{parameter.Name}:{index++}", value);
                        }
                    }
                    else
                    {
                        Data.Add(parameter.Name, parameter.Value);
                    }
                }
            }
            catch (Exception exception)
            {
                var message = "Could not load SSM configuration";

                Console.WriteLine($"{message}\n{exception}");
            }
        }
    }
}
