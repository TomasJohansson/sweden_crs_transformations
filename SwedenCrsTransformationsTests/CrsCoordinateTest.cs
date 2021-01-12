﻿using NUnit.Framework;
using SwedenCrsTransformations;
using static SwedenCrsTransformationsTests.CrsProjectionFactoryTest; // to be able to use constants such as epsgNumberForSweref99tm

namespace SwedenCrsTransformationsTests {
    
    [TestFixture]
    public class CrsCoordinateTest {
        
        [Test]
        public void CreateCoordinateByEpsgNumber() {
            const double x = 20.0;
            const double y = 60.0;
            CrsCoordinate crsCoordinate = CrsCoordinate.CreateCoordinate(epsgNumberForSweref99tm, x, y);
            Assert.AreEqual(epsgNumberForSweref99tm, crsCoordinate.CrsProjection.GetEpsgNumber());
            Assert.AreEqual(x, crsCoordinate.LongitudeX);
            Assert.AreEqual(y, crsCoordinate.LatitudeY);
        }

        [Test]
        public void CreateCoordinate() {
            const double x = 22.5;
            const double y = 62.5;
            CrsCoordinate crsCoordinate = CrsCoordinate.CreateCoordinate(CrsProjection.sweref_99_tm, x, y);
            Assert.AreEqual(epsgNumberForSweref99tm, crsCoordinate.CrsProjection.GetEpsgNumber());
            Assert.AreEqual(CrsProjection.sweref_99_tm, crsCoordinate.CrsProjection);
            Assert.AreEqual(x, crsCoordinate.LongitudeX);
            Assert.AreEqual(y, crsCoordinate.LatitudeY);
        }


        private const double latitudeStockholmCentralStation = 59.330231;
        private const double longitudeStockholmCentralStation = 18.059196;

        [Test]
        public void EqualityTest() {
            CrsCoordinate coordinateInstance_1 = CrsCoordinate.CreateCoordinate(CrsProjection.wgs84, longitudeStockholmCentralStation, latitudeStockholmCentralStation);
            CrsCoordinate coordinateInstance_2 = CrsCoordinate.CreateCoordinate(CrsProjection.wgs84, longitudeStockholmCentralStation, latitudeStockholmCentralStation);
            Assert.AreEqual(coordinateInstance_1, coordinateInstance_2);
            Assert.AreEqual(coordinateInstance_1.GetHashCode(), coordinateInstance_2.GetHashCode());
            Assert.IsTrue(coordinateInstance_1 == coordinateInstance_2);
            Assert.IsTrue(coordinateInstance_2 == coordinateInstance_1);
            Assert.IsTrue(coordinateInstance_1.Equals(coordinateInstance_2));
            Assert.IsTrue(coordinateInstance_2.Equals(coordinateInstance_1));


            double delta = 0.000000000000001; // see comments further below regarding the value of "delta"
            CrsCoordinate coordinateInstance_3 = CrsCoordinate.CreateCoordinate(
                CrsProjection.wgs84,
                longitudeStockholmCentralStation + delta,
                latitudeStockholmCentralStation + delta
            );
            Assert.AreEqual(coordinateInstance_1, coordinateInstance_3);
            Assert.AreEqual(coordinateInstance_1.GetHashCode(), coordinateInstance_3.GetHashCode());
            Assert.IsTrue(coordinateInstance_1 == coordinateInstance_3); // method "operator =="
            Assert.IsTrue(coordinateInstance_3 == coordinateInstance_1);
            Assert.IsTrue(coordinateInstance_1.Equals(coordinateInstance_3));
            Assert.IsTrue(coordinateInstance_3.Equals(coordinateInstance_1));

            // Regarding the chosen value for "delta" (which is added to the lon/lat values, to create a slightly different value) above and below,
            // it is because of experimentation this "breakpoint" value has been determined, i.e. the above value still resulted in equality 
            // but when it was increased as below with one decimal then the above kind of assertions failed and therefore the other assertions below 
            // are used instead e.g. testing the overloaded operator "!=".
            // You should generally be cautios when comparing floating point values but the above test indicate that values are considered equal even though 
            // the difference is as 'big' as in the "delta" value above.

            delta = delta * 10; // moving the decimal one bit to get a somewhat larger values, and then the instances are not considered equal, as you can see in the tests below.
            CrsCoordinate coordinateInstance_4 = CrsCoordinate.CreateCoordinate(
                CrsProjection.wgs84,
                longitudeStockholmCentralStation + delta,
                latitudeStockholmCentralStation + delta
            );
            // Note that below are the Are*NOT*Equal assertions made instead of AreEqual as further above when a smaller delta value was used
            Assert.AreNotEqual(coordinateInstance_1, coordinateInstance_4);
            Assert.AreNotEqual(coordinateInstance_1.GetHashCode(), coordinateInstance_4.GetHashCode());
            Assert.IsTrue(coordinateInstance_1 != coordinateInstance_4); // Note that the method "operator !=" becomes used here
            Assert.IsTrue(coordinateInstance_4 != coordinateInstance_1);
            Assert.IsFalse(coordinateInstance_1.Equals(coordinateInstance_4));
            Assert.IsFalse(coordinateInstance_4.Equals(coordinateInstance_1));
        }


        [Test]
        public void ToStringTest() {
            CrsCoordinate coordinate = CrsCoordinate.CreateCoordinate(CrsProjection.sweref_99_18_00, 153369.673, 6579457.649);
            Assert.AreEqual(
                "CrsCoordinate [ X: 153369.673 , Y: 6579457.649 , CRS: SWEREF_99_18_00 ]",
                coordinate.ToString()
            );
            CrsCoordinate coordinate2 = CrsCoordinate.CreateCoordinate(CrsProjection.wgs84, 18.059196, 59.330231);
            Assert.AreEqual(
                "CrsCoordinate [ Longitude: 18.059196 , Latitude: 59.330231 , CRS: WGS84 ]",
                coordinate2.ToString()
            );
            // now testing the same coordinate as above but with a custom 'ToString' implementation
            CrsCoordinate.SetToStringImplementation(myCustomToStringMethod);
            Assert.AreEqual(
                "59.330231 , 18.059196",
                coordinate2.ToString()
            );
            CrsCoordinate.SetToStringImplementationDefault(); // restores the default 'ToString' implementation
        }

        private string myCustomToStringMethod(CrsCoordinate coordinate) {
            return string.Format(
                "{0} , {1}",
                    coordinate.LatitudeY,
                    coordinate.LongitudeX
            );
        }

    }
}
