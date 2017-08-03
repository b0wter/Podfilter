using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Podfilter.Models.ContentFilters
{
    public abstract class BaseFilter<TMethod, TArgument> : IContentFilter
    {
        //TODO: Test if restoring an instance from the entity framework actually calls the properties (and thus sets MethodSet/ArgumentSet).
        
        /// <summary>
        /// Returns wether a method of operation was set for this filter.
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        [IgnoreDataMember]
        private bool IsMethodSet { get; set; } = false;
        private TMethod _method;
        /// <summary>
        /// Method of operation set for this filter.
        /// </summary>
        public TMethod Method
        {
            get => _method;
            set { _method = value; IsMethodSet = true; }
        }
        
        /// <summary>
        /// Returns wether an argument has been set for this filter.
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        [IgnoreDataMember]
        private bool ArgumentSet { get; set; } = false;
        private TArgument _argument;
        public TArgument Argument
        {
            get => _argument;
            set { _argument = value; ArgumentSet = true; }
        }

        /// <summary>
        /// Type of content the filter is meant for.
        /// </summary>
        public Type TargetType => typeof(TArgument);
        
        protected BaseFilter()
        {
            //
        }

        protected BaseFilter(TMethod method, TArgument argument)
        {
            this.Method = method;
            this.Argument = argument;
        }
        
        /// <summary>
        /// Determines if <paramref name="obj"/> satisfies this filter.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool PassesFilter(object obj)
        {
            ValidateMethodAndArgumentSet();
            var toTest = ConvertToTDefault(obj);
            return PassesFilter(toTest);
        }

        /// <summary>
        /// Tries to parse the content of <paramref name="objectAsString"/> as <see cref="TArgument"/> and determines if it satisfies this filter.
        /// </summary>
        /// <param name="objectAsString"></param>
        /// <returns></returns>
        public bool ParsedPassesFilter(string objectAsString)
        {
            var obj = ParseString(objectAsString);
            return PassesFilter(obj);
        }

        /// <summary>
        /// Does the actual testing, needs to be overriden by the implementing classes.
        /// </summary>
        /// <param name="toTest"></param>
        /// <returns></returns>
        protected abstract bool PassesFilter(TArgument toTest);

        /// <summary>
        /// Parses a string to make an instance of <see cref="TArgument"/>.
        /// </summary>
        /// <param name="stringifiedObject"></param>
        /// <returns></returns>
        protected abstract TArgument ParseString(string stringifiedObject);

        /// <summary>
        /// Checks if the type of <paramref name="obj"/> is the same as <see cref="TArgument"/> and casts <paramref name="obj"/>.
        /// Can be overriden by implementing classes to allow a more complex casting.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If the types dont match.</exception>
        protected virtual TArgument ConvertToTDefault(object obj)
        {
            if (typeof(TArgument) == obj.GetType())
                return (TArgument)obj;
            else if (obj.GetType() == typeof(string))
                return ParseString(obj.ToString());
            if (typeof(TArgument) != obj.GetType())
                throw new ArgumentException($"Parameter is of type {obj.GetType().Name}, {typeof(TArgument)} expected.");
            else
                return (TArgument) obj;
        }

        /// <summary>
        /// Checks if <see cref="Method"/> and <see cref="Argument"/> are set.
        /// </summary>
        /// <exception cref="InvalidOperationException">If either <see cref="Method"/> or <see cref="Argument"/> are not set.</exception>
        protected void ValidateMethodAndArgumentSet()
        {
            if(IsMethodSet == false || Method == null)
                throw new InvalidOperationException("Method has not been set!");
            
            if(ArgumentSet == false || Argument == null)
                throw new InvalidOperationException("Argument has not been set!");
        }
    }
    
}