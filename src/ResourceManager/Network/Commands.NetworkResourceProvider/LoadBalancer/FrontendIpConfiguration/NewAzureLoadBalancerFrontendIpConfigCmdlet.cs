﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Management.Automation;
using Microsoft.Azure.Commands.NetworkResourceProvider.Models;
using Microsoft.Azure.Commands.NetworkResourceProvider.Properties;

namespace Microsoft.Azure.Commands.NetworkResourceProvider
{
    [Cmdlet(VerbsCommon.New, "AzureLoadBalancerFrontendIpConfig"), OutputType(typeof(PSFrontendIpConfiguration))]
    public class NewAzureLoadBalancerFrontendIpConfigCmdlet : CommonAzureLoadBalancerFrontendIpConfig
    {
        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            // Get the subnetId and publicIpAddressId from the object if specified
            if (string.Equals(ParameterSetName, "object"))
            {
                this.SubnetId = this.Subnet.Id;

                if (PublicIpAddress != null)
                {
                    this.PublicIpAddressId = this.PublicIpAddress.Id;
                }
            }

            var frontendIpConfig = new PSFrontendIpConfiguration();
            frontendIpConfig.Name = this.Name;
            frontendIpConfig.Properties = new PSFrontendIpConfigurationProperties();
            frontendIpConfig.Properties.PrivateIpAllocationMethod = this.AllocationMethod;

            if (!string.IsNullOrEmpty(this.SubnetId))
            {
                frontendIpConfig.Properties.Subnet = new PSResourceId();
                frontendIpConfig.Properties.Subnet.Id = this.SubnetId;
            }

            if (!string.IsNullOrEmpty(this.PublicIpAddressId))
            {
                frontendIpConfig.Properties.PublicIpAddress = new PSResourceId();
                frontendIpConfig.Properties.PublicIpAddress.Id = this.PublicIpAddressId;
            }

            frontendIpConfig.Id =
                ChildResourceHelper.GetResourceNotSetId(
                    this.NetworkClient.NetworkResourceProviderClient.Credentials.SubscriptionId,
                    Resources.LoadBalancerFrontendIpConfigName,
                    this.Name);

            WriteObject(frontendIpConfig);

        }
    }
}
