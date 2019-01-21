using System;
using System.Numerics;
using Windows.Foundation;
#if XAMARIN_ANDROID
using _View = Android.Views.View;
#elif XAMARIN_IOS_UNIFIED
using _View = UIKit.UIView;
#elif __WASM__
using _View = Windows.UI.Xaml.UIElement;
#else
using _View = System.Object;
#endif

namespace Windows.UI.Xaml.Media
{
	public partial class MatrixTransform : Transform
	{
		internal override Matrix3x2 ToMatrix(Point absoluteOrigin)
		{
			return Matrix.Inner;
		}

		public Matrix Matrix
		{
			get => (Matrix)GetValue(MatrixProperty);
			set => SetValue(MatrixProperty, value);
		}

		public static global::Windows.UI.Xaml.DependencyProperty MatrixProperty { get; } =
			Windows.UI.Xaml.DependencyProperty.Register(
				"Matrix", typeof(Matrix),
				typeof(MatrixTransform),
				new FrameworkPropertyMetadata(Matrix.Identity, NotifyChangedCallback));
	}
}
