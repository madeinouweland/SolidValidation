using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using SolidValidation.Validation;

namespace SolidValidation.Controls {
    [TemplatePartAttribute(Name = "InputContainerPresenter", Type = typeof(ContentPresenter))]
    public class InputContainer : ContentControl {

        #region EditProp Dependency Property

        /// <summary> 
        /// Get or Sets the EditProp dependency property.  
        /// </summary> 
        public object EditProp {
            get { return (object)GetValue(EditPropProperty); }
            set { SetValue(EditPropProperty, value); }
        }

        /// <summary> 
        /// Identifies the EditProp dependency property. This enables animation, styling, binding, etc...
        /// </summary> 
        public static readonly DependencyProperty EditPropProperty =
            DependencyProperty.Register("EditProp",
                                        typeof(object),
                                        typeof(InputContainer),
                                        new PropertyMetadata(null));

        #endregion EditProp Dependency Property


        public string Caption {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption",
                                        typeof(string),
                                        typeof(InputContainer),
                                        new PropertyMetadata(null));


        public double CaptionWidth {
            get { return (double)GetValue(CaptionWidthProperty); }
            set { SetValue(CaptionWidthProperty, value); }
        }

        public static readonly DependencyProperty CaptionWidthProperty =
            DependencyProperty.Register("CaptionWidth",
                                        typeof(double),
                                        typeof(InputContainer),
                                        new PropertyMetadata(null));


        //     private ContentPresenter contentPresenter;
        public InputContainer() {
            this.DefaultStyleKey = typeof(InputContainer);
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            var editProp = EditProp as IEditProp;
            if (editProp != null && editProp.IsNullable) {
                VisualStateManager.GoToState(this, "IsNullable", false);
            }

            //contentPresenter = GetTemplateChild("InputContainerPresenter") as ContentPresenter;
            //var datatemplate=contentPresenter.ContentTemplate as DataTemplate;
            //var dt= this.ContentTemplate as DataTemplate;
        }

        //public IEnumerable<UIElement> All(Panel panel) {
        //    foreach (var uiElement in panel.Children) {
        //        var element = uiElement as Panel;
        //        if (element != null) {
        //            foreach (var grandChild in All(element))
        //                yield return grandChild;
        //        }
        //        yield return uiElement;
        //    }
        //}
    }
}
