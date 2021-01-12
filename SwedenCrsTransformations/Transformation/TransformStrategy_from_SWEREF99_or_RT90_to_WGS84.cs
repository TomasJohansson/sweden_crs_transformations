﻿using MightyLittleGeodesy.Classes;

namespace SwedenCrsTransformations.Transformation {
    internal class TransformStrategy_from_SWEREF99_or_RT90_to_WGS84 : TransformStrategy {
        // Precondition: sourceCoordinate must be CRS SWEREF99 or RT90
        public CrsCoordinate Transform(
            CrsCoordinate sourceCoordinate,
            CrsProjection targetCrsProjection
        ) {
            var gkProjection = new GaussKreuger();
            gkProjection.swedish_params(sourceCoordinate.CrsProjection);
            LonLat lonLat = gkProjection.grid_to_geodetic(sourceCoordinate.LatitudeY, sourceCoordinate.LongitudeX);
            return CrsCoordinate.CreateCoordinate(targetCrsProjection, lonLat.LongitudeX, lonLat.LatitudeY);
        }
    }

}