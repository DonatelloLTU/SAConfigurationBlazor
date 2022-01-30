//using System;
//using System.Collections.Generic;
//using System.Text;
//using DataLibrary.Models.MongoDBDocuments;
//using DataLibrary.Models.Data_Structures;
//using System.Linq;
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;

//namespace DataLibrary.Models.MongoDBDocuments
//{
//    public class RunInfoJsonMongoDocument
//    {

//        #region Properties
//        [BsonElement("RunID")]
//        public int RunID { get; set; }

//        public RunLayerMessage[] Layers { get; set; }

//        public RunLayerMessage[] Layers_BaseCoat { get; set; }

//        public LineInfoMessageMongo Line { get; set; }
//        [BsonElement("Line")]
//        public string RecipeName { get; set; }

//        public RecipeMessage Recipe { get; set; }
//        [BsonElement("Base_Coat_Name")]
//        public string Base_Coat_Name { get; set; }

//        public RecipeMessage Base_Coat { get; set; }
//        [BsonElement("WorkOrderNbr")]
//        public string WorkOrderNbr { get; set; }

//        public DateTime StartTime { get; set; }

//        public DateTime? EndTime { get; set; }

//        public double? GoodReadsPercentage { get; set; }

//        public int? TotalReads { get; set; }

//        public int DebugCount { get; set; }

//        public int MinReadsPerCycle { get; set; }

//        public SpecMetrixApplicationEnum RequestingApplication { get; set; }

//        public LineParameterOverrideMessageMongo RuntimeOverride { get; set; }

//        public bool Is_Two_Layer_Subtractive_Mode => Recipe != null &&
//                    Recipe.EnableSubtractMode == true &&
//                    Layers.Any(x => x.SubtractiveSettings.Mode == SubtractiveModeEnum.PrimaryLayer) &&
//                    Layers.Any(x => x.SubtractiveSettings.Mode == SubtractiveModeEnum.SubtractiveLayer);

//        #endregion

//        #region Helper Properties


//        /// <summary>
//        /// Returns the layers that we are interested in.  We register this with
//        /// the Static RecipeController so that all controls can call this.
//        /// </summary>
//        /// <param name="layers"></param>
//        /// <returns></returns>
//        public IEnumerable<LayerInfoMessage> GetInterestingLayers(RecipeMessage recipe)
//        {
//            if (recipe == null)
//                yield break;

//            if (recipe.Layers == null || recipe.Layers.Any() == false)
//                yield break;

//            if (recipe.Layers.Any(x => x.LayerType == LayerTypeEnum.InlinePrimary))
//            {
//                var primaryLayer = recipe.Layers.FirstOrDefault(x => x.LayerType == LayerTypeEnum.InlinePrimary);
//                var subtractiveLayer = recipe.Layers.FirstOrDefault(x => x.LayerType == LayerTypeEnum.Subtractive);

//                if (subtractiveLayer != null && recipe.EnableSubtractMode)
//                {
//                    // This is here to prevent flicker in graphs when painting when the recipe is changed and a mouseover occurs
//                    // before the recipe is updated in the OnConfigurationChange event from ther ClientProductionLine.cs
//                    if (primaryLayer.HasBeenAlteredForSubtractive == false)
//                    {
//                        primaryLayer.HasBeenAlteredForSubtractive = true;
//                        primaryLayer.TargetSpec -= subtractiveLayer.TargetSpec;
//                        primaryLayer.GraphTop -= subtractiveLayer.TargetSpec;
//                        primaryLayer.GraphBottom -= subtractiveLayer.TargetSpec;
//                        primaryLayer.LowerSpecLimit -= subtractiveLayer.TargetSpec;
//                        primaryLayer.UpperSpecLimit -= subtractiveLayer.TargetSpec;
//                    }
//                }

//                yield return primaryLayer;

//                if (recipe.Layers.Any(x => x.LayerType == LayerTypeEnum.InlineSecondary))
//                    yield return recipe.Layers.FirstOrDefault(x => x.LayerType == LayerTypeEnum.InlineSecondary);

//                if (recipe.EnableSubtractMode == false && subtractiveLayer != null)
//                    yield return subtractiveLayer;
//            }
//            else
//                yield return recipe.Layers.First();
//        }

//        /// <summary>
//        /// Returns the layers that we are interested in.  We register this with
//        /// the Static RecipeController so that all controls can call this.
//        /// </summary>
//        /// <param name="layers"></param>
//        /// <returns></returns>
//        public IEnumerable<RunLayerMessage> GetInterestingLayers()
//        {
//            if (Recipe == null)
//                yield break;

//            if (Layers == null || Layers.Any() == false)
//                yield break;

//            if (Layers.Any(x => x.LayerType == LayerTypeEnum.InlinePrimary))
//            {
//                var primaryLayer = Layers.FirstOrDefault(x => x.LayerType == LayerTypeEnum.InlinePrimary);
//                yield return primaryLayer;

//                if (Recipe.Layers.Any(x => x.LayerType == LayerTypeEnum.InlineSecondary))
//                    yield return Layers.FirstOrDefault(x => x.LayerType == LayerTypeEnum.InlineSecondary);

//                var subtractiveLayer = Layers.FirstOrDefault(x => x.LayerType == LayerTypeEnum.Subtractive);
//                if (Recipe.EnableSubtractMode == false && subtractiveLayer != null)
//                    yield return subtractiveLayer;
//            }
//            else
//                yield return Layers.First();
//        }

//        #endregion

//    }
//}
