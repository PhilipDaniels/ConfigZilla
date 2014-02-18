using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConfigZilla.Tasks
{
    public abstract class TaskWithProperties : Task
    {
        public ProjectInstance CurrentProjectInstance
        {
            get
            {
                if (_ProjectInstance == null)
                {
                    _ProjectInstance = GetCurrentProjectInstance(BuildEngine);
                }
                return _ProjectInstance;
            }
        }
        ProjectInstance _ProjectInstance;

        public IDictionary<string, string> Properties
        {
            get
            {
                if (_Properties == null)
                {
                    _Properties = GetPropertyValues(CurrentProjectInstance);
                }
                return _Properties;
            }
        }
        IDictionary<string, string> _Properties;


        public string GetProperty(string propertyName)
        {
            return Properties[propertyName];
        }

        /// <summary>
        /// Logs the properties for debugging purposes.
        /// </summary>
        public void DumpProperties(MessageImportance loggingLevel)
        {
            foreach (var name in Properties.Keys.OrderBy(n => n))
            {
                Log.LogMessage
                    (
                    loggingLevel,
                    "Extracted property {0} = {1}",
                    name, Properties[name]
                    );
            }
        }



        /// <summary>
        /// Return the currently executing project.
        /// See http://simoncropp.com/getprojectpathanmsbuildtask
        /// and http://simoncropp.com/howtoaccessbuildvariablesfromanmsbuildtask
        /// </summary>
        /// <param name="buildEngine">The BuildEngine evaluating the project.</param>
        ProjectInstance GetCurrentProjectInstance(IBuildEngine buildEngine)
        {
            const BindingFlags bindingFlags = BindingFlags.NonPublic |
                                                BindingFlags.FlattenHierarchy |
                                                BindingFlags.Instance |
                                                BindingFlags.Public;

            var buildEngineType = buildEngine.GetType();
            var targetBuilderCallbackField = buildEngineType.GetField("targetBuilderCallback", bindingFlags);
            if (targetBuilderCallbackField == null)
            {
                throw new Exception("Could not extract targetBuilderCallback from " + buildEngineType.FullName);
            }
            var targetBuilderCallback = targetBuilderCallbackField.GetValue(buildEngine);
            var targetCallbackType = targetBuilderCallback.GetType();
            var projectInstanceField = targetCallbackType.GetField("projectInstance", bindingFlags);
            if (projectInstanceField == null)
            {
                throw new Exception("Could not extract projectInstance from " + targetCallbackType.FullName);
            }

            return (ProjectInstance)projectInstanceField.GetValue(targetBuilderCallback);
        }

        /// <summary>
        /// Return a dictionary containing *all* the properties defined in the scope
        /// of the specifed project. This is not just the variables the user is using
        /// in their style sheet, but all the standard MSBuild properties as well.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>Dictionary of properties.</returns>
        IDictionary<string, string> GetPropertyValues(ProjectInstance project)
        {
            var props = new Dictionary<string, string>();

            foreach (var property in project.Properties)
            {
                props[property.Name] = property.EvaluatedValue;
            }

            return props;
        }
    }
}
