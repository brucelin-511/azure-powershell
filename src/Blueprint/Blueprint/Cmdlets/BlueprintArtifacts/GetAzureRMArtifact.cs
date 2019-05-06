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

using Microsoft.Azure.Commands.Blueprint.Models;
using System;
using System.Management.Automation;
using ParameterSetNames = Microsoft.Azure.Commands.Blueprint.Common.BlueprintConstants.ParameterSetNames;
using ParameterHelpMessages = Microsoft.Azure.Commands.Blueprint.Common.BlueprintConstants.ParameterHelpMessages;
using System.Text.RegularExpressions;
using Microsoft.WindowsAzure.Commands.Utilities.Common;


namespace Microsoft.Azure.Commands.Blueprint.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "BlueprintArtifact", SupportsShouldProcess = true, DefaultParameterSetName = ParameterSetNames.ArtifactsByBlueprint), OutputType(typeof(PSBlueprintAssignment))]
    public class GetAzureRmArtifact : BlueprintArtifactsCmdletBase
    {
        #region Parameters
        [Parameter(ParameterSetName = ParameterSetNames.ArtifactsByBlueprint, Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "To-Do")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.ArtifactsByBlueprint, Mandatory = true, ValueFromPipeline = true, HelpMessage = "To-Do")]
        [ValidateNotNull]
        public PSBlueprintBase Blueprint { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.ArtifactsByBlueprint, Mandatory = false, HelpMessage = "Version of the blueprint to get the artifacts from.")]
        [ValidateNotNullOrEmpty]
        public string BlueprintVersion { get; set; }
        #endregion

        #region Cmdlet Overrides
        public override void ExecuteCmdlet()
        {
            var scope = Blueprint.Scope;

            try
            {
                if (this.IsParameterBound(c => c.Name))
                {
                    // To-Do - how is blueprint name different than dislplay name
                    WriteObject(BlueprintClient.GetArtifact(scope, Blueprint.Name, Name, BlueprintVersion));
                }
                else
                {
                    WriteObject(BlueprintClient.ListArtifacts(scope, Blueprint.Name, BlueprintVersion)); 
                }
            }
            catch (Exception ex)
            {
               WriteExceptionError(ex);
            }
        }
        #endregion
    }
}
