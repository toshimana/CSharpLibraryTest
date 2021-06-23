using Reactive.Bindings.Interactivity;
using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CSharpLibraryTest
{
    // https://blog.okazuki.jp/entry/2019/04/19/172010
    // https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/how-to-hit-test-in-a-viewport3d?view=netframeworkdesktop-4.8
    public class MouseClickToMousePositionConverter : ReactiveConverter<MouseButtonEventArgs, Point3D>
    {
        private Point3D? _hitPosition = null;
        private double _distance = 0.0;
        private HitTestResultBehavior ResultCallback(HitTestResult result)
        {
            RayHitTestResult rayResult = result as RayHitTestResult;

            if (rayResult != null)
            {
                RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;
                if (rayMeshResult.DistanceToRayOrigin < _distance)
                {
                    _hitPosition = rayMeshResult.PointHit;
                    _distance = rayMeshResult.DistanceToRayOrigin;
                }
            }

            return HitTestResultBehavior.Continue;
        }
        protected override IObservable<Point3D> OnConvert(IObservable<MouseButtonEventArgs> source) =>
            source.Select(e => e.GetPosition((IInputElement)AssociateObject))
                .Select(pos => new PointHitTestParameters(pos))
                .Select(hitParams =>
                {
                    _hitPosition = null;
                    _distance = double.MaxValue;
                    VisualTreeHelper.HitTest((Viewport3D)AssociateObject, null, ResultCallback, hitParams);
                    return _hitPosition;
                })
                .Where(p => p.HasValue)
                .Select(p => p.Value);
    }
}
