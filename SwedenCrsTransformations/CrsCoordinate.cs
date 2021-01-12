﻿using SwedenCrsTransformations.Transformation;
using System;
using System.Collections.Generic;

namespace SwedenCrsTransformations {
    public class CrsCoordinate : IEquatable<CrsCoordinate> {

        public CrsProjection CrsProjection { get; private set; }
        
        public double LongitudeX { get; private set; }
        public double LatitudeY { get; private set; }

        private CrsCoordinate(
            CrsProjection crsProjection,
            double xLongitude,
            double yLatitude
        ) {
            this.CrsProjection = crsProjection;
            this.LongitudeX = xLongitude;
            this.LatitudeY = yLatitude;
        }

        public CrsCoordinate Transform(CrsProjection targetCrsProjection) {
            return Transformer.Transform(this, targetCrsProjection);
        }


        public static CrsCoordinate CreateCoordinate(
            int epsgNumber,
            double xLongitude,
            double yLatitude
        ) {
            CrsProjection crsProjection = CrsProjectionFactory.GetCrsProjectionByEpsgNumber(epsgNumber);
            return CreateCoordinate(crsProjection, xLongitude, yLatitude);
        }

        public static CrsCoordinate CreateCoordinate(
            CrsProjection crsProjection,
            double xLongitude,
            double yLatitude
        ) {
            return new CrsCoordinate(crsProjection, xLongitude, yLatitude);
        }

        // ----------------------------------------------------------------------------------------------------------------------
        // These five methods below was generated with Visual Studio 2019
        public override bool Equals(object obj) {
            return Equals(obj as CrsCoordinate);
        }

        public bool Equals(CrsCoordinate other) {
            return other != null &&
                   CrsProjection == other.CrsProjection &&
                   LongitudeX == other.LongitudeX &&
                   LatitudeY == other.LatitudeY;
        }

        public override int GetHashCode() {
            int hashCode = 1147467376;
            hashCode = hashCode * -1521134295 + CrsProjection.GetHashCode();
            hashCode = hashCode * -1521134295 + LongitudeX.GetHashCode();
            hashCode = hashCode * -1521134295 + LatitudeY.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(CrsCoordinate left, CrsCoordinate right) {
            return EqualityComparer<CrsCoordinate>.Default.Equals(left, right);
        }

        public static bool operator !=(CrsCoordinate left, CrsCoordinate right) {
            return !(left == right);
        }

        // These five methods above was generated with Visual Studio 2019
        // ----------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Two examples of the string that can be returned:
        /// "CrsCoordinate [ X: 153369.673 , Y: 6579457.649 , CRS: SWEREF_99_18_00 ]"
        /// "CrsCoordinate [ Longitude: 18.059196 , Latitude: 59.330231 , CRS: WGS84 ]"
        /// </summary>
        public override string ToString() {
            return _toStringImplementation(this);
        }

        private static Func<CrsCoordinate, string> _toStringImplementation = defaultToStringImplementation;
        
        private static string defaultToStringImplementation(CrsCoordinate coordinate) {
            string crs = coordinate.CrsProjection.ToString().ToUpper();
            bool isWgs84 =  coordinate.CrsProjection.IsWgs84();
            string xOrLongitude = isWgs84 ? "Longitude" : "X";
            string yOrLatitude = isWgs84 ? "Latitude" : "Y";
            return string.Format(
                "{0} [ {1}: {2} , {3}: {4} , CRS: {5} ]",
                    nameof(CrsCoordinate),  // 0
                    xOrLongitude,           // 1
                    coordinate.LongitudeX,  // 2
                    yOrLatitude,            // 3
                    coordinate.LatitudeY,   // 4
                    crs                     // 5
            );
        }

        public static void SetToStringImplementation(Func<CrsCoordinate, string> toStringImplementation) {
            _toStringImplementation = toStringImplementation;
        }
        public static void SetToStringImplementationDefault() { 
            _toStringImplementation = defaultToStringImplementation;
        }
    }
}
