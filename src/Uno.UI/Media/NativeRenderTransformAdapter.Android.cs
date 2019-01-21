using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Android.Views;
using Android.Views.Animations;
using Microsoft.Extensions.Logging;
using Uno.Extensions;
using Uno.Logging;
using Uno.UI.Controls;

namespace Uno.UI.Media
{
	partial class NativeRenderTransformAdapter
	{
		partial void Initialized()
			=> UpdateParent(null, Owner.Parent);

		/// <summary>
		/// Gets the native transform that can be applied by the parent ViewGroup for the given child view
		/// </summary>
		internal MatrixTransformation Matrix { get; } = new MatrixTransformation();

		public void UpdateParent(object oldParent, object newParent)
		{
			// The only way to apply a matrix transform on Android is by using the "static transform" on the parent `GroupView`.
			// Here we register on the parent this RenderTransform for on of its children.

			(oldParent as BindableView)?.UnregisterChildTransform(this);

			if (newParent is BindableView newBindableParent)
			{
				newBindableParent.RegisterChildTransform(this);
			}
			else if (newParent != null && this.Log().IsEnabled(LogLevel.Error))
			{
				this.Log().Error(
					$"A RenderTransform was set on '{Owner}', but its parent is not a BindableView ({newParent.GetType().Name}), "
					+ $"so the transform won't have any effect.");
			}
		}

		partial void Apply(Matrix3x2 matrix, bool isSizeChanged)
		{
			Matrix.Set(matrix);

			if (!isSizeChanged)
			{
				Owner.Invalidate();
			}
		}

		partial void Cleanup()
		{
			(Owner.Parent as BindableView)?.UnregisterChildTransform(this);

			Owner.Invalidate();
		} 

		internal class MatrixTransformation : Transformation
		{
			public MatrixTransformation()
				: this(Matrix3x2.Identity)
			{
			}

			public MatrixTransformation(Matrix3x2 matrix)
			{
				Alpha = 1;
				TransformationType = TransformationTypes.Matrix;

				Set(matrix);
			}

			public bool IsIdentity { get; private set; }

			public void Set(Matrix3x2 matrix)
			{
				Matrix.SetValues(new[]
				{
					matrix.M11, matrix.M21, ViewHelper.LogicalToPhysicalPixels(matrix.M31),
					matrix.M12, matrix.M22, ViewHelper.LogicalToPhysicalPixels(matrix.M32),
					0, 0, 1
				});
				IsIdentity = matrix.IsIdentity;
			}
		}
	}
}
