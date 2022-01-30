//using System;
//using System.Collections.Generic;
//using System.Text;
//using DataLibrary.Models.Data_Structures;

//namespace DataLibrary.Models.MongoDBDocuments
//{
//    /// <summary>
//    /// This is a mutable object.
//    /// This allows short-term changes to the operation of LineState to accommodate alternative reading gathering. 
//    /// The default structure implements standard 'Inline' operation.
//    /// </summary>
//    public class LineParameterOverrideMessageMongo
//    {
//        /// <summary>
//        /// Indicates a specific number of readings that are to be taken and stored
//        /// </summary>
//        public int Count { get; set; } = 0;

//        /// <summary>
//        /// (Seconds) This will temporarily change the probe cycle interval for 'this' specific Start/Stop cycle,
//        /// </summary>
//        public int IntervalOverride { get; set; } = 1000;

//        /// <summary>
//        /// Changes the way the Service will process a run.  Default is for "Inline" for Continuous readings without interruption
//        /// Single = Will execute 1 reading per probe cycle
//        /// Scan = Will override the ProbeCycle Interval by [IntervalOverride] amount of seconds. (Max = 30 seconds)
//        /// Triggered = Will execute a single ProbeCycle for up to 30 seconds to yield a single reading. (Max = 30 seconds)
//        /// </summary>
//        public ReadMode Mode { get; set; } = ReadMode.Continuous;

//        /// <summary>
//        /// (Microseconds) Will set starting IT, or set as semi-permanent IT if StaticIT = true
//        /// </summary>
//        public int IntegrationTime { get; set; } = 2000;

//        /// <summary>
//        /// Forces system to maintain the IT time stored in the recipe, or that is overridden in RecipeOverride.Integration_Time
//        /// </summary>
//        public bool StaticIT { get; set; } = false;

//        /// <summary>
//        /// Forces all readings to use the same reference for evaluation
//        /// </summary>
//        public bool UseStaticReferece { get; set; } = false;

//        public bool EvaluateCSAR { get; set; } = true;

//        public bool? UseRestrictedReflectanceRange { get; set; } = null;

//        public bool ExternalTrigger { get; set; } = false;

//        /// <summary>
//        /// Recipe fields that are allowable for override.  Final validation is applied to all RecipeOverride values.
//        /// </summary>
//        public RecipeOverrideMessageMongo RecipeOverride { get; set; }

//        public LaneOverrideMessage LaneOverride { get; set; }
//    }
//}
