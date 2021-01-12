﻿namespace SwedenCrsTransformations.Transformation {
    internal class TransFormStrategy_From_Sweref99OrRT90_to_WGS84_andThenToRealTarget : TransformStrategy {
        // Precondition: sourceCoordinate must be CRS SWEREF99 or RT90
        public CrsCoordinate Transform(
            CrsCoordinate sourceCoordinate,
            CrsProjection targetCrsProjection
        ) {
            var wgs84coordinate = Transformer.Transform(sourceCoordinate, CrsProjection.wgs84);
            return Transformer.Transform(wgs84coordinate, targetCrsProjection);
        }
    }

}