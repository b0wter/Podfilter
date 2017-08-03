using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Podfilter.Models.ContentFilters
{
    public abstract class BaseFilter<T, U> : IContentFilter
    {
        //TODO: Test if restoring an instance from the entity framework actually calls the properties (and thus sets MethodSet/ArgumentSet).
        
        [JsonIgnore]
        [NotMapped]
        [IgnoreDataMember]
        public bool MethodSet { get; set; } = false;
        private T _method;
        public T Method
        {
            get => _method;
            set { _method = value; MethodSet = true; }
        }
        
        [JsonIgnore]
        [NotMapped]
        [IgnoreDataMember]
        public bool ArgumentSet { get; set; } = false;
        private U _argument;
        public U Argument
        {
            get => _argument;
            set { _argument = value; ArgumentSet = true; }
        }

        public Type TargetType => typeof(U);
        
        public BaseFilter()
        {
            //
        }

        public BaseFilter(T method, U argument)
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
        /// Tries to parse the content of <paramref name="objectAsString"/> as <see cref="U"/> and determines if it satisfies this filter.
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
        protected abstract bool PassesFilter(U toTest);

        /// <summary>
        /// Parses a string to make an instance of <see cref="U"/>.
        /// </summary>
        /// <param name="stringifiedObject"></param>
        /// <returns></returns>
        protected abstract U ParseString(string stringifiedObject);

        /// <summary>
        /// Checks if the type of <paramref name="obj"/> is the same as <see cref="U"/> and casts <paramref name="obj"/>.
        /// Can be overriden by implementing classes to allow a more complex casting.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If the types dont match.</exception>
        protected virtual U ConvertToTDefault(object obj)
        {
            if (typeof(U) == obj.GetType())
                return (U)obj;
            else if (obj.GetType() == typeof(string))
                return ParseString(obj.ToString());
            if (typeof(U) != obj.GetType())
                throw new ArgumentException($"Parameter is of type {obj.GetType().Name}, {typeof(U)} expected.");
            else
                return (U) obj;
        }

        /// <summary>
        /// Checks if <see cref="Method"/> and <see cref="Argument"/> are set.
        /// </summary>
        /// <exception cref="InvalidOperationException">If either <see cref="Method"/> or <see cref="Argument"/> are not set.</exception>
        protected void ValidateMethodAndArgumentSet()
        {
            if(MethodSet == false || Method == null)
                throw new InvalidOperationException("Method has not been set!");
            
            if(ArgumentSet == false || Argument == null)
                throw new InvalidOperationException("Argument has not been set!");
        }
    }
    
}