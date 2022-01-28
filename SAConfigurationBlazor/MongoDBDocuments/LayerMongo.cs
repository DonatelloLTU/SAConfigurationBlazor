using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLibrary.Models.MongoDBDocuments
{
    public class LayerMongo : ILayer
    {
        #region Properties
        [BsonElement("LayerID")]
        public int LayerID { get; set; }
        [BsonElement("RunID")]
        public int RunID { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("LayerType")]
        public LayerTypeEnum LayerType { get; set; }
        public double? ConversionFactor { get; set; }

        #region Coating Data Sheet Parameters

        //public int? CoatingDataSheetID { get; set; } // Reserve for Future
        public WeightConversion WetDensityUnit { get; set; }
        public bool UseWetDensity { get; set; }
        public double WetDensity { get; set; }
        public double DryDensity { get; set; }
        public double PercentSolidByWeight { get; set; }
        public double PercentSolidByVolume { get; set; }

        #endregion

        public ConversionType ConversionType { get; set; }

        #region Peak Analysis Parameters

        public bool CalculatePABeforeFFT { get; set; }
        public bool PaEnable { get; set; }
        public double PaPeakTolerance { get; set; }
        public double PaMaxAmplitude { get; set; }
        public int PaMomentumThreshold { get; set; }
        public double PaHeightThreshold { get; set; }
        public double PaFringeVariance { get; set; }
        public double PaMinThicknessAdjustmentFactor { get; set; }
        public int PaCustomSmoothing { get; set; }
        public double FftYMin { get; set; }

        #endregion

        public int? ScanMinTotalReads { get; set; }
        public int? ScanMinPercentGoodReads { get; set; }
        public bool IsMeasuredWet { get; set; }
        public WaveformTypeEnum WaveformType { get; set; }
        public SpectrometerType SpectrometerType { get; set; }

        // TODO: Consider only holding one set of WaveLengthParams
        public WaveLengthMessage VIS { get; set; }
        public WaveLengthMessage NIR { get; set; }
        public WaveLengthMessage ER { get; set; }

        #region ISpecLimit, IGraphParams

        /// <summary>
        /// Flag to indicate that the recipe is 2-layer subtractive, and that the relative Graphical settings have 
        /// been recalculated to show the adjusted thickness in the PrimaryLayer which should be measuring Total Thickness
        /// </summary>
        public bool HasBeenAlteredForSubtractive { get; set; }
        public double MaxThickness { get; set; }
        public double GraphTop { get; set; }
        public double UpperSpecLimit { get; set; }
        public double UpperCustomLimit { get; set; }
        public double TargetSpec { get; set; }
        public double LowerCustomLimit { get; set; }
        public double LowerSpecLimit { get; set; }
        public double GraphBottom { get; set; }
        public double MinThickness { get; set; }

        /// <summary>
        /// Minimum Read Percentage per Cycle
        /// </summary>
        public int MinReadsPerCycle { get; set; }

        #endregion

        public SubtractiveInfo SubtractiveSettings { get; set; }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Returns the true Start wavelength
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public double GetActualStartWavelength(SpectrometerType type)
        {
            switch (type)
            {
                case SpectrometerType.ER:
                    return CalculationHelpers.EnergyToWavelength(ER.EndWaveLength);
                default:
                    return GetStartWaveLength(type);
            }
        }

        /// <summary>
        /// returns the true End wavelength
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public double GetActualEndWavelength(SpectrometerType type)
        {
            switch (type)
            {
                case SpectrometerType.ER:
                    return CalculationHelpers.EnergyToWavelength(ER.StartWaveLength);
                default:
                    return GetEndWaveLength(type);
            }
        }

        /// <summary>
        /// Returns recipe defined Start Wavelength
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public double GetStartWaveLength(SpectrometerType type)
        {
            switch (type)
            {
                case SpectrometerType.ER:
                    return ER.StartWaveLength;
                case SpectrometerType.VIS:
                    return VIS.StartWaveLength;
                default:
                    return NIR.StartWaveLength;
            }
        }

        /// <summary>
        /// Returns recipe defined End Wavelength
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public double GetEndWaveLength(SpectrometerType type)
        {
            switch (type)
            {
                case SpectrometerType.ER:
                    return ER.EndWaveLength;
                case SpectrometerType.VIS:
                    return VIS.EndWaveLength;
                default:
                    return NIR.EndWaveLength;
            }
        }

        public int? GetCSARMinPixelsPassed(SpectrometerType type)
        {
            switch (type)
            {
                case SpectrometerType.ER:
                    return ER?.CSARMinPixelsPassed;
                case SpectrometerType.NIR:
                    return NIR?.CSARMinPixelsPassed;
                case SpectrometerType.VIS:
                    return VIS?.CSARMinPixelsPassed;
                default:
                    return null;
            }
        }

        public double? GetCSARPercentageLightShift(SpectrometerType type)
        {
            switch (type)
            {
                case SpectrometerType.NIR:
                    return NIR?.CSARPercentageLightShift;
                case SpectrometerType.VIS:
                    return VIS?.CSARPercentageLightShift;
                case SpectrometerType.ER:
                    return ER?.CSARPercentageLightShift;
                default:
                    return null;
            }
        }

        public double GetRefractiveIndex(SpectrometerType type)
        {
            switch (type)
            {
                case SpectrometerType.ER:
                    return ER.IOR;
                case SpectrometerType.VIS:
                    return VIS.IOR;
                case SpectrometerType.NIR:
                default:
                    return NIR.IOR;
            }
        }

        public void LoadFromLayerInfo(RecipeMessage recipe, LayerInfoMessage layer, SubtractiveInfo subInfo)
        {
            if (recipe == null || layer == null)
                return;
            FftYMin = recipe.FFTYMinimum;
            Name = layer.Name;
            LayerType = layer.LayerType;
            // NIR Wavelengths
            NIR = layer.NIR;
            // ER Wavelengths
            ER = layer.ER;
            // VIS Wavelengths
            VIS = layer.VIS;

            ConversionFactor = layer.ConversionRate;
            MinThickness = layer.ConvertToMicrons(recipe.UnitsOfMeasure, layer.MinThickness);
            MaxThickness = layer.ConvertToMicrons(recipe.UnitsOfMeasure, layer.MaxThickness);
            UpperSpecLimit = layer.ConvertToMicrons(recipe.UnitsOfMeasure, layer.UpperSpecLimit);
            LowerSpecLimit = layer.ConvertToMicrons(recipe.UnitsOfMeasure, layer.LowerSpecLimit);
            UpperCustomLimit = layer.ConvertToMicrons(recipe.UnitsOfMeasure, layer.UpperCustomLimit);
            LowerCustomLimit = layer.ConvertToMicrons(recipe.UnitsOfMeasure, layer.LowerCustomLimit);
            TargetSpec = layer.ConvertToMicrons(recipe.UnitsOfMeasure, layer.TargetSpec);
            CalculatePABeforeFFT = layer.CalculatePABeforeFFT;
            // PA Info
            PaEnable = layer.PAEnable;
            PaCustomSmoothing = layer.PACustomSmoothing;
            PaHeightThreshold = layer.PAHeightThreshold;
            PaMaxAmplitude = layer.PAMaxAmplitude;
            PaMomentumThreshold = layer.PAMomentumThreshold;
            PaPeakTolerance = layer.PAPeakTolerance;
            PaFringeVariance = layer.PAFringeVariance;
            PaMinThicknessAdjustmentFactor = layer.PAMinThicknessAdjustmentFactor;
            PaCustomSmoothing = layer.PACustomSmoothing;
            ScanMinPercentGoodReads = layer.MinReadsPerCycle;
            WaveformType = layer.WaveformType;
            SubtractiveSettings = subInfo;

            //Differential processing requirements
            PercentSolidByVolume = layer.PercentSolidByVolume;
            ConversionType = layer.ConversionType;
            IsMeasuredWet = layer.ConversionType != ConversionType.DryToDry;
        }

        /// <summary>
        /// Evaluate and return subtractive results using Optical path length forumula
        /// </summary>
        /// <param name="results"></param>
        /// <param name="type"></param>
        /// <param name="fftResults"></param>
        /// <param name="paResults"></param>
        public void ApplySubtractiveToResults(CalculatedLayerMessage results, SpectrometerType type, bool fftResults, bool paResults)
        {
            if (results == null)
                return;

            double ior = GetRefractiveIndex(type);

            if (paResults)
            {
                results.PaThickness = 0.0;
                if (results.PaThicknessMicrons != 0.0)
                {
                    double newThickness = CalculationHelpers.EstimateSubtractive(results.PaThicknessMicrons, ior, SubtractiveSettings.Constant_Amount_In_Microns, SubtractiveSettings.IOR);
                    results.PaThicknessMicrons = newThickness;
                    results.PaThickness = newThickness * (ConversionFactor ?? 1.0);
                }
            }

            if (fftResults)
            {
                results.FftThickness = 0.0;
                if (results.FftThicknessMicrons != 0.0)
                {
                    double newThickness = CalculationHelpers.EstimateSubtractive(results.FftThicknessMicrons, ior, SubtractiveSettings.Constant_Amount_In_Microns, SubtractiveSettings.IOR);

                    if (SubtractiveSettings.Mode == SubtractiveModeEnum.Differential)
                    {
                        // Differential_Thickness_Microns is exclusively for providing an 'upstream' top coat system with the micron value necessary for subtractive.
                        var reduceThicknessBy = GetDifferentialSolventReduction(newThickness);
                        results.DifferentialThicknessMicrons = (reduceThicknessBy.HasValue) ? results.FftThicknessMicrons - reduceThicknessBy.Value : results.FftThicknessMicrons;
                    }

                    results.FftThicknessMicrons = newThickness;
                    results.FftThickness = newThickness * (ConversionFactor ?? 1.0);
                }
            }
        }

        public double? GetDifferentialSolventReduction(double layerThickness)
        {
            // Differential_Thickness_Microns is exclusively for providing an 'upstream' top coat system with the micron value necessary for subtractive.
            if (IsMeasuredWet == true)
            {
                if (PercentSolidByVolume > 0)
                    return (layerThickness * (1 - (PercentSolidByVolume * 0.01)));
                else
                    return null;
            }
            else
                return null;
        }

        #endregion

    }
}
